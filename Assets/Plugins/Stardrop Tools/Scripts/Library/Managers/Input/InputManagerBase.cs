
using UnityEngine;

public abstract class InputManagerBase : MonoBehaviour
{
    [Header("Generic Info")]
    [SerializeField] protected float horizontal;
    [SerializeField] protected float vertical;
    [Space]
    [SerializeField] protected bool hasInput;
    [SerializeField] protected bool isOverUI;

    [Header("Display Info")]
    [SerializeField] protected Vector2 screenSize;
    [SerializeField] protected float screenAvg = 0;
    [SerializeField] protected float maxScreenDistance = 100f;
    [SerializeField] protected float screenPercentTarget = .1f;
    [SerializeField] protected bool calculateScreenPercent;
    [Space]
    [SerializeField] protected Vector2 inputStart;
    [SerializeField] protected Vector2 inputCurrent;
    [Space]
    [SerializeField] protected float distanceX;
    [SerializeField] protected float distanceY;
    [SerializeField] protected float distance;
}
