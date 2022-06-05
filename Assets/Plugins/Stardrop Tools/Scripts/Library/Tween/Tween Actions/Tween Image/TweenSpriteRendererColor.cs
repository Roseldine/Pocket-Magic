
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSpriteRendererColor : TweenColor
    {
        SpriteRenderer renderer;

        public TweenSpriteRendererColor(SpriteRenderer renderer, int tweenID, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Color> updateEvent = null)
            : base(tweenID, targetColor, targetColor, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = renderer.color;
            target = targetColor;
            this.renderer = renderer;

            SetBaseValues(Tween.TweenType.SpriteRendererColor, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Color.LerpUnclamped(start, target, curve.Evaluate(percent));
            renderer.color = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
}