using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class LoadingBarPopup : View, IView<PopupView>
    {
        public PopupView ViewType => PopupView.LoadingBar;

        [SerializeField]
        private UIController _uiController; //HACK for demo purposes

        [SerializeField]
        private Image _loadingBarImage;

        private float _fillDuration = 2f;
        private float _fillTimer = 0f;
        private float _fillAmount = 0f;

        public override void Init()
        {
            base.Init();

            _offState.OnExitAction += ResetLoadingbar;

            _onState.OnEnterAction += ()=> { _fillTimer = Time.time + _fillDuration; };
        }

        private void ResetLoadingbar()
        {
            _loadingBarImage.fillAmount = 0f;
        }

        private void UpdateLoadingBar()
        {
            _fillAmount = 1f - ((_fillTimer - Time.time) / _fillDuration);
            _loadingBarImage.fillAmount = _fillAmount;
        }

        protected override void UpdateView()
        {
            UpdateLoadingBar();

            if (_fillAmount >= 1f)
            {
                _uiController.ShowPopup(PopupView.Confirmation, ViewTransition.Bounce, false);
            }
        }
    }
}