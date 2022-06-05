

namespace StardropTools.FiniteStateMachine.StateMachineSO
{
    public abstract class AbstractStateSO : UnityEngine.ScriptableObject, IState
    {
        [UnityEngine.SerializeField]                      protected string stateName;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int stateID;
        [UnityEngine.Space]
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected float maxTime;
        [UnityEngine.Space]
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int nextStateID = -1;

        FiniteStateMachineSO stateMachine;
        IState.ExecutionPhaseEnum prevExecutionState;

        public int StateID { get => stateID; set => stateID = value; }
        public float MaxTime { get => maxTime; set => maxTime = value; }
        public int NextStateID { get => nextStateID; set => nextStateID = value; }

        public bool IsInitialized { get; private set; }
        public IState.ExecutionPhaseEnum ExecutionPhase { get; protected set; }

        public virtual void Initialize(FiniteStateMachineSO fsm, int id)
        {
            if (IsInitialized)
                return;

            stateID = id;
            stateMachine = fsm;
            IsInitialized = true;
        }

        public virtual bool EnterState()
        {
            // return if already entered
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Entering)
                return false;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Entering);
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

        public virtual void HandleInput(float timeInState)
        {
            // return if Paused or Completed
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Paused || ExecutionPhase == IState.ExecutionPhaseEnum.Completed || ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;
        }

        public void UpdateState()
        {
            return;
        }

        public virtual void UpdateState(float timeInState)
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
        }
    }
}