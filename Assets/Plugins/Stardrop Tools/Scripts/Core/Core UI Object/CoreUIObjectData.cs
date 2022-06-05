using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class CoreUIObjectData
    {
        public enum PivotEnum
        {
            upperCornerLeft, upperCenter, upperCornerRight,
            middleLeft, middleCenter, middleRight,
            lowerCornerLeft, lowerCenter, lowerCornerRight
        }

        public enum AnchorEnum
        {
            upperCornerLeft, upperCenter, upperCornerRight,
            middleLeft, middleCenter, middleRight,
            lowerCornerLeft, lowerCenter, lowerCornerRight,
            stretchTop, stretchRight, stretchBottom, stretchLeft
        }

        public enum LayoutOptions { none, orientation, stretch }
        public enum StretchEnum { none, top, left, right, bottom }
        public enum OrientationEnum { none, center, top, left, right, bottom }

        [Header("Data")]
        [SerializeField] RectTransform rectTransform;
        [Space]
        [SerializeField] LayoutOptions layoutOption;
        [SerializeField] OrientationEnum orientation;
        [SerializeField] StretchEnum stretch;
        [Space]
        [SerializeField] AnchorEnum anchor;
        [SerializeField] PivotEnum pivot;
        [Space]
        [SerializeField] float targetWidth;
        [SerializeField] float targetHeight;
        public bool UseTargetSize;
        public bool ValidatePivotAndAnchor;
        [Space]
        public bool resetCenter;

        public RectTransform RectTransform { get => rectTransform; }

        public LayoutOptions LayoutOption { get => layoutOption; set => layoutOption = value; }
        public StretchEnum Stretch { get => stretch; set => stretch = value; }
        public OrientationEnum Orientation { get => orientation; set => orientation = value; }

        public PivotEnum Pivot { get => pivot; set => pivot = value; }
        public AnchorEnum Anchor { get => anchor; set => anchor = value; }

        public float TargetWidth { get => targetWidth; set => targetWidth = value; }
        public float TargetHeight { get => targetHeight; set => targetHeight = value; }

        public bool IsStretching { get; private set; }

        public void SetRectTransform(RectTransform rect)
            => rectTransform = rect;

        public void SetDataPivot(PivotEnum pivot)
        {
            if (this.pivot == pivot)
                return;

            this.pivot = pivot;
        }

        public void SetDataAnchor(AnchorEnum anchor)
        {
            if (this.anchor == anchor)
                return;

            this.anchor = anchor;
        }


        public void SetDataLayout(LayoutOptions layout)
        {
            if (layoutOption == layout)
                return;

            layoutOption = layout;
        }

        public void SetDataOrientation(OrientationEnum orientation)
        {
            if (this.orientation == orientation)
                return;

            this.orientation = orientation;
        }

        public void SetDataStretch(StretchEnum stretch)
        {
            if (this.stretch == stretch)
                return;

            this.stretch = stretch;

            IsStretching = stretch != StretchEnum.none ? true : false;
        }
    }
}