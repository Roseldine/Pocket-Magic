
using UnityEngine;

namespace StardropTools.UI
{
    [System.Serializable]
    public class UIColorSimple : MonoBehaviour
    {
        [SerializeField] UIColor highlightImgs;
        [SerializeField] UIColor lightImgs;
        [SerializeField] UIColor darkImgs;
        [Space]
        [Min(0)][SerializeField] float pixelPerUnit = 32;
        [SerializeField] bool validateColors = true;
        [SerializeField] bool validatePixelsPerUnit = true;

        public void UpdateColors()
        {
            highlightImgs.UpdateImageColors();
            lightImgs.UpdateImageColors();
            darkImgs.UpdateImageColors();
        }

        public void UpdateColors(float pixels)
        {
            highlightImgs.UpdateImageColors();
            lightImgs.UpdateImageColors();
            darkImgs.UpdateImageColors();
        }

        private void OnValidate()
        {
            if (validateColors)
                UpdateColors();

            if (validatePixelsPerUnit)
                UpdateColors(pixelPerUnit);
        }
    }
}
