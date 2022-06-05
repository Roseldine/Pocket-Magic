
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLocalRotationEuler : TweenVector3
    {
        Transform transform;

        public TweenLocalRotationEuler(Transform transform, int tweenID, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                      : base(tweenID, targetVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = transform.localEulerAngles; // local
            target = targetVector;
            this.transform = transform;

            SetBaseValues(Tween.TweenType.LocalRotationEuler, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector3.LerpUnclamped(start, target, curve.Evaluate(percent));
            transform.localEulerAngles = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
    
}