
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenVector2 : TweenBase
    {
        public Vector2 start;
        public Vector2 target;
        public Vector2 lerped;

        public static CoreEvent<Vector2> OnUpdate = new CoreEvent<Vector2>();

        public TweenVector2(int tweenID, Vector2 startVector, Vector2 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector2> updateEvent = null)
        {
            start = startVector;
            target = targetVector;

            SetBaseValues(Tween.TweenType.Vector2, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector2.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Vector2 temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}