
using UnityEngine;

namespace StardropTools.AI
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class AIAgent : CoreObject
    {
        public enum NavAgentState { disabled, idle, moving, }

        [Header("Nav Agent")]
        [SerializeField] NavAgentState state;
        [SerializeField] protected UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] bool agentEnabled;
        
        public bool Enabled { get => agentEnabled; set => EnableAgent(value); }
        public bool IsOnNavmesh { get => agent.isOnNavMesh; }

        public readonly CoreEvent OnEnableAgent = new CoreEvent();
        public readonly CoreEvent OnDisableAgent = new CoreEvent();

        public readonly CoreEvent OnSetDestination = new CoreEvent();
        public readonly CoreEvent OnStopAgent = new CoreEvent();
        public readonly CoreEvent OnResumeAgent = new CoreEvent();
        public readonly CoreEvent OnPauseAgent = new CoreEvent();


        public override void UpdateObject()
        {
            base.UpdateObject();


        }

        protected bool CheckIfValid()
        {
            if (Enabled == false)
            {
                Debug.Log("Agent NOT ENABLED!");
                return false;
            }

            if (IsOnNavmesh == false)
            {
                Debug.Log("Agent NOT ON NAVMESH!");
                return false;
            }

            if (agent.hasPath == false)
            {
                Debug.Log("Agent NO PATH!");
                return false;
            }

            else
                return true;
        }

        public void EnableAgent(bool value)
        {
            if (CheckIfValid() == false)
                return;

            agent.enabled = value;
            agent.isStopped = value;

            agentEnabled = value;

            if (value) OnEnableAgent?.Invoke();
            else OnDisableAgent?.Invoke();
        }

        public void SetDestination(Vector3 destination)
        {
            if (CheckIfValid() == false)
                return;

            agent.SetDestination(destination);

            OnSetDestination?.Invoke();
        }

        public void SetVelocity(Vector3 velocity)
        {
            if (CheckIfValid() == false)
                return;

            agent.velocity = velocity;
        }


        public void StopAgent()
        {
            if (CheckIfValid() == false)
                return;

            EnableAgent(false);
            agent.ResetPath();

            OnStopAgent?.Invoke();
        }

        public void PauseAgent()
        {
            if (CheckIfValid() == false)
                return;

            agent.isStopped = true;

            OnPauseAgent?.Invoke();
        }

        public void ResumeAgent()
        {
            if (CheckIfValid() == false)
                return;

            agent.isStopped = false;

            if (agent.destination != null)
                SetDestination(agent.destination);

            OnResumeAgent?.Invoke();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (agent == null)
                agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
    }
}


