
namespace StardropTools.Tween
{
    public class TweenComponentPosition : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 start = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 end = UnityEngine.Vector3.one;

        public CoreEvent<UnityEngine.Vector3> OnTween = new CoreEvent<UnityEngine.Vector3>();

        public override void PlayTween()
        {
            if (data.hasStart)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    tween = Tween.Position(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
                else
                    tween = Tween.LocalPosition(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            }

            else
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    tween = Tween.Position(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
                else
                    tween = Tween.LocalPosition(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    start = target.position;
                else
                    start = target.localPosition;

                end = start;
                copyValues = false;
            }
        }
    }
}