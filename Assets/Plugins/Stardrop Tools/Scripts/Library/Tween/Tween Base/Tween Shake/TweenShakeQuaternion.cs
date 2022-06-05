
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenShakeQuaternion : TweenQuaternion
    {
        protected Vector4 intensity;

        public TweenShakeQuaternion(int tweenID, Quaternion targetQuaternion, Vector4 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Quaternion> updateEvent = null)
                                  : base(tweenID, targetQuaternion, targetQuaternion, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = targetQuaternion;
            target = targetQuaternion;
            this.intensity = intensity;

            SetBaseValues(Tween.TweenType.ShakeQuaternion, tweenID, duration, delay, ignoreTimeScale, curve, loop);

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

            lerped.x = amount.x + start.x;
            lerped.y = amount.y + start.y;
            lerped.z = amount.z + start.z;
            lerped.w = amount.w + start.w;

            OnUpdate?.Invoke(lerped);
        }
    }
}