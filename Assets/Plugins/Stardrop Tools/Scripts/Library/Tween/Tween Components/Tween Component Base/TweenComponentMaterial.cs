

namespace StardropTools.Tween
{
    public abstract class TweenComponentMaterial : TweenComponent
    {
        public UnityEngine.Material target;
        [UnityEngine.SerializeField] protected bool copyValues;

        public CoreEvent<UnityEngine.Color> OnTween = new CoreEvent<UnityEngine.Color>();
    }
}