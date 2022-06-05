
namespace StardropTools
{
    [System.Serializable]
    public class CoreEventTrigger : CoreEventTriggerBase
    {

        public static readonly CoreEvent OnEvent = new CoreEvent();


        public CoreEventTrigger() { }
        public CoreEventTrigger(string name) => eventName = name;
        public CoreEventTrigger(int id) => eventID = id;

        public CoreEventTrigger(string name, int id)
        {
            eventName = name;
            eventID = id;
        }

        public override void Invoke()
        {
            base.Invoke();
            OnEvent?.Invoke();
        }
    }

    [System.Serializable]
    public class CoreEventTrigger<T> : CoreEventTriggerBase
    {

        public static readonly CoreEvent<T> OnEvent = new CoreEvent<T>();


        public CoreEventTrigger() { }
        public CoreEventTrigger(string name) => eventName = name;
        public CoreEventTrigger(int id) => eventID = id;

        public CoreEventTrigger(string name, int id)
        {
            eventName = name;
            eventID = id;
        }

        public void Invoke(T type)
        {
            base.Invoke();
            OnEvent?.Invoke(type);
        }
    }
}

