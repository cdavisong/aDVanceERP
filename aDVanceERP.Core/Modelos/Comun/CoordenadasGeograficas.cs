using System.ComponentModel;
using System.Globalization;

namespace aDVanceERP.Core.Modelos.Comun;

[Serializable]
public class CoordenadasGeograficas : ICloneable, INotifyPropertyChanged, IDataErrorInfo {
    private double _latitud;
    private double _longitud;
    private double? _altitud;
    private string? _precision;

    public CoordenadasGeograficas() { }

    public CoordenadasGeograficas(double latitud, double longitud, double? altitud = null) {
        Latitud = latitud;
        Longitud = longitud;
        Altitud = altitud;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Latitud en grados decimales (-90 a 90)
    /// </summary>
    public double Latitud {
        get => _latitud;
        set {
            if (value < -90 || value > 90)
                throw new ArgumentOutOfRangeException(nameof(Latitud), "La latitud debe estar entre -90 y 90 grados");

            if (_latitud != value) {
                _latitud = value;
                OnPropertyChanged(nameof(Latitud));
            }
        }
    }

    /// <summary>
    /// Longitud en grados decimales (-180 a 180)
    /// </summary>
    public double Longitud {
        get => _longitud;
        set {
            if (value < -180 || value > 180)
                throw new ArgumentOutOfRangeException(nameof(Longitud), "La longitud debe estar entre -180 y 180 grados");

            if (_longitud != value) {
                _longitud = value;
                OnPropertyChanged(nameof(Longitud));
            }
        }
    }

    /// <summary>
    /// Altitud en metros sobre el nivel del mar (opcional)
    /// </summary>
    public double? Altitud {
        get => _altitud;
        set {
            if (_altitud != value) {
                _altitud = value;
                OnPropertyChanged(nameof(Altitud));
            }
        }
    }

    /// <summary>
    /// Precisión del geoposicionamiento (ej: "10m", "alta", "baja")
    /// </summary>
    public string? Precision {
        get => _precision;
        set {
            if (_precision != value) {
                _precision = value;
                OnPropertyChanged(nameof(Precision));
            }
        }
    }

    /// <summary>
    /// Indica si las coordenadas son válidas
    /// </summary>
    public bool EsValido => this["Latitud"] == null && this["Longitud"] == null;

    /// <summary>
    /// Calcula la distancia en kilómetros hasta otras coordenadas (fórmula de Haversine)
    /// </summary>
    public double CalcularDistancia(CoordenadasGeograficas otrasCoordenadas) {
        if (otrasCoordenadas == null)
            throw new ArgumentNullException(nameof(otrasCoordenadas));

        var radioTierra = 6371; // Radio de la Tierra en km
        var dLat = ToRadians(otrasCoordenadas.Latitud - Latitud);
        var dLon = ToRadians(otrasCoordenadas.Longitud - Longitud);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(Latitud)) * Math.Cos(ToRadians(otrasCoordenadas.Latitud)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return radioTierra * c;
    }

    /// <summary>
    /// Convierte a formato de grados, minutos y segundos
    /// </summary>
    public string ToGmsFormat() {
        return $"{ToGms(Latitud, 'N', 'S')} {ToGms(Longitud, 'E', 'O')}";
    }

    /// <summary>
    /// Representación en string de las coordenadas
    /// </summary>
    public override string ToString() {
        return Altitud.HasValue
            ? $"{Latitud.ToString("F6", CultureInfo.InvariantCulture)}, {Longitud.ToString("F6", CultureInfo.InvariantCulture)}, {Altitud.Value}m"
            : $"{Latitud.ToString("F6", CultureInfo.InvariantCulture)}, {Longitud.ToString("F6", CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Parsea un string en formato lat,long,alt a CoordenadasGeograficas
    /// </summary>
    public static CoordenadasGeograficas Parse(string coordenadasString) {
        if (string.IsNullOrWhiteSpace(coordenadasString))
            throw new ArgumentException("El string de coordenadas no puede estar vacío");

        var partes = coordenadasString.Split(',');

        if (partes.Length < 2)
            throw new FormatException("Formato de coordenadas inválido");

        var latitud = double.Parse(partes[0].Trim(), CultureInfo.InvariantCulture);
        var longitud = double.Parse(partes[1].Trim(), CultureInfo.InvariantCulture);
        double? altitud = partes.Length > 2 ? double.Parse(partes[2].Trim(), CultureInfo.InvariantCulture) : (double?)null;

        return new CoordenadasGeograficas(latitud, longitud, altitud);
    }

    public object Clone() {
        return new CoordenadasGeograficas(Latitud, Longitud, Altitud) {
            Precision = Precision
        };
    }

    public string? this[string columnName] {
        get {
            switch (columnName) {
                case nameof(Latitud):
                    return Latitud < -90 || Latitud > 90 ? "Latitud fuera de rango (-90 a 90)" : null;
                case nameof(Longitud):
                    return Longitud < -180 || Longitud > 180 ? "Longitud fuera de rango (-180 a 180)" : null;
                default:
                    return null;
            }
        }
    }

    public string? Error => this[nameof(Latitud)] ?? this[nameof(Longitud)];

    private double ToRadians(double angle) {
        return Math.PI * angle / 180.0;
    }

    private string ToGms(double decimalDegrees, char positiveIndicator, char negativeIndicator) {
        var indicator = decimalDegrees >= 0 ? positiveIndicator : negativeIndicator;
        var absDegrees = Math.Abs(decimalDegrees);
        var degrees = Math.Floor(absDegrees);
        var minutes = Math.Floor((absDegrees - degrees) * 60);
        var seconds = ((absDegrees - degrees - minutes / 60) * 3600);

        return $"{degrees}° {minutes}' {seconds:F2}\" {indicator}";
    }

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
