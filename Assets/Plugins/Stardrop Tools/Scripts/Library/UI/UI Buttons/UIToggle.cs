using System.Collections;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggle : UIButton
    {
        [SerializeField] protected bool value;
        [SerializeField] protected bool toggle;

        [Header("Toggle Components")]
        [SerializeField] RectTransform parentToggleComponents;
        [SerializeField] UIToggleComponent[] toggleComponents;
        [SerializeField] bool getComponents;

        public bool Value { get => value; }

        public CoreEvent OnToggle = new CoreEvent();
        public CoreEvent<bool> OnToggleValue = new CoreEvent<bool>();

        public override void Initialize()
        {
            base.Initialize();
            Toggle(value);
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            OnClick.AddListener(Toggle);
            SubscribeToggleComponents();
        }

        public void Toggle()
        {
            value = !value;
            OnToggle?.Invoke();
            OnToggleValue?.Invoke(value);
        }

        public void Toggle(bool val)
        {
            value = val;
            OnToggle?.Invoke();
            OnToggleValue?.Invoke(value);
        }

        void SubscribeToggleComponents()
        {
            GetToggleComponents();

            if (toggleComponents != null && toggleComponents.Length > 0)
                foreach (UIToggleComponent component in toggleComponents)
                    component.SubscribeToToggle(this);
        }

        void GetToggleComponents()
        {
            if (parentToggleComponents == null)
                return;

            toggleComponents = parentToggleComponents.GetComponents<UIToggleComponent>();

            //if (toggleComponents == null || toggleComponents.Length != parentToggleComponents.childCount)
            //    toggleComponents = Utilities.GetItems<UIToggleComponent>(parentToggleComponents);

            if (toggleComponents != null && toggleComponents.Length > 0)
                foreach (UIToggleComponent component in toggleComponents)
                    component.SetToggleTarget(this);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (getComponents)
            {
                GetToggleComponents();
                getComponents = false;
            }
        }
    }
}