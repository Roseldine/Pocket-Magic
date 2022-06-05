


namespace StardropTools.Tween
{
    [System.Serializable]
    public abstract class TweenBase
    {
        public Tween.TweenType tweenType;
        public Tween.LoopType loopType;
        public Tween.TweenState tweenState;

        public int tweenID;

        public float delay;
        public float duration;
        public float runtime;
        public float timeInState;

        protected float percent;
        protected bool ignoreTimeScale;

        protected UnityEngine.AnimationCurve curve;

        public readonly CoreEvent OnStart = new CoreEvent();
        public readonly CoreEvent OnComplete = new CoreEvent();
        public readonly CoreEvent OnPaused = new CoreEvent();
        public readonly CoreEvent OnCanceled = new CoreEvent();

        public readonly CoreEvent OnDelayStart = new CoreEvent();
        public readonly CoreEvent OnDelayComplete = new CoreEvent();

        protected void SetBaseValues(Tween.TweenType type, int tweenID, float duration, float delay, bool ignoreTimeScale, UnityEngine.AnimationCurve curve, Tween.LoopType loop)
        {
            tweenType = type;
            this.tweenID = tweenID;

            this.duration = duration;
            this.delay = delay;
            this.curve = curve;

            curve.keys = curve == null ? null : curve.keys;
            loopType = loop;
            this.ignoreTimeScale = ignoreTimeScale;

            ResetRuntime();
        }

        public void Initialize()
        {
            runtime = 0;
            timeInState = 0;
            OnStart?.Invoke();

            if (delay > 0)
                ChangeState(Tween.TweenState.waiting);
            else
                ChangeState(Tween.TweenState.running);
        }

        public void StateMachine()
        {
            switch (tweenState)
            {
                case Tween.TweenState.waiting:
                    Waiting();
                    break;

                case Tween.TweenState.running:
                    Running();
                    break;

                case Tween.TweenState.complete:
                    Complete();
                    break;

                case Tween.TweenState.paused:
                    Pause();
                    break;

                case Tween.TweenState.canceled:
                    Stop();
                    break;
            }
        }

        public void ChangeState(Tween.TweenState nextState)
        {
            // check if not the same
            if (tweenState == nextState)
                return;

            // to delay
            if (nextState == Tween.TweenState.waiting)
                OnDelayStart?.Invoke();

            // from delay to running
            if (tweenState == Tween.TweenState.waiting && nextState == Tween.TweenState.running)
                OnDelayComplete?.Invoke();

            // to complete
            if (nextState == Tween.TweenState.complete)
            {
                OnComplete?.Invoke();
            }

            // to pause
            if (nextState == Tween.TweenState.paused)
                OnPaused?.Invoke();

            // to cancel
            if (nextState == Tween.TweenState.canceled)
                OnCanceled?.Invoke();

            tweenState = nextState;
            timeInState = 0;
        }

        protected virtual void Waiting()
        {
            if (ignoreTimeScale)
                timeInState += UnityEngine.Time.unscaledDeltaTime;
            else
                timeInState += UnityEngine.Time.deltaTime;

            if (timeInState >= delay)
                ChangeState(Tween.TweenState.running);
        }

        protected virtual void Running()
        {
            if (ignoreTimeScale)
            {
                timeInState += UnityEngine.Time.unscaledDeltaTime;
                runtime += UnityEngine.Time.unscaledDeltaTime;
            }
            else
            {
                timeInState += UnityEngine.Time.deltaTime;
                runtime += UnityEngine.Time.deltaTime;
            }

            timeInState += UnityEngine.Time.deltaTime;
            percent = UnityEngine.Mathf.Min(timeInState / duration, 1);

            TweenUpdate(percent);

            if (percent >= 1)
                ChangeState(Tween.TweenState.complete);
        }

        protected virtual void Complete()
        {
            if (loopType == Tween.LoopType.none)
                RemoveFromManagerList();
            else if (loopType == Tween.LoopType.loop)
                Loop();
            else if (loopType == Tween.LoopType.pingPong)
                PingPong();
        }

        public virtual void Pause() { }

        public virtual void Stop()
            => RemoveFromManagerList();

        protected abstract void TweenUpdate(float percent);
        protected abstract void Loop();
        protected abstract void PingPong();

        protected void RemoveFromManagerList()
        {
            OnStart.RemoveAllListeners();
            OnComplete.RemoveAllListeners();
            OnPaused.RemoveAllListeners();
            OnCanceled.RemoveAllListeners();
            OnDelayStart.RemoveAllListeners();
            OnDelayComplete.RemoveAllListeners();

            TweenManager.Instance.RemoveTween(this);
        }

        protected void ResetRuntime() => runtime = 0;
    }
}