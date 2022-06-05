
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLocalRotation : TweenQuaternion
    {
        Transform transform;

        public TweenLocalRotation(Transform transform, int tweenID, Quaternion targetQuaternion, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Quaternion> updateEvent = null)
                      : base(tweenID, targetQuaternion, targetQuaternion, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = transform.localRotation; // local
            target = targetQuaternion;
            this.transform = transform;

            SetBaseValues(Tween.TweenType.LocalRotation, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            lerped = Quaternion.LerpUnclamped(start, target, curve.Evaluate(percent));
            transform.localRotation = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
    
}