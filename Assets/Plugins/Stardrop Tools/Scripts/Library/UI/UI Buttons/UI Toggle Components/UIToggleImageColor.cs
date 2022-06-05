
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleImageColor : UIToggleComponent
    {
        [SerializeField] UnityEngine.UI.Image[] images;
        [Tooltip("0-false, 1-true")]
        [SerializeField] Color[] colors;

        public override void SubscribeToToggle(UIToggle target)
        {
            base.SubscribeToToggle(target);

            target.OnToggleValue.AddListener(ToggleImageColor);
        }

        public void ToggleImageColor(bool val)
        {
            if (val == false)
                SetColors(colors[0]);
            else
                SetColors(colors[1]);
        }

        void SetColors(Color color)
        {
            if (images != null && images.Length > 0 && colors != null && colors.Length > 0)
                for (int i = 0; i < images.Length; i++)
                    images[i].color = color;
        }
    }
}


