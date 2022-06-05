
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(UISizeCopy))]
    public class UIRootCanvas : CoreUIObject
    {
        [SerializeField] UISizeCopy sizeCopy;
        [SerializeField] bool copy;

        public override void Initialize()
        {
            base.Initialize();
            sizeCopy.Copy();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (sizeCopy == null)
                sizeCopy = GetComponent<UISizeCopy>();

            if (copy)
            {
                sizeCopy.Copy();
                copy = false;
            }
        }
    }
}
