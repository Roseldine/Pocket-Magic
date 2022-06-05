

namespace StardropTools.Tween
{
    public abstract class TweenComponentTransform : TweenComponent
    {
        [UnityEngine.SerializeField] protected ITweenComponent.GlobalOrLocal globalOrLocal;
        public UnityEngine.Transform target;
        [UnityEngine.SerializeField] protected bool copyValues;
    }
}