

namespace StardropTools.Tween
{
    public abstract class TweenComponentSpriteRenderer : TweenComponent
    {
        public UnityEngine.SpriteRenderer target;
        [UnityEngine.SerializeField] protected bool copyValues;
    }
}