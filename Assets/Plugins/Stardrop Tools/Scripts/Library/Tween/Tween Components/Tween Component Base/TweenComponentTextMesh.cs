

namespace StardropTools.Tween
{
    public abstract class TweenComponentTextMesh : TweenComponent
    {
        public TMPro.TextMeshProUGUI target;
        [UnityEngine.SerializeField] protected bool copyValues;
    }
}