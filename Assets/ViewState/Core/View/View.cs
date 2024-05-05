using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

namespace View
{
    [RequireComponent(typeof(Animation))]
    public abstract class View : MonoBehaviour, IState
    {
        public bool IsShowing => _stateMachine.CurrentState == _onState;
        public bool IsTransitioning => _stateMachine.CurrentState == _transitionOnState || _stateMachine.CurrentState == _transitionOffState;

        public event Action OnTransitionOnCompleted = delegate { };
        public event Action OnTransitionOffCompleted = delegate { };

        [SerializeField]
        protected GameObject _child;

        protected StaticState _offState;
        protected TransitionState _transitionOnState;
        protected StaticState _onState;
        protected TransitionState _transitionOffState;

        private StateMachine _stateMachine;
        private Animation _animation;

        public virtual void Init()
        {
            _animation = GetComponent<Animation>();

            _offState = new StaticState();
            _offState.OnEnterAction += DisableView;
            _offState.OnEnterAction += OnTransitionOffCompleted;
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

        private void TransitionOnComplete()
        {
            _stateMachine.StateExitComplete();
            _stateMachine.ChangeState(_onState);
        }

        private void TransitionOffComplete()
        {
            _stateMachine.StateExitComplete();
            _stateMachine.ChangeState(_offState);
        }

        private void TryAddAnimationClip(AnimationClip clip)
        {
            if (_animation.GetClip(clip.name) == null)
            {
                _animation.AddClip(clip, clip.name);
            }
        }
    }
}