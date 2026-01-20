using Microsoft.Web.WebView2.WinForms;

namespace TiendaSimple {
    public partial class Form1 : Form {
        private const string BASE_URL = "https://advanceerp.free.nf/admin/crud.php";
        private const string TOKEN = "tu_token_secreto_123";

        private WebView2 webView;

        public Form1() {
            InitializeComponent();
            CreateWebView();
        }

        private void CreateWebView() {
            webView = new WebView2();
            webView.Dock = DockStyle.None;
            this.Controls.Add(webView);

            // Inicializar WebView2
            _ = InitializeWebViewAsync();
        }

        private async Task InitializeWebViewAsync() {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.Navigate("about:blank");
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            // Botón para crear categoría
            var btnCrearCategoria = new Button {
                Text = "🆕 Crear Categoría Ejemplo",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(180, 30)
            };
            btnCrearCategoria.Click += async (s, ev) => await CrearCategoriaEjemploAsync();
            this.Controls.Add(btnCrearCategoria);

            // Botón para crear producto (ahora debería funcionar)
            var btnCrearProducto = new Button {
                Text = "📦 Crear Producto",
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(180, 30)
            };
            btnCrearProducto.Click += async (s, ev) => await CreateProductoAsync();
            this.Controls.Add(btnCrearProducto);
        }

        private async Task CrearCategoriaEjemploAsync() {
            try {
                // Navegar al formulario de categorías
                webView.CoreWebView2.Navigate("https://advanceerp.free.nf/admin/categorias.php");
                await Task.Delay(3000); // Esperar a que cargue completamente

                // Rellenar el formulario y hacer clic en el botón
                string script = @"
                    var campoNombre = document.querySelector('input[name=""nombre_categoria""]');
                    if (campoNombre) {
                        campoNombre.value = 'Electrónica';
                    }
            
                    var botonCrear = document.querySelector('button[name=""crear_categoria""]');
                    if (botonCrear) {
                        botonCrear.click();
                    } else {
                        var botones = document.querySelectorAll('button');
                        for (var i = 0; i < botones.length; i++) {
                            if (botones[i].textContent.includes('Crear') || 
                                botones[i].textContent.includes('crear')) {
                                botones[i].click();
                                break;
                            }
                        }
                    }
                ";

                await webView.ExecuteScriptAsync(script);

                // Esperar a que se procese la creación
                await Task.Delay(3000);

                // Extraer el resultado (lista actualizada de categorías)
                string scriptResultado = @"
                    (function() {
                        var el = document.getElementById('data');
                        return el ? el.innerText : JSON.stringify({ error: 'No se pudo obtener datos' });
                    })()";

                string resultado = await webView.ExecuteScriptAsync(scriptResultado);
                string jsonLimpio = resultado.Trim('"').Replace("\\\"", "\"");

                MessageBox.Show($"✅ Categoría creada.\nCategorías actuales:\n{jsonLimpio}");
            } catch (Exception ex) {
                MessageBox.Show($"❌ Error al crear categoría:\n{ex.Message}");
            }
        }

        private async Task CreateProductoAsync() {
            try {
                // Navegar al formulario real
                webView.CoreWebView2.Navigate("https://advanceerp.free.nf/admin/productos.php");
                await Task.Delay(3000); // Esperar a que cargue

                // Rellenar campos y hacer clic
                string script = @"
                    document.querySelector('input[name=""nombre_producto""]').value = 'Producto Prueba C#';
                    document.querySelector('textarea[name=""descripcion_producto""]').value = 'Creado desde app';
                    document.querySelector('input[name=""precio_producto""]').value = '89.99';
                    document.querySelector('input[name=""stock_producto""]').value = '5';
                    document.querySelector('select[name=""categoria_id""]').value = '1';
                    document.querySelector('button[name=""crear_producto""]').click();
                ";

                await webView.ExecuteScriptAsync(script);
                await Task.Delay(3000);

                // Extraer resultado
                string getResult = @"
                    (function() {
                        var el = document.getElementById('data');
                        return el ? el.innerText : JSON.stringify({ error: 'No data' });
                    })()";

                string result = await webView.ExecuteScriptAsync(getResult);
                string clean = result.Trim('"').Replace("\\\"", "\"");
                MessageBox.Show($"Resultado:\n{clean}");
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}

