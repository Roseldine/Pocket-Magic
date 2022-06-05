
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenImageFillAmount : TweenFloat
    {
        UnityEngine.UI.Image image;

        public TweenImageFillAmount(UnityEngine.UI.Image image, int tweenID, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<float> updateEvent = null)
            : base(tweenID, image.pixelsPerUnitMultiplier, targetFloat, duration, delay, ignoreTimeScale, curve, loop, updateEvent)
        {
            start = image.fillAmount;
            target = targetFloat;
            this.image = image;

            SetBaseValues(Tween.TweenType.ImageFillAmount, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            image.fillAmount = lerped;
        }
    }
}