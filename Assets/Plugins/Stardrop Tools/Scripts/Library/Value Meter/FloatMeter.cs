
namespace StardropTools
{
    public class FloatMeter : ValueMeter
    {
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected float startValue;
        [UnityEngine.SerializeField] protected float minValue;
        [UnityEngine.SerializeField] protected float maxValue;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected float value;

        protected float targetValue;
        public float Value { get => value; }

        public readonly CoreEvent<float> OnValueChange = new CoreEvent<float>();

        public override void Initialize()
        {
            if (isInitialized)
                return;

            ResetValue();
            isInitialized = true;
        }

        public override void ResetValue()
        {
            value = startValue;
            OnValueChange?.Invoke(value);
        }

        public void IncrementValue(float amount, bool lerp = false)
        {
            var target = UnityEngine.Mathf.Clamp(value + amount, 0, maxValue);
            ValueChecks(target, lerp);
        }

        public void DecrementValue(float amount, bool lerp = false) => IncrementValue(-amount, lerp);

        public void MultiplyValue(float amount, bool lerp = false)
        {
            var target = UnityEngine.Mathf.Clamp(value * amount, 0, maxValue);
            ValueChecks(target, lerp);
        }

        public void DivideValue(float amount, bool lerp = false)
        {
            var target = UnityEngine.Mathf.Clamp(value / amount, 0, maxValue);
            ValueChecks(target, lerp);
        }


        protected void CheckIfMax()
        {
            if (value == maxValue)
                isMax = true;

            OnReachMax?.Invoke();
        }

        protected void CheckIfMin()
        {
            if (value == 0)
                isMin = true;

            OnReachMin?.Invoke();
        }

        protected void ValueChecks(float targetValue, bool lerp = false)
        {
            if (lerp)
                LerpValue(targetValue);
            else
                value = targetValue;

            CheckIfMin();
            CheckIfMax();

            OnValueChangeDry?.Invoke();
            OnValueChange?.Invoke(value);
        }

        protected void LerpValue(float target)
        {
            StopLerp();
            targetValue = target;
            lerpValueCR = StartCoroutine(LerpValueCR(target));
        }

        protected void StopLerp()
        {
            if (lerpValueCR != null)
                StopCoroutine(lerpValueCR);
            value = targetValue;
        }

        protected System.Collections.IEnumerator LerpValueCR(float target)
        {
            float t = 0;
            float animTime = .2f;
            float start = value;

            while (t < animTime)
            {
                value = UnityEngine.Mathf.Lerp(start, target, t / animTime);



                OnValueChangeDry?.Invoke();
                OnValueChange?.Invoke(value);

                t += UnityEngine.Time.deltaTime;
                yield return null;
            }

            value = target;

            OnValueChangeDry?.Invoke();
            OnValueChange?.Invoke(value);
        }

        protected virtual void OnValidate()
        {
            if (value != startValue)
                value = startValue;
        }
    }
}