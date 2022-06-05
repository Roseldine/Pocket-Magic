
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRotation : TweenQuaternion
    {
        Transform transform;

        public TweenRotation(Transform transform, int tweenID, Quaternion targetQuaternion, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Quaternion> updateEvent = null)
                      : base(tweenID, targetQuaternion, targetQuaternion, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = transform.rotation;
            target = targetQuaternion;
            this.transform = transform;

            SetBaseValues(Tween.TweenType.Rotation, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            lerped = Quaternion.LerpUnclamped(start, target, curve.Evaluate(percent));
            transform.rotation = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
    
}