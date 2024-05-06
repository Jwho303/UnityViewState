using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class NonePopup : View, IView<PopupView>
    {
        public PopupView ViewType => PopupView.None;

        protected override void UpdateView()
        {
            
        }
    }
}