using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Infraestructura.Extensiones.BD;

public static class LectorDatosExt {
    public static bool HasColumn(this MySqlDataReader reader, string columnName) {
        for (int i = 0; i < reader.FieldCount; i++) {
            if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
}
