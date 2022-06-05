
using UnityEngine;

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    public class EventStateMachineComponent : MonoBehaviour, IStateMachine
    {
        [SerializeField] EventStateMachine stateMachine;
        public EventStateMachine EventStateMachine { get => stateMachine; set => stateMachine = value; }

        public bool IsInitialized { get; private set; }
        public EventState CurrentState { get => stateMachine.CurrentState; }
        public float TimeInCurrentState { get => stateMachine.CurrentState.TimeInState; }
        public int StateCount { get { if (stateMachine != null) return stateMachine.StateCount; else return 0; } }
        public EventState GetState(int stateIndex) => stateMachine.GetState(stateIndex);

        public CoreEvent<int> OnStateEnter { get => stateMachine.OnStateEnter; }
        public CoreEvent<int> OnStateExit { get => stateMachine.OnStateEnter; }
        public CoreEvent<int> OnStateUpdate { get => stateMachine.OnStateEnter; }
        public CoreEvent<int> OnStatePause { get => stateMachine.OnStateEnter; }
        public CoreEvent<int> OnStateResume { get => stateMachine.OnStateEnter; }


        public void Initialize()
            => stateMachine.Initialize();

        public void UpdateStateMachine()
            => stateMachine.UpdateStateMachine();

        public void ChangeState(int nextStateID)
            => stateMachine.ChangeState(nextStateID);

        public void InitializeStates()
            => stateMachine.InitializeStates();

        public void NextState()
            => stateMachine.NextState();

        public void PreviousState()
            => stateMachine.PreviousState();

        public void PauseState()
            => stateMachine.PauseState();

        public void ResumeState()
            => stateMachine.ResumeState();

        public void UpdateStateIDs()
            => stateMachine.UpdateStateIDs();

        public void AddState()
            => stateMachine.AddState();

        public void AddState(string stateName)
            => stateMachine.AddState(stateName);

        public void AddState(EventState state)
            => stateMachine.AddState(state);

        public void AddStates(EventState[] states)
            => stateMachine.AddStates(states);

        public void RemoveState()
            => stateMachine.RemoveState();

        public void RemoveState(int id)
            => stateMachine.RemoveState(id);

        public void RemoveState(EventState state)
            => stateMachine.RemoveState(state);

        public void ClearStates()
            => stateMachine.ClearStates();

        private void OnValidate()
        {
            if (StateCount > 0)
                stateMachine.UpdateStateIDs();
        }
    }
}