
namespace StardropTools
{
    public abstract class ValueMeter : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] protected int meterID;

        protected bool isMin;
        protected bool isMax;

        protected UnityEngine.Coroutine lerpValueCR;
        protected bool isInitialized;

        public int MeterID { get => meterID; set => meterID = value; }

        public readonly CoreEvent OnReachMin = new CoreEvent();
        public readonly CoreEvent OnReachMax = new CoreEvent();
        public readonly CoreEvent OnValueChangeDry = new CoreEvent();

        public abstract void Initialize();
        public abstract void ResetValue();
    }
}