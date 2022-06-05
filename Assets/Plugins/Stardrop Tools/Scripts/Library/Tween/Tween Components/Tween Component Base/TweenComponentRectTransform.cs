

namespace StardropTools.Tween
{
    public abstract class TweenComponentRectTransform : TweenComponent
    {
        [UnityEngine.SerializeField] protected ITweenComponent.GlobalOrLocal globalOrLocal;
        public UnityEngine.RectTransform target;
        [UnityEngine.SerializeField] protected bool copyValues;
    }
}