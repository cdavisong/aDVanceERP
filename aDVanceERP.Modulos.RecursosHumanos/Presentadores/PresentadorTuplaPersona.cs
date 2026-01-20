using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorTuplaPersona : PresentadorVistaTupla<IVistaTuplaPersona, Persona> {
    public PresentadorTuplaPersona(IVistaTuplaPersona vista, Persona objeto) : base(vista, objeto) { }
}