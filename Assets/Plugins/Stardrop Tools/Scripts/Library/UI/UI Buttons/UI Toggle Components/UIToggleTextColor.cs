
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleTextColor : UIToggleComponent
    {
        [SerializeField] TMPro.TextMeshProUGUI[] texts;
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
            if (texts != null && texts.Length > 0 && colors != null && colors.Length > 0)
                for (int i = 0; i < texts.Length; i++)
                    texts[i].color = color;
        }
    }
}


