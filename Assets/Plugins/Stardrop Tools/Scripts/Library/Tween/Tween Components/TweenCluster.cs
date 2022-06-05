
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenCluster : CoreComponent
    {
        [SerializeField] bool testTweens;
        [Space]
        [SerializeField] Transform[] parentTweens;
        [SerializeField] TweenComponent[] tweens;
        [SerializeField] float duration;
        [SerializeField] bool getTweens;
        [Space]
        [SerializeField] TweenCluster[] nextTweens;

        float time;

        public readonly CoreEvent OnTweenStart = new CoreEvent();
        public readonly CoreEvent OnTweenComplete = new CoreEvent();

        public override void Initialize()
        {
            base.Initialize();
            GetTweens();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            StopTweens();
        }

        public void PlayTweens()
        {
            time = 0;
            StopTweens();

            for (int i = 0; i < tweens.Length; i++)
                tweens[i].PlayTween();

            OnTweenStart?.Invoke();
            Invoke(nameof(TweenComplete), duration);
        }

        public void PauseTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].PauseTween();
        }

        public void StopTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].StopTween();
        }

        void TweenComplete()
        {
            if (nextTweens != null && nextTweens.Length > 0)
                for (int i = 0; i < nextTweens.Length; i++)
                    nextTweens[i].PlayTweens();

            OnTweenComplete?.Invoke();
        }

        void GetTweens()
        {
            System.Collections.Generic.List<TweenComponent> list = new System.Collections.Generic.List<TweenComponent>();

            for (int p = 0; p < parentTweens.Length; p++)
            {
                var components = parentTweens[p].GetComponents<TweenComponent>();
                if (components.Exists())
                {
                    for (int i = 0; i < components.Length; i++)
                        list.Add(components[i]);
                }

                components = Utilities.GetItems<TweenComponent>(parentTweens[p]);
                if (components.Exists())
                {
                    for (int i = 0; i < components.Length; i++)
                        list.Add(components[i]);
                }
            }

            tweens = list.ToArray();

            if (tweens.Exists())
            {
                duration = 0;

                for (int i = 0; i < tweens.Length; i++)
                {
                    var tween = tweens[i];
                    tween.SetTweenID(i);

                    float tweenDuration = tween.Duration + tween.Delay;
                    if (tweenDuration > duration)
                        duration = tweenDuration;
                }
            }
        }

        private void OnValidate()
        {
            if (getTweens)
                GetTweens();
            getTweens = false;

            if (testTweens)
                PlayTweens();
            testTweens = false;
        }
    }
}