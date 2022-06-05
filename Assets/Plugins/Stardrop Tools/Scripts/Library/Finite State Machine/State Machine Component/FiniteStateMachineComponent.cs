

namespace StardropTools.FiniteStateMachine.StateMachineComponent
{
    public class FiniteStateMachineComponent : UnityEngine.MonoBehaviour, IStateMachine
    {
        public int fsmID = 0;
        [UnityEngine.SerializeField] int startStateID = 0;
        [UnityEngine.SerializeField] AbstractStateComponent startState;
        [UnityEngine.SerializeField] AbstractStateComponent currentState;
        [UnityEngine.SerializeField] AbstractStateComponent previousState;
        [UnityEngine.SerializeField] System.Collections.Generic.List<AbstractStateComponent> states;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool log;

        public bool IsInitialized { get; private set; }
        public AbstractStateComponent CurrentState { get => currentState; }
        public float TimeInCurrentState { get => currentState.TimeInState; }
        public AbstractStateComponent GetState(int stateIndex) => states[stateIndex];

        public readonly CoreEvent<AbstractStateComponent> OnStateEnter = new CoreEvent<AbstractStateComponent>();
        public readonly CoreEvent<AbstractStateComponent> OnStateExit = new CoreEvent<AbstractStateComponent>();
        public readonly CoreEvent<AbstractStateComponent> OnStateUpdate = new CoreEvent<AbstractStateComponent>();
        public readonly CoreEvent<AbstractStateComponent> OnStatePause = new CoreEvent<AbstractStateComponent>();
        public readonly CoreEvent<AbstractStateComponent> OnStateResume = new CoreEvent<AbstractStateComponent>();

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
                currentState.HandleInput();
                currentState.UpdateState();

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


        public void ChangeState(AbstractStateComponent nextState)
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

        public void AddState(AbstractStateComponent state)
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

        public void RemoveState(AbstractStateComponent state)
            => states.RemoveSafe(state);
    }
}
