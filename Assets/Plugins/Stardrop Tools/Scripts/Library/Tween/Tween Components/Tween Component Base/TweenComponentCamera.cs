

namespace StardropTools.Tween
{
    public abstract class TweenComponentCamera : TweenComponent
    {
        public UnityEngine.Camera target;
        [UnityEngine.SerializeField] protected bool copyValues;
    }
}