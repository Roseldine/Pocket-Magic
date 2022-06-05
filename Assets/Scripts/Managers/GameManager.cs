
using StardropTools;
using StardropTools.FiniteStateMachine.EventFiniteStateMachine;

public class GameManager : StardropTools.Singletons.SingletonManagerStateMachined<GameManager>
{
    [UnityEngine.SerializeField] UnityEngine.Transform parentManagers;
    [UnityEngine.SerializeField] CoreManager[] managers;


    #region Events
    public CoreEvent OnInitializeEnter { get => GetState(0).OnStateEnter; }
    public CoreEvent OnInitializeUpdate { get => GetState(0).OnStateUpdate; }
    public CoreEvent OnInitializeExit { get => GetState(0).OnStateExit; }

    public CoreEvent OnIdleEnter { get => GetState(1).OnStateEnter; }
    public CoreEvent OnIdleUpdate { get => GetState(1).OnStateUpdate; }
    public CoreEvent OnIdleExit { get => GetState(1).OnStateExit; }

    public CoreEvent OnPlayEnter { get => GetState(2).OnStateEnter; }
    public CoreEvent OnPlayUpdate { get => GetState(2).OnStateUpdate; }
    public CoreEvent OnPlayExit { get => GetState(2).OnStateExit; }

    public CoreEvent OnFinishEnter { get => GetState(3).OnStateEnter; }
    public CoreEvent OnFinishUpdate { get => GetState(3).OnStateUpdate; }
    public CoreEvent OnFinishExit { get => GetState(3).OnStateExit; }

    public CoreEvent OnPauseEnter { get => GetState(4).OnStateEnter; }
    public CoreEvent OnPauseUpdate { get => GetState(4).OnStateUpdate; }
    public CoreEvent OnPauseExit { get => GetState(4).OnStateExit; }

    #endregion // events


    public override void Initialize()
    {
        //CreateEventStates();
        SynceEvents();
        base.Initialize();

        LoopManager.OnUpdate.AddListener(eventStateMachine.UpdateStateMachine);

        GetManagers();
        InitializeManagers();
        LateInitializeManagers();
    }

    public override void LateInitialize()
    {
        base.LateInitialize();
        ChangeState(1); // change to idle
    }

    protected override void CreateEventStates()
    {
        var states = new EventState[5];

        states[0] = new EventState(0, "Initialization");
        states[1] = new EventState(1, "Idle");
        states[2] = new EventState(2, "Play");
        states[3] = new EventState(3, "Finish");
        states[4] = new EventState(4, "Pause");

        eventStateMachine.EventStateMachine = new EventStateMachine(states);
    }

    void SynceEvents()
    {
        if (IsInitialized)
            return;

        //OnInitializeEnter = SyncEnter(0);
        //OnInitializeUpdate = SyncUpdate(0);
        //OnInitializeExit = SyncExit(0);

        //OnIdleEnter = SyncEnter(1);
        //OnIdleUpdate = SyncUpdate(1);
        //OnIdleExit = SyncExit(1);
        //
        //OnPlayEnter = SyncEnter(2);
        //OnPlayUpdate = SyncUpdate(2);
        //OnPlayExit = SyncExit(2);
        //
        //OnFinishEnter = SyncEnter(3);
        //OnFinishUpdate = SyncUpdate(3);
        //OnFinishExit = SyncExit(3);
        //
        //OnPauseEnter = SyncEnter(4);
        //OnPauseUpdate = SyncUpdate(4);
        //OnPauseExit = SyncExit(4);
    }

    /// <summary>
    /// 0-initialization, 1-idle, 2-play, 3-finish, 4-pause
    /// </summary>
    public override void ChangeState(int nextStateID)
    {
        if (IsInitialized && IsLateInitialized && nextStateID == 0)
        {
            Print("Game Manager is already Initialized");
            return;
        }

        base.ChangeState(nextStateID);
    }


    void GetManagers()
        => managers = GetItems<CoreManager>(parentManagers);

    void InitializeManagers()
    {
        if (managers.Exists())
            for (int i = 0; i < managers.Length; i++)
                managers[i].Initialize();
    }

    void LateInitializeManagers()
    {
        if (managers.Exists())
            for (int i = 0; i < managers.Length; i++)
                managers[i].LateInitialize();
    }
}
