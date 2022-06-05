
namespace StardropTools
{
    public class CoreComponent : UnityEngine.MonoBehaviour, IInitialize
    {
        public bool IsInitialized { get; protected set; }
        public bool IsLateInitialized { get; protected set; }
        public int ComponentFinderID { get; protected set; }

        public readonly CoreEvent OnInitialize = new CoreEvent();
        public readonly CoreEvent OnLateInitialize = new CoreEvent();

        public readonly CoreEvent OnAwake = new CoreEvent();
        public readonly CoreEvent OnStart = new CoreEvent();

        public readonly CoreEvent OnEnabled = new CoreEvent();
        public readonly CoreEvent OnDisabled = new CoreEvent();

        public readonly CoreEvent OnReset = new CoreEvent();



        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
            OnInitialize?.Invoke();
        }

        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
                return;

            IsLateInitialized = true;
            OnLateInitialize?.Invoke();
        }

        protected virtual void Awake()
        {
            ComponentFinderID = CoreComponentFinder.AddCoreComponent(this);
            OnAwake?.Invoke();
        }

        protected virtual void Start()
        {
            OnStart?.Invoke();
        }

        protected virtual void OnEnable()
        {
            OnEnabled.Invoke();
        }

        protected virtual void OnDisable()
        {
            OnDisabled?.Invoke();
        }

        public virtual void Reset()
        {
            IsInitialized = false;
            OnReset?.Invoke();
        }
    }
}