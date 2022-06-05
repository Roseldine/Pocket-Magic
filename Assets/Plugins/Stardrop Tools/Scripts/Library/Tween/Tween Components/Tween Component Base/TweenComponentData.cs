
namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenComponentData
    {
        public Tween.LoopType loop;
        public Tween.EaseCurve ease;
        [UnityEngine.Space]
        public float duration = 1;
        public float delay;
        [UnityEngine.Space]
        public bool ignoreTimeScale = true;
        public bool hasStart = true;

        public UnityEngine.AnimationCurve curve { get => Tween.GetEaseCurve(ease); }
    }
}