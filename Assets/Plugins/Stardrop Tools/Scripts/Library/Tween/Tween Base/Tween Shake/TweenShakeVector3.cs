
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenShakeVector3 : TweenVector3
    {
        protected Vector3 intensity;

        public TweenShakeVector3(int tweenID, Vector3 targetVector, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                                  : base(tweenID, targetVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = targetVector;
            target = targetVector;
            this.intensity = intensity;

            SetBaseValues(Tween.TweenType.ShakeVector3, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                lerped = start;

            percent = 1 - percent;

            Vector3 amount = intensity * curve.Evaluate(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);

            lerped = amount + start;

            OnUpdate?.Invoke(lerped);
        }
    }
}