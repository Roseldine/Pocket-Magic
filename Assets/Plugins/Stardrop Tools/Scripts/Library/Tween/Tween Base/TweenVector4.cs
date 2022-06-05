
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenVector4 : TweenBase
    {
        public Vector4 start;
        public Vector4 target;
        public Vector4 lerped;

        public static CoreEvent<Vector4> OnUpdate = new CoreEvent<Vector4>();

        public TweenVector4(int tweenID, Vector4 startVector, Vector4 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector4> updateEvent = null)
        {
            start = startVector;
            target = targetVector;

            SetBaseValues(Tween.TweenType.Vector4, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector4.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Vector4 temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}