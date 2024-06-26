using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jwho303.ViewState
{
    [RequireComponent(typeof(Animation))]
    public abstract class View : MonoBehaviour, IState
    {
        public bool IsShowing => _stateMachine.CurrentState == _onState;
        public bool IsTransitioning => _stateMachine.CurrentState == _transitionOnState || _stateMachine.CurrentState == _transitionOffState;

        public Action OnTransitionOnCompleted { get; set; } = delegate { };
        public Action OnTransitionOffCompleted { get; set; } = delegate { };

        [SerializeField]
        protected GameObject _child;

        protected StaticState _offState;
        protected TransitionState _transitionOnState;
        protected StaticState _onState;
        protected TransitionState _transitionOffState;

        protected StateMachine _stateMachine;
        protected RectTransform _rectTransform;
        protected Animation _animation;

        private Vector3 _initialPosition;

        public virtual void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
            _animation = GetComponent<Animation>();
            _initialPosition = _rectTransform.anchoredPosition;

            _offState = new StaticState();
            _offState.OnEnterAction += OnTransitionOffCompleted;
            _offState.OnEnterAction += DisableView;
            _offState.OnEnterAction += ReturnToInitialPosition;
            _offState.OnExitAction += EnableView;

            _stateMachine = new StateMachine(_offState);

            _transitionOnState = new TransitionState(_animation);
            _transitionOnState.OnTransitionCompleteAction += TransitionOnComplete;

            _onState = new StaticState();
            _onState.OnEnterAction += OnTransitionOnCompleted;
            _onState.OnUpdateAction += UpdateView;

            _transitionOffState = new TransitionState(_animation);
            _transitionOffState.OnTransitionCompleteAction += TransitionOffComplete;
        }

        public void EnterState() => _stateMachine.ChangeState(_transitionOnState);
        public void ExitState() => _stateMachine.ChangeState(_transitionOffState);
        public void UpdateState() => _stateMachine.Update();
        public void Interupt() => _stateMachine.CurrentState.Interupt();

        public void SetAnimationClips(AnimationClip onClip, AnimationClip offClip)
        {
            TryAddAnimationClip(onClip);
            TryAddAnimationClip(offClip);

            _transitionOnState.AnimationClip = onClip;
            _transitionOffState.AnimationClip = offClip;
        }

        protected virtual void EnableView() => _child.SetActive(true);
        protected abstract void UpdateView();
        protected virtual void DisableView() => _child.SetActive(false);

        protected void TransitionOnComplete()
        {
            _stateMachine.StateExitComplete();
            _stateMachine.ChangeState(_onState);
        }

        protected void TransitionOffComplete()
        {
            _stateMachine.StateExitComplete();
            _stateMachine.ChangeState(_offState);
        }

        protected void TryAddAnimationClip(AnimationClip clip)
        {
            if (_animation.GetClip(clip.name) == null)
            {
                _animation.AddClip(clip, clip.name);
            }
        }

        protected void ReturnToInitialPosition()
        {
            _rectTransform.anchoredPosition = _initialPosition;
        }
    }
}