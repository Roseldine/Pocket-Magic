
namespace StardropTools.UI
{
    public class UISizeCopy : UnityEngine.MonoBehaviour
    {
        public enum CopyOrientation { both, horizontal, vertical }

        [UnityEngine.SerializeField] string identifier;
        [UnityEngine.SerializeField] UnityEngine.RectTransform target;
        [UnityEngine.SerializeField] CopyOrientation orientation;
        [UnityEngine.SerializeField] UnityEngine.RectTransform[] copys;
        [UnityEngine.SerializeField] bool updateName;
        [UnityEngine.SerializeField] bool copy;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool getChildren;
        [UnityEngine.SerializeField] bool clear;

        public void Copy()
        {
            if (target != null)
            {
                if (getChildren)
                    copys = Utilities.GetItems<UnityEngine.RectTransform>(target);

                if (copys != null && copys.Length > 0)
                    for (int i = 0; i < copys.Length; i++)
                    {
                        if (orientation == CopyOrientation.both)
                            copys[i].sizeDelta = target.sizeDelta;
                        else if (orientation == CopyOrientation.horizontal)
                            copys[i].sizeDelta = new UnityEngine.Vector2(target.sizeDelta.x, copys[i].sizeDelta.y);
                        else if (orientation == CopyOrientation.vertical)
                            copys[i].sizeDelta = new UnityEngine.Vector2(copys[i].sizeDelta.x, target.sizeDelta.y);
                    }
            }
        }

        public void SetName()
        {
            if (UnityEngine.Application.isPlaying)
                return;
            if (identifier != null && identifier.Length > 0)
                name = "Size Copy - " + identifier;
        }

        private void OnValidate()
        {
            if (copy)
                Copy();
            copy = false;

            if (updateName)
                SetName();
            updateName = false;

            if (clear)
                copys = null;
            clear = false;

            if (getChildren)
            {
                copys = target.GetComponentsInChildren<UnityEngine.RectTransform>();
                getChildren = false;
            }
        }
    }
}