

public static class WaitForSecondsManager
{
    public static System.Collections.Generic.Dictionary<string, WaitForSecondsCache> waitForSecondsDictionary = new System.Collections.Generic.Dictionary<string, WaitForSecondsCache>();

    public static UnityEngine.WaitForSeconds GetWait(string key, float time)
    {
        if (waitForSecondsDictionary.TryGetValue(key, out var waitCache))
            return waitCache.GetWait(time);

        waitForSecondsDictionary[key] = new WaitForSecondsCache();
        return waitForSecondsDictionary[key].GetWait(time);
    }
}