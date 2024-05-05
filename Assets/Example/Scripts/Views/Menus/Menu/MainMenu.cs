using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class MainMenu : View, IView<MenuView>
    {
        public MenuView ViewType => MenuView.MainMenu;

        protected override void UpdateView()
        {
            
        }
    }
}