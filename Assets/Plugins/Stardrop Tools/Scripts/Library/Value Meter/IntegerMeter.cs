

namespace StardropTools
{
    public class IntegerMeter : ValueMeter
    {
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected int startValue;
        [UnityEngine.SerializeField] protected int minValue;
        [UnityEngine.SerializeField] protected int maxValue;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected int value;

        protected int targetValue;
        public int Value { get => value; }

        public readonly CoreEvent<int> OnValueChange = new CoreEvent<int>();

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
            ValueChange();
        }

        public virtual void ValueChange()
        {
            OnValueChange?.Invoke(value);
        }

        public void IncrementValue(int amount, bool lerp = false)
        {
            var target = UnityEngine.Mathf.Clamp(value + amount, 0, maxValue);
            ValueChecks(target, lerp);
        }

        public void DecrementValue(int amount, bool lerp = false) => IncrementValue(-amount, lerp);

        public void MultiplyValue(int amount, bool lerp = false)
        {
            var target = UnityEngine.Mathf.Clamp(value * amount, 0, maxValue);
            ValueChecks(target, lerp);
        }

        public void DivideValue(int amount, bool lerp = false)
        {
            var target = UnityEngine.Mathf.Clamp(value / amount, 0, maxValue);
            ValueChecks(target, lerp);
        }


        protected virtual void CheckIfMax()
        {
            if (value == maxValue)
                isMax = true;

            OnReachMax?.Invoke();
        }

        protected virtual void CheckIfMin()
        {
            if (value == minValue)
                isMin = true;

            OnReachMin?.Invoke();
        }

        protected void ValueChecks(int targetValue, bool lerp = false)
        {
            if (lerp)
                LerpValue(targetValue);
            else
                value = targetValue;

            CheckIfMin();
            CheckIfMax();

            OnValueChangeDry?.Invoke();
            ValueChange();
        }

        protected void LerpValue(int target)
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

        protected System.Collections.IEnumerator LerpValueCR(int target)
        {
            float t = 0;
            float animTime = .2f;
            int start = value;

            while (t < animTime)
            {
                value = (int)UnityEngine.Mathf.Lerp(start, target, t / animTime);

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