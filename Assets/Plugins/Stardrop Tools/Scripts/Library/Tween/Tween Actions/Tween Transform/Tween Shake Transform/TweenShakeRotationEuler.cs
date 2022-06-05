
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeRotationEuler : TweenShakeVector3
    {
        Transform transform;

        public TweenShakeRotationEuler(Transform transform, int tweenID, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                      : base(tweenID, transform.eulerAngles, intensity, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            this.transform = transform;
            start = transform.eulerAngles;

            SetBaseValues(Tween.TweenType.ShakePosition, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            transform.eulerAngles = lerped;
        }
    }
    
}