using System.Net;
using System.Net.Sockets;
using System.Text;

namespace aDVanceERP.Core.Servicios.Comun;

public sealed class ServidorTCP : IDisposable
{
    private TcpListener? _listener;
    private TcpClient? _cliente;
    private NetworkStream? _hiloRed;
    private CancellationTokenSource? _cts;
    private bool _disposed;

    public bool ServicioActivo { get; private set; }

    public event Action<string>? DatosRecibidos;
    public event Action<string>? CambioEstado;

    public async void IniciarAsync(int puerto)
    {
        if (ServicioActivo || _disposed)
            return;

        var nombreHost = Dns.GetHostName();

        // Obtener todas las direcciones IP del Host
        var direccionesIp = Dns.GetHostAddresses(nombreHost);
        var direcciones = string.Join(", ", direccionesIp.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).Select(ip => ip.ToString()));

        _cts = new CancellationTokenSource();
        _listener = new TcpListener(IPAddress.Any, puerto);

        try
        {
            _listener.Start();
            ServicioActivo = true;
            OnStatusChanged($"Servidor de scanner iniciado en {(direcciones.Split(',').Length > 1 ? "las direcciones" : "la dirección")} : {direcciones} | Puerto : {puerto}");

            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    _cliente = await _listener.AcceptTcpClientAsync(_cts.Token).ConfigureAwait(false);
                    _hiloRed = _cliente.GetStream();

                    var clienteIp = ((IPEndPoint?)_cliente.Client.RemoteEndPoint)?.Address?.ToString();
                    OnStatusChanged($"Cliente conectado: {clienteIp}");

                    await ProcessClientDataAsync(_cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // Servidor detenido
                }
                catch (SocketException ex)
                {
                    OnStatusChanged($"Error de socket: {ex.SocketErrorCode}");
                }
                catch (IOException ex)
                {
                    OnStatusChanged($"Error de E/S: {ex.Message}");
                }
                catch (Exception ex)
                {
                    OnStatusChanged($"Error inesperado: {ex.Message}");
                }
                finally
                {
                    DisconnectClient();
                }
            }
        }
        finally
        {
            Detener();
        }
    }

    private async Task ProcessClientDataAsync(CancellationToken ct)
    {
        if (_cliente == null || _hiloRed == null) return;

        try
        {
            using (var reader = new StreamReader(_hiloRed, Encoding.UTF8))
            {
                while (!ct.IsCancellationRequested && _cliente.Connected)
                {
                    var data = await reader.ReadLineAsync(ct).ConfigureAwait(false);

                    if (data != null)
                    {
                        OnDataReceived(data);
                    }
                    else
                    {
                        break; // Cliente desconectado
                    }
                }
            }
        }
        catch (Exception ex)
        {
            OnStatusChanged($"Error procesando datos: {ex.Message}");
        }
    }

    public void Detener()
    {
        if (!ServicioActivo || _disposed) return;

        try
        {
            _cts?.Cancel();
            _listener?.Stop();
            DisconnectClient();
            ServicioActivo = false;
            OnStatusChanged("Servidor detenido");
        }
        catch (Exception ex)
        {
            OnStatusChanged($"Error al detener servidor: {ex.Message}");
        }
    }

    private void DisconnectClient()
    {
        try
        {
            _hiloRed?.Dispose();
            _cliente?.Dispose();
        }
        catch (Exception ex)
        {
            OnStatusChanged($"Error desconectando cliente: {ex.Message}");
        }
        finally
        {
            _hiloRed = null;
            _cliente = null;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;

        Detener();

        _cts?.Dispose();
        _listener?.Stop();
        _listener = null;

        _disposed = true;
        GC.SuppressFinalize(this);
    }

    private void OnDataReceived(string data)
    {
        try
        {
            DatosRecibidos?.Invoke(data);
        }
        catch (Exception ex)
        {
            OnStatusChanged($"Error en evento DatosRecibidos: {ex.Message}");
        }
    }

    private void OnStatusChanged(string status)
    {
        try
        {
            CambioEstado?.Invoke(status);
        }
        catch (Exception ex)
        {
            // Loggear error pero no propagar
            Console.WriteLine($"Error en evento CambioEstado: {ex.Message}");
        }
    }
}