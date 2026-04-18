using aDVancePOS.Mobile.Modelos;

using Android.Views;

namespace aDVancePOS.Mobile.Adaptadores {
    internal class VentaEsperaAdapter : BaseAdapter<VentaExportacion> {
        private readonly Activity _ctx;
        private readonly List<VentaExportacion> _ventas;

        public VentaEsperaAdapter(Activity ctx, List<VentaExportacion> ventas) {
            _ctx = ctx;
            _ventas = ventas;
        }

        public override int Count => _ventas.Count;
        public override VentaExportacion this[int i] => _ventas[i];
        public override long GetItemId(int pos) => pos;

        public override View GetView(int pos, View? cv, ViewGroup parent) {
            var view = cv ?? _ctx.LayoutInflater
                .Inflate(Resource.Layout.item_venta_espera, parent, false)!;

            var venta = _ventas[pos];

            decimal pagado = venta.Pagos.Sum(p => p.MontoPagado);
            decimal pendiente = venta.ImporteTotal - pagado;

            view.FindViewById<TextView>(Resource.Id.lblTicketEspera)!.Text = venta.NumeroTicket;
            view.FindViewById<TextView>(Resource.Id.lblTotalEspera)!.Text = $"$ {venta.ImporteTotal:N2}";
            view.FindViewById<TextView>(Resource.Id.lblFechaEspera)!.Text = venta.FechaVenta.ToLocalTime().ToString("HH:mm  dd/MM");
            view.FindViewById<TextView>(Resource.Id.lblPendienteEspera)!.Text = pendiente > 0 ? $"Pendiente: $ {pendiente:N2}" : "Pagado";

            return view;
        }
    }
}
