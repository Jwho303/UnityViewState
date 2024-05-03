using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public enum ViewTransition
    {
        Bounce,
        SlideLeft,
        SlideRight,
        SlideUp,
        SlideDown,
        Fade,
    }

    [System.Serializable]
    public struct TransitionDataSet
    {
        public ViewTransition TransitionKey;
        public AnimationClip OnClip;
        public AnimationClip OffClip;
        public bool Overlap;
    }

    [CreateAssetMenu(fileName = "TransitionData", menuName = "Data/Collection/TransitionData")]
    public class TransitionData : ScriptableObject
    {
        public TransitionDataSet[] TransitionDataSets;

        public TransitionDataSet GetTransition(ViewTransition transitionKey)
        {
            for (int i = 0; i < TransitionDataSets.Length; i++)
            {
                if (TransitionDataSets[i].TransitionKey == transitionKey)
                {
                    return TransitionDataSets[i];
                }
            }

            throw new System.Exception($"No transition found for {transitionKey}");
        }
    }
}