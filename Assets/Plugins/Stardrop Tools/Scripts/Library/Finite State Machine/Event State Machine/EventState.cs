

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    [System.Serializable]
    public class EventState : IState
    {
        [UnityEngine.SerializeField]                      protected string stateName;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int stateID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField]                      protected float timeInState;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected float maxTime;
        [UnityEngine.Space]
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int nextStateID = -1;

        EventStateMachine stateMachine;
        IState.ExecutionPhaseEnum prevExecutionState;

        public string StateName { get => stateName; set => stateName = value; }
        public int StateID { get => stateID; set => stateID = value; }
        public float MaxTime { get => maxTime; set => maxTime = value; }
        public int NextStateID { get => nextStateID; set => nextStateID = value; }

        public float TimeInState { get => timeInState; }

        public bool IsInitialized { get; private set; }
        public IState.ExecutionPhaseEnum ExecutionPhase { get; protected set; }


        public readonly CoreEvent OnStateInitialize = new CoreEvent();
        public readonly CoreEvent OnStateEnter = new CoreEvent();
        public readonly CoreEvent OnStateExit = new CoreEvent();
        public readonly CoreEvent OnStateInput = new CoreEvent();
        public readonly CoreEvent OnStateUpdate = new CoreEvent();
        public readonly CoreEvent OnStatePause = new CoreEvent();
        public readonly CoreEvent OnStateComplete = new CoreEvent();

        #region Constructor
        public EventState() { nextStateID = -1; }

        public EventState(int id)
        {
            stateID = id;
            nextStateID = -1;
        }

        public EventState(string name)
        {
            stateName = name;
            nextStateID = -1;
        }

        public EventState(int id, string name)
        {
            stateID = id;
            stateName = name;
            nextStateID = -1;
        }

        public EventState(int id, string name, float maxTime)
        {
            stateID = id;
            stateName = name;
            nextStateID = -1;
            this.maxTime = maxTime;
        }

        public EventState(int id, string name, int nextStateID)
        {
            stateID = id;
            stateName = name;
            this.nextStateID = nextStateID;
        }

        public EventState(int id, string name, int nextStateID, float maxTime)
        {
            stateID = id;
            stateName = name;
            this.nextStateID = nextStateID;
            this.maxTime = maxTime;
        }
        #endregion // constructor

        public virtual void Initialize(EventStateMachine fsm, int id)
        {
            if (IsInitialized)
                return;

            stateID = id;
            stateMachine = fsm;
            IsInitialized = true;

            OnStateInitialize?.Invoke();
        }

        public virtual bool EnterState()
        {
            // return if already entered
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Entering)
                return false;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Entering);
            timeInState = 0;
            return true;
        }

        public virtual bool ExitState()
        {
            // return if already exited
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return false;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Exited);
            return true;
        }

        public virtual void HandleInput()
        {
            // return if Paused or Completed
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Paused || ExecutionPhase == IState.ExecutionPhaseEnum.Completed || ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            OnStateInput?.Invoke();
        }

        public virtual void UpdateState()
        {
            // return if Paused or Completed
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Paused || ExecutionPhase == IState.ExecutionPhaseEnum.Completed || ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            // check if completion time reached
            if (maxTime > 0 && timeInState > maxTime)
            {
                if (nextStateID >= 0)
                    stateMachine.ChangeState(nextStateID);

                ChangeExecutionStage(IState.ExecutionPhaseEnum.Completed);
            }

            timeInState += UnityEngine.Time.deltaTime;

            OnStateUpdate?.Invoke();
        }

        public void PauseState()
        {
            // do nothing if state isn't updating
            if (ExecutionPhase != IState.ExecutionPhaseEnum.Updating)
                return;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Paused);
        }

        public void ResumeState()
        {
            // do nothing if state isn't paused
            if (ExecutionPhase != IState.ExecutionPhaseEnum.Paused)
                return;

            ChangeExecutionStage(prevExecutionState);
        }

        public void ChangeState(int stateID) => stateMachine.ChangeState(stateID);

        protected void ChangeExecutionStage(IState.ExecutionPhaseEnum phase)
        {
            if (ExecutionPhase == phase)
                return;

            ExecutionPhase = phase;
            prevExecutionState = ExecutionPhase;

            switch (phase)
            {
                case IState.ExecutionPhaseEnum.None:
                    break;

                case IState.ExecutionPhaseEnum.Entering:
                    OnStateEnter?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Exited:
                    OnStateExit?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Updating:
                    OnStateUpdate?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Paused:
                    OnStatePause?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Completed:
                    OnStateComplete?.Invoke();
                    break;
            }
        }
    }
}