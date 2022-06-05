
namespace StardropTools.FiniteStateMachine
{
    public interface IStateMachine
    {
        public void Initialize();
        public void UpdateStateMachine();
        public void ChangeState(int nextStateID);
        public void NextState();
        public void PreviousState();
        public void PauseState();
        public void ResumeState();

        public void InitializeStates();
        public void UpdateStateIDs();
        public void AddState(); // just as reminder
        public void RemoveState(); // just as reminder
    }
}