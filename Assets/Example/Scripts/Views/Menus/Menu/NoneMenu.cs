using System;
using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

namespace View
{
    public class NoneMenu : View, IView<MenuView>
    {
        public MenuView ViewType => MenuView.None;

        protected override void UpdateView()
        {
            
        }
    }
}