

using aDVanceERP.Core.Mensajes.Utiles;

using System.Data;
using System.Text.RegularExpressions;

namespace aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion;

public partial class VistaRegistroGastoDinamico : Form {
    private bool _modoEdicion;
    private ValidadorFormulas _validadorFormulas;

    public VistaRegistroGastoDinamico(List<string> conceptosValidos) {
        InitializeComponent();
        Inicializar();

        // Reemplazar el caracter espacio
        var variables = new string[conceptosValidos.Count];

        for (int i = 0; i < conceptosValidos.Count; i++) {
            var conceptoSplit = conceptosValidos[i].Split(" ");
            var conceptoSinEspacios = string.Join("", conceptoSplit.Select(
                    palabra => char.ToUpper(palabra[0]) + palabra.Substring(1).ToLower())
                );

            variables[i] = conceptoSinEspacios.Replace(" ", string.Empty).Replace("_", string.Empty).Replace("-", string.Empty);
        }

        _fieldConceptosDisponibles.Items.Clear();
        _fieldConceptosDisponibles.Items.AddRange(variables);
        _validadorFormulas = new ValidadorFormulas(variables.ToList());
    }

    public bool Habilitada {
        get => Enabled;
        set => Enabled = value;
    }

    public Point Coordenadas {
        get => Location;
        set => Location = value;
    }

    public Size Dimensiones {
        get => Size;
        set => Size = value;
    }

    public bool ModoEdicionDatos {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar contacto" : "Registrar contacto";
            _modoEdicion = value;
        }
    }

    public string Ecuacion {
        get => fieldEcuacion.Text.Trim();
        set {
            fieldEcuacion.Text = value;
            ValidarFormula();
        }
    }

    public event EventHandler? RegistrarDatos;

    public void Inicializar() {
        // Eventos
        _fieldConceptosDisponibles.DoubleClick += delegate {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && !"(+-*/".Contains(fieldEcuacion.Text.Last())) {
                if (fieldEcuacion.Text.EndsWith(" "))
                    fieldEcuacion.Text = fieldEcuacion.Text.TrimEnd();

                return;
            }
            if (!string.IsNullOrEmpty(fieldEcuacion.Text))
                fieldEcuacion.Text += " ";

            fieldEcuacion.Text += _fieldConceptosDisponibles.SelectedItem?.ToString();          
        };
        btnSuma.Click += delegate (object? sender, EventArgs args) {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && (char.IsLetterOrDigit(fieldEcuacion.Text.Last()) || ")".Contains(fieldEcuacion.Text.Last())))
                fieldEcuacion.Text += " +";
            else if (string.IsNullOrEmpty(fieldEcuacion.Text))
                fieldEcuacion.Text = " +";
        };
        btnResta.Click += delegate (object? sender, EventArgs args) {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && (char.IsLetterOrDigit(fieldEcuacion.Text.Last()) || ")".Contains(fieldEcuacion.Text.Last())))
                fieldEcuacion.Text += " -";
            else if (string.IsNullOrEmpty(fieldEcuacion.Text))
                fieldEcuacion.Text = " -";
        };
        btnMultiplicacion.Click += delegate (object? sender, EventArgs args) {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && (char.IsLetterOrDigit(fieldEcuacion.Text.Last()) || ")".Contains(fieldEcuacion.Text.Last())))
                fieldEcuacion.Text += " *";
            else if (string.IsNullOrEmpty(fieldEcuacion.Text))
                fieldEcuacion.Text = " *";
        };
        btnDivision.Click += delegate (object? sender, EventArgs args) {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && (char.IsLetterOrDigit(fieldEcuacion.Text.Last()) || ")".Contains(fieldEcuacion.Text.Last())))
                fieldEcuacion.Text += " /";
            else if (string.IsNullOrEmpty(fieldEcuacion.Text))
                fieldEcuacion.Text = " /";
        };
        btnInsertarConstante.Click += delegate (object? sender, EventArgs args) {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && "(+-*/".Contains(fieldEcuacion.Text.Last()))
                fieldEcuacion.Text += $" {fieldConstante.Text}";
            else if (string.IsNullOrEmpty(fieldEcuacion.Text))
               fieldEcuacion.Text = $" {fieldConstante.Text}";
        };
        btnParentesisIzquierdo.Click += delegate (object? sender, EventArgs args) {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && "+-*/".Contains(fieldEcuacion.Text.Last()))
                fieldEcuacion.Text += " (";
            else if (string.IsNullOrEmpty(fieldEcuacion.Text))
                fieldEcuacion.Text = " (";
        };
        btnParentesisDerecho.Click += delegate (object? sender, EventArgs args) {
            if (!string.IsNullOrEmpty(fieldEcuacion.Text) && char.IsLetterOrDigit(fieldEcuacion.Text.Last()))
                fieldEcuacion.Text += " )";
            else if (string.IsNullOrEmpty(fieldEcuacion.Text))
                fieldEcuacion.Text = " )";
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ValidarFormula()) {
                RegistrarDatos?.Invoke(sender, args);
                Close();
            }
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) {
            Close();
        };
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        ModoEdicionDatos = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private bool ValidarFormula() {
        bool validacion = _validadorFormulas.ValidarFormula(fieldEcuacion.Text, out string errorValidacion);

        if (validacion) {
            fieldEcuacion.BorderColor = Color.ForestGreen;
            fieldEcuacion.HoverState.BorderColor = Color.LightGreen;

        } else {
            fieldEcuacion.BorderColor = Color.LightCoral;
            fieldEcuacion.HoverState.BorderColor = Color.IndianRed;

            CentroNotificaciones.Mostrar($"Error en la fórmula : {errorValidacion}");
        }

        return validacion;
    }
}

public class ValidadorFormulas {
    private readonly List<string> _conceptosValidos;

    public ValidadorFormulas(List<string> conceptosValidos) {
        _conceptosValidos = conceptosValidos;
    }

    public bool ValidarFormula(string formula, out string error) {
        error = null;

        // 1. Verificar caracteres permitidos
        var regex = new Regex(@"^[a-zA-Z0-9+\-*\/\s().,]+$");
        if (!regex.IsMatch(formula)) {
            error = "La fórmula contiene caracteres no permitidos";
            return false;
        }

        // 2. Extraer variables (nombres de conceptos)
        var variables = ExtraerVariables(formula);

        // 3. Verificar que todas las variables sean conceptos válidos
        foreach (var variable in variables) {
            if (!_conceptosValidos.Contains(variable)) {
                error = $"El concepto '{variable}' no existe en el sistema";
                return false;
            }
        }

        // 4. Verificar sintaxis matemática (opcional: podrías usar un evaluador real)
        try {
            // Reemplazar variables con valores de prueba para verificar sintaxis
            var formulaPrueba = formula;
            foreach (var variable in variables) {
                formulaPrueba = formulaPrueba.Replace(variable, "1");
            }

            // Usar DataTable.Compute para verificar sintaxis
            new DataTable().Compute(formulaPrueba, null);
        } catch (Exception ex) {
            error = $"Error en la sintaxis de la fórmula: {ex.Message}";
            return false;
        }

        return true;
    }

    private List<string> ExtraerVariables(string formula) {
        // Eliminar operadores y números
        var sinOperadores = Regex.Replace(formula, @"[\d+\-*\/\s().,]", " ");

        // Extraer palabras (nombres de variables)
        return sinOperadores.Split([" "], StringSplitOptions.RemoveEmptyEntries)
                            .Distinct()
                            .ToList();
    }
}
