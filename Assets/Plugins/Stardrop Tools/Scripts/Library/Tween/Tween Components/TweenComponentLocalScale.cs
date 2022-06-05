
namespace StardropTools.Tween
{
    public class TweenComponentLocalScale : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 start = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 end = UnityEngine.Vector3.one;

        public CoreEvent<UnityEngine.Vector3> OnTween = new CoreEvent<UnityEngine.Vector3>();

        public override void PlayTween()
        {
            if (data.hasStart)
                tween = Tween.LocalScale(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.LocalScale(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                start = target.localScale;
                end = start;
                copyValues = false;
            }
        }
    }
}