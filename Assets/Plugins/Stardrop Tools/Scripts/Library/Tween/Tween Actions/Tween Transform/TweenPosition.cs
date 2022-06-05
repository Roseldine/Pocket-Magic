
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenPosition : TweenVector3
    {
        Transform transform;

        public TweenPosition(Transform transform, int tweenID, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                      : base(tweenID, targetVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = transform.position;
            target = targetVector;
            this.transform = transform;

            SetBaseValues(Tween.TweenType.Position, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector3.LerpUnclamped(start, target, curve.Evaluate(percent));
            transform.position = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
    
}