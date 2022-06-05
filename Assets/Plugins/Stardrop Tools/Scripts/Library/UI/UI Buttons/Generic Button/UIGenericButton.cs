
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI.GenericButton
{
    public class UIGenericButton : UIButton
    {
        public enum GraphicOptions { icon, text, both }

        [Header("Generic Buttons")]
        [SerializeField] UIGenericButtonComponentData componentData;
        [SerializeField] CoreUIObjectData.OrientationEnum orientation;
        [SerializeField] GraphicOptions graphicOptions;
        [Space]
        [SerializeField] Sprite icon;
        [SerializeField] string text;
        [Range(0, 4)] [SerializeField] int pixelMultiplierID;
        [SerializeField] float[] pixelPerUnitMultiplier = { 4, 8, 16, 32, 64, 128 };
        //[Min(0)][SerializeField] float roundCornerMultiplier = 32f;
        [Space]
        [Min(0)][SerializeField] int styleID;
        [SerializeField] UIGenericButtonStyleDBSO[] spriteDB;

        protected UIGenericButtonStyleDBSO style;
        protected UIGenericButtonGraphicCollection graphics;

        protected void ValidateStyle()
        {
            if (spriteDB.Exists() == false)
                return;

            if (style != spriteDB[styleID])
                style = spriteDB[styleID];

            switch (DataOrientation)
            {
                case CoreUIObjectData.OrientationEnum.center:
                    graphics = style.center;
                    break;

                case CoreUIObjectData.OrientationEnum.top:
                    graphics = style.top;
                    break;

                case CoreUIObjectData.OrientationEnum.left:
                    graphics = style.left;
                    break;

                case CoreUIObjectData.OrientationEnum.right:
                    graphics = style.right;
                    break;

                case CoreUIObjectData.OrientationEnum.bottom:
                    graphics = style.bottom;
                    break;

                default:
                    break;
            }
        }

        protected void ValidateGraphics()
        {
            if (spriteDB.Exists() == false || graphics == null)
                return;

            // set component data
            if (componentData.background != null)
                componentData.SetBackground(graphics.background);

            if (componentData.gradient != null)
                componentData.SetGradient(graphics.gradient);

            if (componentData.shade != null)
                componentData.SetShade(graphics.shade);

            if (componentData.outline != null)
                componentData.SetOutline(graphics.outline);

            // icon & text
            if (componentData.icon != null)
                componentData.SetIcon(icon);

            if (componentData.text != null)
                componentData.SetText(text);

            if (componentData.text != null)
                componentData.SetTextFont(style.font);

            componentData.SetPixelPerUnityMultiplier(pixelPerUnitMultiplier[pixelMultiplierID]);
        }

        protected void ValidateOutline()
        {
            if (componentData.outline == null || componentData.outlineRect == null)
                return;

            RectTransform outlineRect = componentData.outlineRect;
            float pixels = componentData.outline.pixelsPerUnitMultiplier;
            outlineRect.sizeDelta = new Vector2(0, Size.y - pixels * .25f);
        }

        protected void ValidateData()
        {
            ValidateStyle();
            ValidateGraphics();
            ValidateOutline();

            // orientation
            if (orientation != CoreUIObjectData.OrientationEnum.none && DataLayout != CoreUIObjectData.LayoutOptions.orientation)
                DataLayout = CoreUIObjectData.LayoutOptions.orientation;

            if (DataOrientation != orientation)
                DataOrientation = orientation;

            // set icon or text
            if (graphicOptions == GraphicOptions.icon)
            {
                componentData.SetIconActive(true);
                componentData.SetTextActive(false);
            }

            else if (graphicOptions == GraphicOptions.text)
            {
                componentData.SetIconActive(false);
                componentData.SetTextActive(true);
            }

            else if (graphicOptions == GraphicOptions.both)
            {
                componentData.SetIconActive(true);
                componentData.SetTextActive(true);
            }
        }

        

        protected override void OnValidate()
        {
            base.OnValidate();

            ValidateData();
        }
    }

    [System.Serializable]
    public class UIGenericButtonComponentData
    {
        public Image background;
        public Image gradient;
        public Image shade;
        public Image outline;
        public RectTransform outlineRect;
        [Space]
        public Image icon;
        public TMPro.TextMeshProUGUI text;

        private GameObject iconObj, textObj;

        public void SetBackground(Sprite sprite)
        {
            if (sprite != null && background != null && background.sprite != sprite)
                background.sprite = sprite;
        }

        public void SetGradient(Sprite sprite)
        {
            if (sprite != null && gradient != null && gradient.sprite != sprite)
                gradient.sprite = sprite;
        }

        public void SetShade(Sprite sprite)
        {
            if (sprite != null && shade != null && shade.sprite != sprite)
                shade.sprite = sprite;
        }

        public void SetOutline(Sprite sprite)
        {
            if (sprite != null && outline != null && outline.sprite != sprite)
                outline.sprite = sprite;
        }

        public void SetIcon(Sprite sprite)
        {
            if (sprite != null && icon != null && icon.sprite != sprite)
                icon.sprite = sprite;
        }

        public void SetText(string value)
        {
            if (value != null && text != null && text.text != value)
                text.text = value;
        }

        public void SetTextFont(TMPro.TMP_FontAsset font)
        {
            if (font != null && text != null && text.font != font)
                text.font = font;
        }

        public void SetIconActive(bool value)
        {
            if (icon == null)
                return;

            if (iconObj == null)
                iconObj = icon.gameObject;

            iconObj.SetActive(value);
        }

        public void SetTextActive(bool value)
        {
            if (text == null)
                return;

            if (textObj == null)
                textObj = text.gameObject;

            textObj.SetActive(value);
        }

        public void SetPixelPerUnityMultiplier(float value)
        {
            var imgs = new Image[4];
            imgs[0] = background;
            imgs[1] = gradient;
            imgs[2] = shade;
            imgs[3] = outline;

            for (int i = 0; i < imgs.Length; i++)
                if (imgs[i] != null)
                    imgs[i].pixelsPerUnitMultiplier = value;
        }
    }

    [System.Serializable]
    public class UIGenericButtonGraphicData
    {
        public Sprite background;
        public Sprite gradient;
        public Sprite shade;
        public Sprite outline;
        public Sprite icon;
        public string text;
    }
}

