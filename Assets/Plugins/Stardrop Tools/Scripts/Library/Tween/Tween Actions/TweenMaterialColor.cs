
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenMaterialColor : TweenColor
    {
        Material material;

        public TweenMaterialColor(Material material, int tweenID, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Color> updateEvent = null)
            : base(tweenID, targetColor, targetColor, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = material.color;
            target = targetColor;
            this.material = material;

            SetBaseValues(Tween.TweenType.MaterialColor, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Color.LerpUnclamped(start, target, curve.Evaluate(percent));
            material.color = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
}