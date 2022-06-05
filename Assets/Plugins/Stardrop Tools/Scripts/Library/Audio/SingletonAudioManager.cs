using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools;
using StardropTools.Audio;

public class SingletonAudioManager<T> : CoreManager where T : Component
{
    #region Manager Singleton
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


    public override void Initialize()
    {
        base.Initialize();

        SingletonInitialization();
    }
    #endregion // manager singleton

    public void PlayOneShotAtSource(AudioSource source, AudioClip clip, float pitch = 1)
    {
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }

    public void PlayRandomFromDatabse(AudioSourceDB audioSourceDB)
        => PlayOneShotAtSource(audioSourceDB.Source, audioSourceDB.RandomClip);

    public void PlayFromDatabase(AudioSourceDB audioSourceDB, int clipIndex)
        => PlayOneShotAtSource(audioSourceDB.Source, audioSourceDB.GetClipAtIndex(clipIndex));


    public void PlaySourceClip(AudioSourceClip sourceClip)
    {
        sourceClip.Play();
    }

    public void PlaySourceClipOneShot(AudioSourceClip sourceClip)
    {
        sourceClip.Source.PlayOneShot(sourceClip.Clip);
    }
}
