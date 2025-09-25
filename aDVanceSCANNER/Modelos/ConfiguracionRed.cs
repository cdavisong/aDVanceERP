using System.Net;

namespace aDVanceSCANNER.Modelos;

public class ConfiguracionRed
{
    public string? DireccionIP { get; private set; } = "127.0.0.1";
    public int Puerto { get; private set; } = 9002;

    public bool EstablecerDireccionIp(string direccionIP)
    {
        if (!IPAddress.TryParse(direccionIP, out var ip))
            return false;

        DireccionIP = ip.ToString();

        return true;
    }

    public bool EstablecerPuerto(int puerto)
    {
        if (puerto is < 1 or > 65535)
            return false;

        Puerto = puerto;

        return true;
    }
}