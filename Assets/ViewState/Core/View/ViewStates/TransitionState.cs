using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using State;

namespace View
{
    public class TransitionState : IState
    {
        public Animation Animation;
        public AnimationClip AnimationClip;
        public Action OnTransitionCompleteAction = delegate { };

        public TransitionState(Animation animation)
        {
            Animation = animation;
        }

        public void EnterState()
        {
            if (AnimationClip == null)
            {
                return;
            }

            Animation.clip = AnimationClip;
            Animation.Play(AnimationClip.name);
        }

        public void UpdateState()
        {
            if (AnimationClip == null)
            {
                TransitionComplete();
                return;
            }

            Debug.Assert(AnimationClip != null, "Animation clip is null!");
            Debug.Assert(Animation[AnimationClip.name] != null, "Animation clip not assigned!");

            if (!Animation.isPlaying || Animation[AnimationClip.name].normalizedTime >= 1.0f)
            {
                TransitionComplete();
            }
        }

        public void ExitState()
        {

        }

        public void Interupt()
        {
            TransitionComplete();
        }

        private void TransitionComplete()
        {
            if (AnimationClip != null)
            {
                Animation[AnimationClip.name].normalizedTime = 1.0f;
            }

            OnTransitionCompleteAction();
        }
    }
}