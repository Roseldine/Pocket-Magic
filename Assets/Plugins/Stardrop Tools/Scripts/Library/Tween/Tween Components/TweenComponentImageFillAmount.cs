
namespace StardropTools.Tween
{
    public class TweenComponentImageFillAmount : TweenComponentImage
    {
        [UnityEngine.Space]
        public float start;
        public float end;

        public CoreEvent<float> OnTween = new CoreEvent<float>();

        public override void PlayTween()
        {
            if (data.hasStart)
                tween = Tween.ImageFillAmount(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.ImageFillAmount(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                start = target.fillAmount;
                end = start;
                copyValues = false;
            }
                
        }
    }
}