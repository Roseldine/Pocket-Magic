
namespace StardropTools.Tween
{
    public class TweenComponentSizeDelta : TweenComponentRectTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector2 start = UnityEngine.Vector2.one;
        public UnityEngine.Vector2 end = UnityEngine.Vector2.one;

        public CoreEvent<UnityEngine.Vector2> OnTween = new CoreEvent<UnityEngine.Vector2>();

        public override void PlayTween()
        {
            if (data.hasStart)
                tween = Tween.SizeDelta(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.SizeDelta(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                start = target.sizeDelta;
                end = start;
                copyValues = false;
            }
        }
    }
}