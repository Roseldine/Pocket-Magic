
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenManager : Singleton<TweenManager>
    {
        [SerializeField] TweenCurvesSO curves;
        public static System.Collections.Generic.List<TweenBase> tweens = new System.Collections.Generic.List<TweenBase>();

        protected override void Awake()
        {
            base.Awake();
            LoopManager.OnUpdate.AddListener(UpdateTweens);
        }


        public void ProcessTween(TweenBase tween)
        {
            if (Application.isPlaying == false)
            {
                Debug.Log("Can't tween on Edit Mode!");
                return;
            }

            if (tween == null)
            {
                Debug.Log("Tween is null!");
                return;
            }

            if (tween.duration <= 0)
            {
                Debug.Log("Tween has zero duration!");
                return;
            }

            FilterTween(tween.tweenType, tween.tweenID);

            AddTween(tween);
        }

        void FilterTween(Tween.TweenType type, int tweenID)
        {
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                if (tweens[i].tweenID == tweenID && tweens[i].tweenType == type)
                {
                    tweens[i].Stop();
                }
            }
        }

        void AddTween(TweenBase tween)
        {
            if (tweens.Contains(tween) == false)
            {
                tween.Initialize();
                tweens.Add(tween);
            }
        }

        public void RemoveTween(TweenBase tween)
            => tweens.RemoveSafe(tween);

        public void UpdateTweens()
        {
            if (tweens.Exists())
                for (int i = 0; i < tweens.Count; i++)
                {
                    if (tweens.Exists() == false)
                        break;

                    tweens[i].StateMachine();
                }
        }
    }
}