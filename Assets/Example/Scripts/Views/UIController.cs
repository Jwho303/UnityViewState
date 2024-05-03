using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private ViewController<MenuView> _menuController;

        public void Start()
        {
            _menuController = CreateViewController<MenuView>();
        }

        private ViewController<TEnum> CreateViewController<TEnum>() where TEnum : struct
        {
            IView<TEnum>[] views = GetComponentsInChildren<IView<TEnum>>(true);
            Debug.Log($"[UIController] Found ({views.Length}) {typeof(TEnum).Name} views");

            return new ViewController<TEnum>(views);
        }

        public void Update()
        {
            _menuController.UpdateViewController();

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
        }

        [ContextMenu("Show")]
        public void ShowTargetView()
        {
            _menuController.Show(_targetView);
        }

        [ContextMenu("Show with Animation")]
        public void ShowTargetViewWithAnimation()
        {
            TransitionDataSet transitionDataSet = _transitionData.GetTransition(ViewTransition.SlideLeft);
            _menuController.Show(_targetView, transitionDataSet.OnClip, transitionDataSet.OffClip);
        }

        [ContextMenu("Hide")]
        public void Hide()
        {
            _menuController.Show(MenuView.None);
        }
    }

}