

namespace StardropTools.FiniteStateMachine.StateMachineComponent
{
    #region Copy Body
    /*
    public override bool EnterState()
    {
        base.EnterState();
    
        return true;
    }
    
    
    public override bool ExitState()
    {
        base.ExitState();
    
        return true;
    }
    
    
    public override void HandleInput()
    {
        base.HandleInput();
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
    }
    
    
    public override void PauseState()
    {
        base.PauseState();
    }
    */
    #endregion // copy body

    //[CreateAssetMenu(fileName = "New State", menuName = "Finite State Machine / Create New State")]
    public abstract class AbstractStateComponent : UnityEngine.MonoBehaviour, IState
    {
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int stateID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected float timeInState;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected float maxTime;
        [UnityEngine.Space]
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int nextStateID = -1;

        FiniteStateMachineComponent stateMachine;
        IState.ExecutionPhaseEnum prevExecutionState;

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

        public virtual void Initialize(FiniteStateMachineComponent fsm, int id)
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
