
using UnityEngine;

namespace StardropTools
{
    public class SimpleAnimation : CoreComponent
    {
        [Header("Animation")]
        [SerializeField] Animator animator;
        [SerializeField] AnimState[] animStates;
        [Min(0)] [SerializeField] float overralCrossFadeTime = 0;
        [SerializeField] float normalizedTime;
        [SerializeField] float realTime;
        [Space]
        [SerializeField] string[] animatorTriggers;
        [Space]
        [SerializeField] bool getAnimStates;
        [SerializeField] bool clearAnimStates;

        Coroutine animTimeCR;

        public int StateCount { get => animStates.Length; }
        public AnimState CurrentState { get => animStates[currentAnimID]; }
        public int currentAnimID;

        public readonly CoreEvent<int> OnAnimStartID = new CoreEvent<int>();
        public readonly CoreEvent<int> OnAnimCompleteID = new CoreEvent<int>();

        public readonly CoreEvent<int> OnAnimEventInt = new CoreEvent<int>();
        public readonly CoreEvent<float> OnAnimEventFloat = new CoreEvent<float>();
        public readonly CoreEvent<string> OnAnimEventString = new CoreEvent<string>();

        public readonly CoreEvent<AnimState> OnAnimStart = new CoreEvent<AnimState>();
        public readonly CoreEvent<AnimState> OnAnimComplete = new CoreEvent<AnimState>();
        public readonly CoreEvent<AnimState> OnAnimEvent = new CoreEvent<AnimState>();

        AnimatorStateInfo stateInfo;
        string lastTrigger;

        public AnimState GetAnimState(int animID)
        {
            if (animID < 0 || animID > animStates.Length)
            {
                Debug.Log("Anim State out of bounds");
                return null;
            }

            else
                return animStates[animID];
        }

        public AnimState GetAnimState(string animStateName)
        {
            AnimState state = null;

            for (int i = 0; i < animStates.Length; i++)
            {
                var tempState = GetAnimState(i);

                if (tempState.StateName[0] == animStateName[0] &&
                    tempState.StateName.Equals(animStateName))
                {
                    state = tempState;
                    break;
                }
            }

            if (state == null)
                Debug.Log("No anim state with name: " + animStateName + ", was found!");

            return state;
        }

        public void AnimEvent(int eventID)
            => OnAnimEventInt?.Invoke(eventID);
        public void AnimEvent(float eventFloat)
            => OnAnimEventFloat?.Invoke(eventFloat);
        public void AnimEvent(string eventString)
            => OnAnimEventString?.Invoke(eventString);

        int GetStateIDViaAnimator(AnimatorStateInfo info)
        {
            int stateID = -1;

            for (int i = 0; i < animStates.Length; i++)
                if (info.IsName(animStates[i].StateName))
                {
                    stateID = i;
                    break;
                }

            return stateID;
        }


        public void RefreshAnimState()
        {
            if (animator.GetCurrentAnimatorStateInfo(CurrentState.Layer).IsName(CurrentState.StateName) == false)
            {
                animator.Play(CurrentState.StateName, CurrentState.Layer);
                //Debug.Log("Refeshed Anim!");
            }
        }

        public void SetAnimationTrigger(int animID, bool resetTrigger, bool inAnimState = false)
        { 
            var trigger = "";

            if (inAnimState)
                trigger = animStates[animID].ParamTrigger;
            else
                trigger = animatorTriggers[animID];

            animator.SetTrigger(trigger);
            lastTrigger = trigger;

            if (resetTrigger)
                Invoke(nameof(ResetLastTrigger), .1f);
        }

        void ResetLastTrigger()
            => animator.ResetTrigger(lastTrigger);


        public void PlayAnimation(int animID, bool forceAnimation = false, bool disableOnFinish = false)
        {
            if (forceAnimation == false && animID == currentAnimID)
                return;

            if (animID >= 0 && animID < animStates.Length)
            {
                var targetState = animStates[animID];

                if (animator.enabled == false)
                    animator.enabled = true;

                animator.Play(targetState.StateName, targetState.Layer);
                RefreshAnimState();

                AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
                currentAnimID = animID;

                OnAnimStartID?.Invoke(currentAnimID);
                OnAnimStart?.Invoke(CurrentState);
            }

            else
                Debug.LogFormat("Animation ID: {0}, does not exist", animID);
        }

        
        public void CrossFadeAnimation(int animID, bool forceAnimation = false, bool disableOnFinish = false)
        {
            if (forceAnimation == false && animID == currentAnimID)
                return;

            if (animID >= 0 && animID < animStates.Length)
            {
                var targetState = animStates[animID];

                if (animator.enabled == false)
                    animator.enabled = true;

                animator.CrossFade(targetState.StateName, targetState.crossfade);

                AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
                currentAnimID = animID;

                OnAnimStartID?.Invoke(currentAnimID);
                OnAnimStart?.Invoke(CurrentState);
            }

            else
                Debug.LogFormat("Animation ID: {0}, does not exist", animID);
        }

        void AnimationLifetime(float time, bool disableOnFinish)
        {
            if (isActiveAndEnabled == false)
                return;

            if (animTimeCR != null)
                StopCoroutine(animTimeCR);

            animTimeCR = StartCoroutine(AnimTimeCR(time, disableOnFinish));
        }

        System.Collections.IEnumerator AnimTimeCR(float time, bool disableOnFinish)
        {
            yield return WaitForSecondsManager.GetWait("animation", time);
            animator.enabled = disableOnFinish;
            OnAnimCompleteID?.Invoke(currentAnimID);
            OnAnimComplete?.Invoke(CurrentState);
        }

#if UNITY_EDITOR
        void GetAnimatorStates()
        {
            if (animator == null)
            {
                Debug.Log("Animator not found!");
                return;
            }

            // get triggers
            System.Collections.Generic.List<string> stringParams = new System.Collections.Generic.List<string>();
            for (int i = 0; i < animator.parameterCount; i++)
            {
                var param = animator.GetParameter(i);

                if (param.type == AnimatorControllerParameterType.Trigger)
                    stringParams.Add(param.name);
            }
            animatorTriggers = stringParams.ToArray();


            // Get a reference to the Animator Controller:
            UnityEditor.Animations.AnimatorController animController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;

            // Number of layers:
            int layerCount = animController.layers.Length;
            Debug.Log("Layer Count: " + layerCount);
            AnimationClip[] animClips = animator.runtimeAnimatorController.animationClips;
            Debug.Log("Clip count: " + animClips.Length);

            if (animClips.Length == 0)
            {
                Debug.Log("NO Animation Clips!");
                return;
            }

            var listAnimStates = new System.Collections.Generic.List<AnimState>();

            for (int layer = 0; layer < layerCount; layer++)
            {
                // Names of each layer:
                //Debug.LogFormat("Layer {0}: {1}", layer, animController.layers[layer].name);

                UnityEditor.Animations.AnimatorStateMachine animatorStateMachine = animController.layers[layer].stateMachine;
                UnityEditor.Animations.ChildAnimatorState[] states = animatorStateMachine.states;

                // loop through ChildAnimatorState && add to list
                for (int i = 0; i < states.Length; i++)
                {
                    var newState = new AnimState(states[i].state.name, layer, .15f, animClips[i].length);
                    listAnimStates.Add(newState);
                    Debug.Log("State: " + states[i].state.name);
                }
            }

            animStates = listAnimStates.ToArray();
        }


        private void OnValidate()
        {
            if (animator == null)
                animator = GetComponent<Animator>();

            if (getAnimStates)
            {
                GetAnimatorStates();
                getAnimStates = false;
            }

            if (clearAnimStates)
            {
                animStates = new AnimState[0];
                clearAnimStates = false;
            }

            if (overralCrossFadeTime > 0)
                foreach (AnimState animeState in animStates)
                    animeState.crossfade = overralCrossFadeTime;
        }
#endif
    }
}

