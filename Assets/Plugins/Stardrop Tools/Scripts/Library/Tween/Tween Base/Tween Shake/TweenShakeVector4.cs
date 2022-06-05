
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenShakeVector4 : TweenVector4
    {
        protected Vector4 intensity;

        public TweenShakeVector4(int tweenID, Vector4 targetVector, Vector4 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector4> updateEvent = null)
                                  : base(tweenID, targetVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = targetVector;
            target = targetVector;
            this.intensity = intensity;

            SetBaseValues(Tween.TweenType.ShakeVector4, tweenID, duration, delay, ignoreTimeScale, curve, loop);

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

            Vector4 amount = intensity * curve.Evaluate(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);
            amount.w = Random.Range(-amount.w, amount.w);

            lerped = start + amount;
        }
    }
}