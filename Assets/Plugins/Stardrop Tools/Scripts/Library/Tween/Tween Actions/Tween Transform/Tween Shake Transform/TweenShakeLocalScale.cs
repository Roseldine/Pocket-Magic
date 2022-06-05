
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeLocalScale : TweenShakeVector3
    {
        Transform transform;

        public TweenShakeLocalScale(Transform transform, int tweenID, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                      : base(tweenID, transform.localScale, intensity, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            this.transform = transform;
            start = transform.localScale;

            SetBaseValues(Tween.TweenType.ShakePosition, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            transform.localScale = lerped;
        }
    }
    
}