using System.Drawing.Drawing2D;

namespace aDVanceERP.Core.Utiles {
    public static class UtilesManipulacionImagenes {
        public static Image ObtenerRecorteImagen100x100(this Image original) {
            // Calcular las proporciones
            float originalWidth = original.Width;
            float originalHeight = original.Height;
            var ratio = Math.Min(100 / originalWidth, 100 / originalHeight);

            // Calcular el nuevo tamaño manteniendo las proporciones
            var newWidth = (int) (originalWidth * ratio);
            var newHeight = (int) (originalHeight * ratio);

            // Crear un nuevo bitmap con las dimensiones deseadas
            var resizedImage = new Bitmap(100, 100);

            // Usar Graphics para dibujar la imagen redimensionada y centrada
            using (var graphics = Graphics.FromImage(resizedImage)) {
                // Configurar la calidad de la interpolación
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                // Rellenar el fondo con un color (opcional)
                graphics.Clear(Color.White);

                // Calcular la posición para centrar la imagen
                var posX = (100 - newWidth) / 2;
                var posY = (100 - newHeight) / 2;

                // Dibujar la imagen redimensionada
                graphics.DrawImage(original, posX, posY, newWidth, newHeight);
            }

            // Retornar la imagen redimensionada
            return resizedImage;
        }
    }
}
