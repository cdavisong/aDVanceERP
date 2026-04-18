using Android.Content;
using Android.Views;

namespace aDVancePOS.Mobile.Vistas {
    public class MaxHeightScrollView : ScrollView {
        private readonly int _maxHeightPx;

        public MaxHeightScrollView(Context context, int maxHeightPx) : base(context) {
            _maxHeightPx = maxHeightPx;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
            if (_maxHeightPx > 0) {
                int atMost = View.MeasureSpec.MakeMeasureSpec(_maxHeightPx, MeasureSpecMode.AtMost);
                base.OnMeasure(widthMeasureSpec, atMost);
            } else {
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            }
        }
    }
}