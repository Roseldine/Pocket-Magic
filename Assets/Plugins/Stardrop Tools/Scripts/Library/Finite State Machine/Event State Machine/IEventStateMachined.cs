

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    public interface IEventStateMachined
    {
		CoreEvent SyncEventEnter(int stateIndex);
		CoreEvent SyncEventExit(int stateIndex);
		CoreEvent SyncEventUpdate(int stateIndex);

		EventState GetState(int index);
		void ChangeState(int stateIndex);
	}
}