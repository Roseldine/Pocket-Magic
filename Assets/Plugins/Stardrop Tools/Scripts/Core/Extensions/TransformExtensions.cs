
using UnityEngine;

// global & local:
//  -position
//  -rotation
//  -scale

// parent & child

public static class TransformExtensions
{
    #region Set Position

    public static void SetPosition(this Transform transform, Vector3 position)
        => transform.position = position;



    // set/change SINGLE position axis
    public static void SetPositionX(this Transform transform, float xValue)
        => transform.position.SetX(xValue);

    public static void SetPositionY(this Transform transform, float yValue)
        => transform.position.SetX(yValue);

    public static void SetPositionZ(this Transform transform, float zValue)
        => transform.position.SetX(zValue);



    // Set/change TWO position axis
    public static void SetPositionXY(this Transform transform, float xValue, float yValue)
        => transform.position.SetXY(xValue, yValue);

    public static void SetPositionXZ(this Transform transform, float xValue, float zValue)
        => transform.position.SetXZ(xValue, zValue);

    public static void SetPositionYZ(this Transform transform, float yValue, float zValue)
        => transform.position.SetYZ(yValue, zValue);

    #endregion // set position

    #region Set Local Position

    public static void SetLocalPosition(this Transform transform, Vector3 position)
        => transform.localPosition = position;



    // set/change SINGLE local position axis
    public static void SetLocalPositionX(this Transform transform, float xValue)
        => transform.localPosition.SetX(xValue);

    public static void SetLocalPositionY(this Transform transform, float yValue)
        => transform.localPosition.SetY(yValue);

    public static void SetLocalPositionZ(this Transform transform, float zValue)
        => transform.localPosition.SetZ(zValue);



    // Set/change TWO local position axis
    public static void SetLocalPositionXY(this Transform transform, float xValue, float yValue)
        => transform.localPosition.SetXY(xValue, yValue);

    public static void SetLocalPositionXZ(this Transform transform, float xValue, float zValue)
        => transform.localPosition.SetXZ(xValue, zValue);

    public static void SetLocalPositionYZ(this Transform transform, float yValue, float zValue)
        => transform.localPosition.SetYZ(yValue, zValue);

    #endregion // set local position


    #region Set Rotation

    // set/change SINGLE
    public static void SetEulerX(this Transform transform, float xValue)
        => transform.eulerAngles = transform.eulerAngles.SetX(xValue);

    public static void SetEulerY(this Transform transform, float yValue)
        => transform.eulerAngles = transform.eulerAngles.SetY(yValue);

    public static void SetEulerZ(this Transform transform, float zValue)
        => transform.eulerAngles = transform.eulerAngles.SetZ(zValue);


    // set/change TWO
    public static void SetEuluerXY(this Transform transform, float xValue, float yValue)
        => transform.eulerAngles = transform.eulerAngles.SetXY(xValue, yValue);

    public static void SetEuluerXZ(this Transform transform, float xValue, float zValue)
        => transform.eulerAngles = transform.eulerAngles.SetXZ(xValue, zValue);

    public static void SetEuluerYZ(this Transform transform, float yValue, float zValue)
        => transform.eulerAngles = transform.eulerAngles.SetYZ(yValue, zValue);

    #endregion // set rotation

    #region Set Local Rotation

    // set/change SINGLE
    public static void SetLocalEulerX(this Transform transform, float xValue)
        => transform.localEulerAngles = transform.localEulerAngles.SetX(xValue);

    public static void SetLocalEulerY(this Transform transform, float yValue)
        => transform.localEulerAngles = transform.localEulerAngles.SetY(yValue);

    public static void SetLocalEulerZ(this Transform transform, float zValue)
        => transform.localEulerAngles = transform.localEulerAngles.SetZ(zValue);


    // set/change TWO
    public static void SetLocalEuluerXY(this Transform transform, float xValue, float yValue)
        => transform.localEulerAngles = transform.localEulerAngles.SetXY(xValue, yValue);

    public static void SetLocalEuluerXZ(this Transform transform, float xValue, float zValue)
        => transform.localEulerAngles = transform.localEulerAngles.SetXZ(xValue, zValue);

    public static void SetLocalEuluerYZ(this Transform transform, float yValue, float zValue)
        => transform.localEulerAngles = transform.localEulerAngles.SetYZ(yValue, zValue);

    #endregion // set local rotation


    #region Set Scale
    // set/change SINGLE
    public static void SetLocalScaleX(this Transform transform, float xValue)
        => transform.localScale = transform.localScale.SetX(xValue);

    public static void SetLocalScaleY(this Transform transform, float yValue)
        => transform.localScale = transform.localScale.SetY(yValue);

    public static void SetLocalScaleZ(this Transform transform, float zValue)
        => transform.localScale = transform.localScale.SetZ(zValue);


    // set/change TWO
    public static void SetLocalScaleXY(this Transform transform, float xValue, float yValue)
        => transform.localScale = transform.localScale.SetXY(xValue, yValue);

    public static void SetLocalScaleXZ(this Transform transform, float xValue, float zValue)
        => transform.localScale = transform.localScale.SetXZ(xValue, zValue);

    public static void SetLocalScaleYZ(this Transform transform, float yValue, float zValue)
        => transform.localScale = transform.localScale.SetYZ(yValue, zValue);
    #endregion // set scale


    #region Set Parent & Child
    public static void SetParent(this Transform transform, Transform parent)
        => transform.parent = parent;

    public static void SetChild(this Transform parent, Transform child)
        => child.parent = parent;

    public static void SetPreviousSibling(this Transform transform)
        => transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);

    public static void SetNexySibling(this Transform transform)
        => transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);


    #endregion // parent & child
}