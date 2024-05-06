using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SettingsMenu : View, IView<MenuView>
    {
        public MenuView ViewType => MenuView.SettingsMenu;

        [SerializeField]
        private UIController _uiController; //HACK for demo purposes

        [SerializeField]
        private Button _homeButton;

        [SerializeField]
        private Button _mainMenuButton;

        [SerializeField]
        private Button _aboutButton;

        [SerializeField]
        private Button _popupButton;

        public override void Init()
        {
            base.Init();

            _homeButton.onClick.AddListener(() => { _uiController.HideMenu(); });
            _mainMenuButton.onClick.AddListener(() => { _uiController.ShowMenu(MenuView.MainMenu, ViewTransition.SlideLeft); });
            _aboutButton.onClick.AddListener(() => { _uiController.ShowMenu(MenuView.AboutMenu, ViewTransition.SlideRight); });
            _popupButton.onClick.AddListener(() => { _uiController.ShowPopup(PopupView.LoadingBar, ViewTransition.Bounce); });
        }

        protected override void UpdateView()
        {
            
        }
    }
}