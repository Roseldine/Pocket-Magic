

public class WaitForSecondsCache
{
    public System.Collections.Generic.Dictionary<float, UnityEngine.WaitForSeconds> poolableLifetimeDictionary = new System.Collections.Generic.Dictionary<float, UnityEngine.WaitForSeconds>();
    
    public UnityEngine.WaitForSeconds GetWait(float time)
    {
        if (poolableLifetimeDictionary.TryGetValue(time, out var wait))
            return wait;

        poolableLifetimeDictionary[time] = new UnityEngine.WaitForSeconds(time);
        return poolableLifetimeDictionary[time];
    }
}