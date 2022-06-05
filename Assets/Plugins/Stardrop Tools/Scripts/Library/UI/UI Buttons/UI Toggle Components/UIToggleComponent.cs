
using UnityEngine;

namespace StardropTools.UI
{
    public abstract class UIToggleComponent : MonoBehaviour
    {
        [SerializeField] protected UIToggle toggle;

        public void SetToggleTarget(UIToggle target) => toggle = target;

        public virtual void SubscribeToToggle(UIToggle target)
        {
            if (toggle != target)
                toggle = target;
        }
    }
}


