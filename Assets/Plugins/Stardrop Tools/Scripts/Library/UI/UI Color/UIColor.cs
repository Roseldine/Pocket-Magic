
using UnityEngine;

namespace StardropTools.UI
{
    [System.Serializable]
    public class UIColor
    {
        public UnityEngine.UI.Image[] images;
        public Color color;
        [Min(0)]public float pixelPerUnit = 32;

        public void UpdateImageColors(float pixelsPerUnitMultiplier = -1)
        {
            if (images.Exists())
                for (int i = 0; i < images.Length; i++)
                    if (images[i] != null)
                    {
                        images[i].color = color;

                        if (pixelsPerUnitMultiplier >= 0)
                            images[i].pixelsPerUnitMultiplier = pixelPerUnit;
                    }
        }

        public void UpdatePixelsPerUnit(float pixelsPerUnitMultiplier)
        {
            pixelPerUnit = pixelsPerUnitMultiplier;

            if (images.Exists())
                for (int i = 0; i < images.Length; i++)
                    if (images[i] != null)
                        images[i].pixelsPerUnitMultiplier = pixelPerUnit;
        }
    }
}
