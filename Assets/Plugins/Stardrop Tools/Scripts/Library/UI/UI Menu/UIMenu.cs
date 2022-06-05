
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIMenu : CoreUIObject
    {
        public int menuID;

        public override void Initialize()
        {
            base.Initialize();
            OnEnabled.AddListener(Open);
        }

        public virtual void Open()
        {
            SetActive(true);
        }

        public virtual void Close()
        {
            SetActive(false);
        }
    }
}