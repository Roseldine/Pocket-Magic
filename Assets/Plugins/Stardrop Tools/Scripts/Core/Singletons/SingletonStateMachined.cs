
using UnityEngine;
using StardropTools.FiniteStateMachine.EventFiniteStateMachine;

[RequireComponent(typeof(EventStateMachine))]
public abstract class SingletonStateMachined<T> : MonoBehaviour where T : MonoBehaviour, IEventStateMachined
{
	/// <summary>
	/// The instance.
	/// </summary>
	private static T instance;


	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	void SingletonInitialization()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}

		else
			Destroy(gameObject);
	}


	private void Awake()
	{
		SingletonInitialization();
	}

	#region Event State Machine

	[SerializeField] protected EventStateMachine eStateMachine;
	public EventState CurrentState { get => eStateMachine.CurrentState; }
	public virtual EventState GetState(int index) => eStateMachine.GetState(index);

	public CoreEvent SyncEventEnter(int stateIndex)
	=> GetState(stateIndex).OnStateEnter;
	public CoreEvent SyncEventExit(int stateIndex)
		=> GetState(stateIndex).OnStateExit;
	public CoreEvent SyncEventUpdate(int stateIndex)
		=> GetState(stateIndex).OnStateUpdate;

	public virtual void ChangeState(int stateIndex) => eStateMachine.ChangeState(stateIndex);

	protected virtual void OnValidate()
	{
		if (eStateMachine == null)
			eStateMachine = GetComponent<EventStateMachine>();
	}
	#endregion // event fsm
}