
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenTextMeshColor : TweenColor
    {
        TMPro.TextMeshProUGUI textMesh;

        public TweenTextMeshColor(TMPro.TextMeshProUGUI textMesh, int tweenID, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Color> updateEvent = null)
            : base(tweenID, targetColor, targetColor, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = textMesh.color;
            target = targetColor;
            this.textMesh = textMesh;

            SetBaseValues(Tween.TweenType.ImageColor, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Color.LerpUnclamped(start, target, curve.Evaluate(percent));
            textMesh.color = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
}