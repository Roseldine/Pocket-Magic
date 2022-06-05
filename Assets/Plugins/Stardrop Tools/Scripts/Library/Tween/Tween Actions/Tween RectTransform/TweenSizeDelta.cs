
using UnityEngine;

namespace StardropTools.Tween
{
    
    public class TweenSizeDelta : TweenVector2
    {
        RectTransform rectTransform;

        public TweenSizeDelta(RectTransform rectTransform, int tweenID, Vector2 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector2> updateEvent = null)
                      : base(tweenID, targetVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = rectTransform.sizeDelta;
            target = targetVector;
            this.rectTransform = rectTransform;

            SetBaseValues(Tween.TweenType.SizeDelta, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector2.LerpUnclamped(start, target, curve.Evaluate(percent));
            rectTransform.sizeDelta = lerped;

            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Vector2 temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
    
}