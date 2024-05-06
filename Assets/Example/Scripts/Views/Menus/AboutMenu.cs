using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace View
{
    public class AboutMenu : View, IView<MenuView>
    {
        public MenuView ViewType => MenuView.AboutMenu;

        [SerializeField]
        private UIController _uiController; //HACK for demo purposes

        [SerializeField]
        private Button _homeButton;

        [SerializeField]
        private Button _settingsButton;

        [SerializeField]
        private Button _mainMenuButton;

        [SerializeField]
        private TextMeshProUGUI _textBox;

        private int _count = 0;

        public override void Init()
        {
            base.Init();

            _homeButton.onClick.AddListener(() => { _uiController.HideMenu(); });
            _settingsButton.onClick.AddListener(() => { _uiController.ShowMenu(MenuView.SettingsMenu, ViewTransition.SlideRight); });
            _mainMenuButton.onClick.AddListener(() => { _uiController.ShowMenu(MenuView.MainMenu, ViewTransition.SlideLeft); });

            _offState.OnExitAction += IncrementMenuOpenCount;
        }

        protected override void UpdateView()
        {
            
        }

        private void IncrementMenuOpenCount()
        {
            _count++;
            string s = $"Hello. Thanks for trying out this demo.\nYou have opened this menu {_count} times";
            _textBox.SetText(s);
        }
    }
}