

public class LoopManager : Singleton<LoopManager>
{
    public bool IsInitialized { get; private set; }

    public static readonly CoreEvent OnAwake = new CoreEvent();
    public static readonly CoreEvent OnStart = new CoreEvent();
    public static readonly CoreEvent OnUpdate = new CoreEvent();
    public static readonly CoreEvent OnLateUpdate = new CoreEvent();
    public static readonly CoreEvent OnFixedUpdate = new CoreEvent();

    public static readonly CoreEvent OnEnabled = new CoreEvent();
    public static readonly CoreEvent OnDisabled = new CoreEvent();
    

    public void Initialize()
    {
        if (IsInitialized)
            return;

        IsInitialized = true;
    }


    private void Start() => OnStart?.Invoke();

    private void Update() => OnUpdate?.Invoke();

    private void FixedUpdate() => OnFixedUpdate?.Invoke();

    private void OnEnable() => OnEnabled?.Invoke();

    private void OnDisable() => OnDisabled?.Invoke();
}
