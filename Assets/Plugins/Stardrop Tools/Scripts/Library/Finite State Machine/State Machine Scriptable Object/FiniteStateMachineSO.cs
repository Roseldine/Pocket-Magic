

namespace StardropTools.FiniteStateMachine.StateMachineSO
{
    public class FiniteStateMachineSO : UnityEngine.MonoBehaviour, IStateMachine
    {
        public int fsmID = 0;
        [UnityEngine.SerializeField] int startStateID = 0;
        [UnityEngine.SerializeField] AbstractStateSO startState;
        [UnityEngine.SerializeField] AbstractStateSO currentState;
        [UnityEngine.SerializeField] AbstractStateSO previousState;
        [UnityEngine.SerializeField] System.Collections.Generic.List<AbstractStateSO> states;
        [UnityEngine.SerializeField] float timeInState;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool log;

        public bool IsInitialized { get; private set; }
        public AbstractStateSO CurrentState { get => currentState; }
        public float TimeInCurrentState { get => timeInState; }
        public AbstractStateSO GetState(int stateIndex) => states[stateIndex];

        public readonly CoreEvent<AbstractStateSO> OnStateEnter = new CoreEvent<AbstractStateSO>();
        public readonly CoreEvent<AbstractStateSO> OnStateExit = new CoreEvent<AbstractStateSO>();
        public readonly CoreEvent<AbstractStateSO> OnStateUpdate = new CoreEvent<AbstractStateSO>();
        public readonly CoreEvent<AbstractStateSO> OnStatePause = new CoreEvent<AbstractStateSO>();
        public readonly CoreEvent<AbstractStateSO> OnStateResume = new CoreEvent<AbstractStateSO>();

        public void Initialize()
        {
            if (IsInitialized)
                return;

            ChangeState(startState);
            InitializeStates();

            ChangeState(startStateID);
            IsInitialized = true;
        }

        public void UpdateStateMachine()
        {
            if (currentState != null)
            {
                currentState.HandleInput(timeInState);
                currentState.UpdateState(timeInState);

                OnStateUpdate?.Invoke(currentState);
            }
        }


        public virtual void ChangeState(int nextStateID)
        {
            if (currentState.StateID == nextStateID)
                return;

            var state = states[nextStateID];
            ChangeState(state);
        }


        public void ChangeState(AbstractStateSO nextState)
        {
            if (currentState == nextState)
                return;

            if (currentState != null)
            {
                currentState.ExitState();
                previousState = currentState;
                OnStateExit?.Invoke(currentState);
            }

            currentState = nextState;
            currentState.EnterState();
            OnStateEnter?.Invoke(currentState);

            if (log == true)
                UnityEngine.Debug.Log("<color=yellow> Changed to state: </color> <color=cyan>" + currentState.name + " (id: " + currentState.StateID + ")" + "</color>");
        }

        public void NextState() => ChangeState(currentState.NextStateID);

        public void PreviousState()
        {
            if (previousState != null)
                ChangeState(previousState);
        }

        public void PauseState()
        {
            currentState.PauseState();
            OnStatePause?.Invoke(currentState);
        }

        public void ResumeState()
        {
            currentState.ResumeState();
            OnStateResume?.Invoke(currentState);
        }

        public void InitializeStates()
        {
            if (states.Exists() == false)
                return;

            for (int i = 0; i < states.Count; i++)
                states[i].Initialize(this, i);
        }

        public void UpdateStateIDs()
        {
            if (states.Exists() == false)
                return;

            for (int i = 0; i < states.Count; i++)
                states[i].StateID = i;
        }

        public void AddState()
        {
            return;
        }

        public void AddState(AbstractStateSO state)
        {
            states.Add(state);
            UpdateStateIDs();
        }

        public void RemoveState()
        {
            return;
        }

        public void RemoveState(int id)
        {
            var state = GetState(id);
            states.RemoveSafe(state);
        }

        public void RemoveState(AbstractStateSO state)
            => states.RemoveSafe(state);
    }
}