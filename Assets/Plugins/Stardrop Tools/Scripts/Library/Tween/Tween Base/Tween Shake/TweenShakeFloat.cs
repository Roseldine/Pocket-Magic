


namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenShakeFloat : TweenFloat
    {
        protected float intensity;

        public TweenShakeFloat(int tweenID, float targetFloat, float intensity, float duration, float delay, bool ignoreTimeScale, UnityEngine.AnimationCurve curve, Tween.LoopType loop, CoreEvent<float> updateEvent = null)
                      : base(tweenID, targetFloat, targetFloat, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = targetFloat;
            target = targetFloat;
            this.intensity = intensity;

            SetBaseValues(Tween.TweenType.ShakeFloat, tweenID, duration, delay, ignoreTimeScale, curve, loop);

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
            float amount = intensity * curve.Evaluate(percent);
            lerped = start + UnityEngine.Random.Range(-amount, amount);
        }
    }
}