
namespace StardropTools.Tween
{
    public class TweenComponentShakeScale : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 intensity = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 end = UnityEngine.Vector3.one;

        public CoreEvent<UnityEngine.Vector3> OnTween = new CoreEvent<UnityEngine.Vector3>();

        public override void PlayTween()
        {
            tween = Tween.ShakeLocalScale(target, intensity, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                end = target.localScale;

                copyValues = false;
            }
        }
    }
}