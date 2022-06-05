


namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenShakeInt : TweenInt
    {
        protected int intensity;

        public TweenShakeInt(int tweenID, int targetInt, int intensity, float duration, float delay, bool ignoreTimeScale, UnityEngine.AnimationCurve curve, Tween.LoopType loop, CoreEvent<int> updateEvent = null)
                      : base(tweenID, targetInt, targetInt, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = targetInt;
            target = targetInt;
            this.intensity = intensity;

            SetBaseValues(Tween.TweenType.ShakeInteger, tweenID, duration, delay, ignoreTimeScale, curve, loop);

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
            int amount = (int)(intensity * curve.Evaluate(percent));
            lerped = start + UnityEngine.Random.Range(-amount, amount);
        }
    }
}