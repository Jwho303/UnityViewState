using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jwho303.ViewState.Example
{
    public class MainMenu : View, IView<MenuView>
    {
        public MenuView ViewType => MenuView.MainMenu;

        [SerializeField]
        private UIController _uiController; //HACK for demo purposes

        [SerializeField]
        private Button _homeButton;

        [SerializeField]
        private Button _settingsButton;

        [SerializeField]
        private Button _aboutButton;

        public override void Init()
        {
            base.Init();

            _homeButton.onClick.AddListener(() => { _uiController.HideMenu();});
            _settingsButton.onClick.AddListener(() => { _uiController.ShowMenu(MenuView.SettingsMenu, ViewTransition.SlideRight); });
            _aboutButton.onClick.AddListener(() => { _uiController.ShowMenu(MenuView.AboutMenu, ViewTransition.SlideRight); });
        }

        protected override void UpdateView()
        {
            
        }
    }
}