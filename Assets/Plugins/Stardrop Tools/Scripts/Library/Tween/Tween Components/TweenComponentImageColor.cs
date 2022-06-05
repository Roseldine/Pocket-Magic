
namespace StardropTools.Tween
{
    public class TweenComponentImageColor : TweenComponentImage
    {
        [UnityEngine.Space]
        public UnityEngine.Color start = UnityEngine.Color.white;
        public UnityEngine.Color end = UnityEngine.Color.white;

        public CoreEvent<UnityEngine.Color> OnTween = new CoreEvent<UnityEngine.Color>();

        public override void PlayTween()
        {
            if (data.hasStart)
                tween = Tween.ImageColor(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.ImageColor(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                start = target.color;
                end = start;
                copyValues = false;
            }
        }
    }
}