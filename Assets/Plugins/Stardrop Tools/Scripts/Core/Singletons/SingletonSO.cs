
using UnityEngine;

/// <summary>
/// Requires a reference in another script, doesn't matter which or where
/// </summary>
public abstract class SingletonSO<T> : ScriptableObject where T : ScriptableObject
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
				T[] results = Resources.FindObjectsOfTypeAll<T>();

				if (results.Length == 0)
                {
					Debug.LogError("SingletonSO: results legnth is 0 of " + typeof(T).ToString());
					return null;
                }

				if (results.Length > 1)
                {
					Debug.LogError("SingletonSO: results legnth is greater than 1 of " + typeof(T).ToString());
					return null;
				}

				instance = results[0];
				instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
			}

			return instance;
		}
	}
}
