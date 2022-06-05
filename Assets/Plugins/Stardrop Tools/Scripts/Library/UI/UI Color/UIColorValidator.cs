
using UnityEngine;

namespace StardropTools.UI
{
    [System.Serializable]
    public class UIColorValidator : MonoBehaviour
    {
        [SerializeField] UIColor[] colors;
        [Space]
        [Min(0)][SerializeField] float pixelPerUnit = 32;
        [SerializeField] bool validate = true;

        protected void UpdateColorsInImageArray(UnityEngine.UI.Image[] array, Color color)
        {
            if (array.Exists())
                for (int i = 0; i < array.Length; i++)
                    if (array[i] != null)
                    {
                        array[i].color = color;
                        array[i].pixelsPerUnitMultiplier = pixelPerUnit;
                    }
        }

        public void UpdateColors()
        {
            if (colors.Exists())
                foreach (UIColor color in colors)
                    color.UpdateImageColors(pixelPerUnit);
        }



        private void OnValidate()
        {
            if (validate)
            {
                UpdateColors();
            }
        }
    }
}
