using System.Collections;
using System.Collections.Generic;

namespace Jwho303.ViewState.Example
{
    public class NonePopup : View, IView<PopupView>
    {
        public PopupView ViewType => PopupView.None;

        protected override void UpdateView()
        {
            
        }
    }
}