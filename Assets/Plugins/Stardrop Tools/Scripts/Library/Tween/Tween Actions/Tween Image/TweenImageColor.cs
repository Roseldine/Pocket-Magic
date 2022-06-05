
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenImageColor : TweenColor
    {
        UnityEngine.UI.Image image;

        public TweenImageColor(UnityEngine.UI.Image image, int tweenID, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Color> updateEvent = null)
            : base(tweenID, targetColor, targetColor, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = image.color;
            target = targetColor;
            this.image = image;

            SetBaseValues(Tween.TweenType.ImageColor, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Color.LerpUnclamped(start, target, curve.Evaluate(percent));
            image.color = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
}