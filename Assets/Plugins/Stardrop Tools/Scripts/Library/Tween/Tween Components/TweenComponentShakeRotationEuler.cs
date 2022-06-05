
namespace StardropTools.Tween
{
    public class TweenComponentShakeRotationEuler : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 intensity = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 end = UnityEngine.Vector3.one;

        public CoreEvent<UnityEngine.Vector3> OnTween = new CoreEvent<UnityEngine.Vector3>();

        public override void PlayTween()
        {
            if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                tween = Tween.ShakeRotationEuler(target, intensity, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.ShakeLocalRotationEuler(target, intensity, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    end = target.eulerAngles;
                else
                    end = target.localEulerAngles;

                copyValues = false;
            }
        }
    }
}