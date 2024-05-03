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

        public event Action OnTransitionOnCompleted;
        public event Action OnTransitionOffCompleted;

        private StateMachine _stateMachine;

        private StaticState _offState;
        private TransitionState _transitionOnState;
        private StaticState _onState;
        private TransitionState _transitionOffState;

        [SerializeField]
        protected GameObject _child;

        public virtual void Init()
        {
            Debug.Log("Init");
            Animation animation = GetComponent<Animation>();

            _offState = new StaticState();
            _offState.OnEnterAction += DisableView;
            _offState.OnEnterAction += OnTransitionOffCompleted;
            _offState.OnExitAction += EnableView;

            _stateMachine = new StateMachine(_offState);

            _transitionOnState = new TransitionState(animation);
            _transitionOnState.OnAnimationCompleteAction += OnTransitionOnComplete;

            _onState = new StaticState();
            _onState.OnEnterAction += OnTransitionOnCompleted;
            _onState.OnUpdateAction += UpdateView;

            _transitionOffState = new TransitionState(animation);
            _transitionOffState.OnAnimationCompleteAction += OnTransitionOffComplete;
        }

        public void EnterState() => _stateMachine.ChangeState(_transitionOnState);
        public void ExitState() => _stateMachine.ChangeState(_transitionOffState);
        public void UpdateState() => _stateMachine.Update();
        [ContextMenu("Interupt")]
        public void Interupt() => _stateMachine.CurrentState.Interupt();

        public virtual void EnableView() => _child.SetActive(true);
        public abstract void UpdateView();
        public virtual void DisableView() => _child.SetActive(false);

        public void SetAnimationClips(AnimationClip onClip, AnimationClip offClip)
        {
            _transitionOnState.AnimationClip = onClip;
            _transitionOffState.AnimationClip = offClip;
        }

        private void OnTransitionOnComplete()
        {
            Debug.Log("[ViewStateMachine] OnTransitionOnComplete");
            _stateMachine.StateExitComplete();
            _stateMachine.ChangeState(_onState);
        }

        private void OnTransitionOffComplete()
        {
            Debug.Log("[ViewStateMachine] OnTransitionOffComplete");
            _stateMachine.StateExitComplete();
            _stateMachine.ChangeState(_offState);
        }
    }
}