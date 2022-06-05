
namespace StardropTools.Tween
{
    public class TweenComponentAnchoredPosition : TweenComponentRectTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector2 start = UnityEngine.Vector2.one;
        public UnityEngine.Vector2 end = UnityEngine.Vector2.one;

        public CoreEvent<UnityEngine.Vector2> OnTween = new CoreEvent<UnityEngine.Vector2>();

        public override void PlayTween()
        {
            if (data.hasStart)
                tween = Tween.AnchoredPosition(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.AnchoredPosition(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                start = target.anchoredPosition;
                end = start;
                copyValues = false;
            }
        }
    }
}