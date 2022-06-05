
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenShakeVector2 : TweenVector2
    {
        protected Vector2 intensity;

        public TweenShakeVector2(int tweenID, Vector2 targetVector, Vector2 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector2> updateEvent = null)
                                  : base(tweenID, targetVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = targetVector;
            target = targetVector;
            this.intensity = intensity;

            SetBaseValues(Tween.TweenType.ShakeVector2, tweenID, duration, delay, ignoreTimeScale, curve, loop);

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

            Vector2 amount = intensity * curve.Evaluate(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);

            lerped = start + amount;
        }
    }
}