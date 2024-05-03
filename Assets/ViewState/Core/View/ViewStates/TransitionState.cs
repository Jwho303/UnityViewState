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
        public Action OnAnimationCompleteAction = delegate { };

        public TransitionState(Animation animation)
        {
            Animation = animation;
        }

        public void EnterState()
        {
            if (Animation.GetClip(Animation.name) == null)
            {
                Animation.AddClip(AnimationClip, AnimationClip.name);
            }

            Animation.clip = AnimationClip;
            Animation.Play(AnimationClip.name);
        }

        public void UpdateState()
        {
            Debug.Assert(Animation != null, "Animation is null!");
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
            Animation[AnimationClip.name].normalizedTime = 1.0f;
            OnAnimationCompleteAction();
        }
    }
}