
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenColor : TweenBase
    {
        public Color start;
        public Color target;
        public Color lerped;

        public static CoreEvent<Color> OnUpdate = new CoreEvent<Color>();

        public TweenColor(int tweenID, Color startColor, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Color> updateEvent = null)
        {
            start = startColor;
            target = targetColor;

            SetBaseValues(Tween.TweenType.Color, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Color.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Color temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}