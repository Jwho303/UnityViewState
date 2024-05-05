using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class SettingsMenu : View, IView<MenuView>
    {
        public MenuView ViewType => MenuView.SettingsMenu;

        protected override void UpdateView()
        {
            
        }
    }
}