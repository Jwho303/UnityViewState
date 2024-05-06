using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jwho303.ViewState.Example
{
    public class NoneMenu : View, IView<MenuView>
    {
        public MenuView ViewType => MenuView.None;

        protected override void UpdateView()
        {
            
        }
    }
}