using System.Net.Sockets;
using System.Text;

namespace aDVanceSCANNER.Modelos {
    public sealed class ClienteTCP(ConfiguracionRed? configuracionRed = null) : IDisposable {
        private readonly ConfiguracionRed _configuracionRed = configuracionRed ?? new ConfiguracionRed();
        
        private TcpClient? _cliente;
        private NetworkStream? _stream;
        private bool _disposed;

        public bool Conectado {
            get => _cliente?.Connected == true;
        }

        public bool EstablecerDireccionIp(string direccionIP) =>
            _configuracionRed.EstablecerDireccionIp(direccionIP);

        public bool EstablecerPuerto(int puerto) =>
            _configuracionRed.EstablecerPuerto(puerto);

        public async Task<string> ConectarAsync() {
            if (_configuracionRed.DireccionIP == null)
                return "La dirección IP no es válida";

            try {
                await DesconectarAsync();
                _cliente = new TcpClient();

                var connectTask = _cliente.ConnectAsync(_configuracionRed.DireccionIP, _configuracionRed.Puerto);
                
                if (await Task.WhenAny(connectTask, Task.Delay(5000)) != connectTask) {
                    throw new TimeoutException("Tiempo de conexión agotado");
                }

                _stream = _cliente.GetStream();
                return $"Conectado a {_configuracionRed.DireccionIP}";
            } catch (SocketException ex) {
                return $"Error de socket: {ex.SocketErrorCode}"; // Ej: ConnectionRefused, TimedOut
            } catch (Exception ex) {
                return $"Error: {ex.Message}"; // Ej: Permission denied
            }
        }

        public async Task<string> EnviarAsync(string datos) {
            if (!Conectado || _stream is not { CanWrite: true })
                return "No hay conexión de red activa";

            try {
                var bytesDatos = Encoding.UTF8.GetBytes(datos + Environment.NewLine);
                await _stream.WriteAsync(bytesDatos, 0, bytesDatos.Length);
                return "Datos enviados";
            } catch (Exception ex) {
                return $"Error al enviar: {ex.Message}";
            }
        }

        public async Task DesconectarAsync() {
            if (_stream != null) {
                await _stream.DisposeAsync();
                _stream = null;
            }
            _cliente?.Dispose();
            _cliente = null;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async void Dispose(bool disposing) {
            if (_disposed) return;
            if (disposing) await DesconectarAsync();

            _disposed = true;
        }
    }
}