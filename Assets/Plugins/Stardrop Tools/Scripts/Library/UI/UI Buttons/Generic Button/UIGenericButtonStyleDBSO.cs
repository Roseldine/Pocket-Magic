
using UnityEngine;

namespace StardropTools.UI.GenericButton
{
    [CreateAssetMenu(menuName = "Stardrop / UI / Button / Generic Button Graphics Database ")]
    public class UIGenericButtonStyleDBSO : ScriptableObject
    {
        [SerializeField] Sprite icon;
        [SerializeField] Sprite gradient;
        public TMPro.TMP_FontAsset font;
        [Space]
        public UIGenericButtonGraphicCollection center;
        public UIGenericButtonGraphicCollection top;
        public UIGenericButtonGraphicCollection left;
        public UIGenericButtonGraphicCollection right;
        public UIGenericButtonGraphicCollection bottom;

        private void OnValidate()
        {
            if (gradient == null)
                return;

            var styles = new UIGenericButtonGraphicCollection[5];
            styles[0] = center;
            styles[1] = top;
            styles[2] = left;
            styles[3] = right;
            styles[4] = bottom;

            for (int i = 0; i < styles.Length; i++)
                styles[i].gradient = gradient;
        }
    }
}

