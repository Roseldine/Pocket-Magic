
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenFloat : TweenBase
    {
        public float start;
        public float target;
        public float lerped;

        public static CoreEvent<float> OnUpdate = new CoreEvent<float>();

        public TweenFloat(int tweenID, float startFloat, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<float> updateEvent = null)
        {
            start = startFloat;
            target = targetFloat;

            SetBaseValues(Tween.TweenType.Float, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Mathf.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            float temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}