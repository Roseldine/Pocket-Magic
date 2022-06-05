using System.Collections;
using UnityEngine;

namespace StardropTools
{
    public class SimpleAnimationTransitor : CoreComponent
    {
        [SerializeField] new SimpleAnimation animation;
        [Space]
        [SerializeField] int animationID;
        [SerializeField] string animationName;
        [Space]
        [SerializeField] bool forceAnimation;
        [SerializeField] bool disableOnFinish;

        public void PlayAnimation()
        {
            animation.PlayAnimation(animationID, forceAnimation, disableOnFinish);
        }

        public void CrossFadeAnimation()
        {
            animation.CrossFadeAnimation(animationID, forceAnimation, disableOnFinish);
        }
    }
}