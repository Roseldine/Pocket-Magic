
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakePosition : TweenShakeVector3
    {
        Transform transform;
        Vector3 endPosition;

        public TweenShakePosition(Transform transform, int tweenID, Vector3 intensity, Vector3 endPosition, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                      : base(tweenID, transform.position, intensity, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            this.transform = transform;
            start = transform.position;
            this.endPosition = endPosition;

            SetBaseValues(Tween.TweenType.ShakePosition, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            transform.position = lerped;

            if (percent >= 1)
                transform.position = endPosition;
        }
    }
    
}