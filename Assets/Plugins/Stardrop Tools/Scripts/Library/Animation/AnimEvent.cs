
namespace StardropTools
{
    [System.Serializable]
    public class AnimEvent : CoreComponent
    {
        [UnityEngine.SerializeField] SimpleAnimation sourceAnimation;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] string evenName;
        
        public string EventName { get => evenName; }

        public void InvokeEvent(int eventID)
        {
            sourceAnimation.AnimEvent(eventID);
            UnityEngine.Debug.LogFormat("<color=cyan> Event: {0} triggered! </color>", eventID);
        }

        public void InvokeEvent(float eventID)
        {
            sourceAnimation.AnimEvent(eventID);
            UnityEngine.Debug.LogFormat("<color=cyan> Event: {0} triggered! </color>", eventID);
        }

        public void InvokeEvent(string eventID)
        {
            sourceAnimation.AnimEvent(eventID);
            UnityEngine.Debug.LogFormat("<color=cyan> Event: {0} triggered! </color>", eventID);
        }
    }
}
