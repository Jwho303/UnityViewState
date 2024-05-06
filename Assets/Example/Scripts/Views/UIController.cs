using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using State;
using System;

namespace View
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private MenuView _targetView = MenuView.MainMenu;
        [SerializeField]
        private TransitionData _transitionData;

        [SerializeField]
        private Button _startButton;

        private ViewController<MenuView> _menuController;
        private ViewController<PopupView> _popupController;

        private void Start()
        {
            _menuController = CreateViewController(MenuView.None);
            _popupController = CreateViewController(PopupView.None);

            _startButton.onClick.AddListener(() => { ShowMenu(MenuView.MainMenu, ViewTransition.Bounce); });
        }

        private ViewController<TEnum> CreateViewController<TEnum>(TEnum startState) where TEnum : struct
        {
            IView<TEnum>[] views = GetComponentsInChildren<IView<TEnum>>(true);

            return new ViewController<TEnum>(views, startState);
        }

        public void ShowMenu(MenuView view, ViewTransition transition, bool overlap = true)
        {
            TransitionDataSet transitionDataSet = _transitionData.GetTransition(transition);
            _menuController.Show(view, transitionDataSet.OnClip, transitionDataSet.OffClip, overlap);
        }

        public void HideMenu()
        {
            TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.Bounce);
            _menuController.Show(MenuView.None, transitionDataSet.OnClip, transitionDataSet.OffClip, true);
        }

        public void ShowPopup(PopupView view, ViewTransition transition, bool overlap = true)
        {
            TransitionDataSet transitionDataSet = _transitionData.GetTransition(transition);
            _popupController.Show(view, transitionDataSet.OnClip, transitionDataSet.OffClip, overlap);
        }

        public void HidePopup()
        {
            TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.Bounce);
            _popupController.Show(PopupView.None, transitionDataSet.OnClip, transitionDataSet.OffClip, true);
        }

        public void Update()
        {
            _menuController.UpdateViewController();
            _popupController.UpdateViewController();

            //KeyPressSpamTest(); //For demo purposes
        }

        private void KeyPressSpamTest()
        {
            //Slide Left
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.SlideLeft);
                _menuController.Show(MenuView.None, transitionDataSet.OnClip, transitionDataSet.OffClip);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.SlideLeft);
                _menuController.Show(MenuView.MainMenu, transitionDataSet.OnClip, transitionDataSet.OffClip);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.SlideLeft);
                _menuController.Show(MenuView.SettingsMenu, transitionDataSet.OnClip, transitionDataSet.OffClip);
            }

            //Slide Right
            if (Input.GetKeyDown(KeyCode.A))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.SlideRight);
                _menuController.Show(MenuView.None, transitionDataSet.OnClip, transitionDataSet.OffClip);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.SlideRight);
                _menuController.Show(MenuView.MainMenu, transitionDataSet.OnClip, transitionDataSet.OffClip);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.SlideRight);
                _menuController.Show(MenuView.SettingsMenu, transitionDataSet.OnClip, transitionDataSet.OffClip);
            }

            //Bounce
            if (Input.GetKeyDown(KeyCode.Z))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.Bounce);
                _menuController.Show(MenuView.None, transitionDataSet.OnClip, transitionDataSet.OffClip, false);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.Bounce);
                _menuController.Show(MenuView.MainMenu, transitionDataSet.OnClip, transitionDataSet.OffClip, false);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.Bounce);
                _menuController.Show(MenuView.SettingsMenu, transitionDataSet.OnClip, transitionDataSet.OffClip, false);
            }
        }
    }

}