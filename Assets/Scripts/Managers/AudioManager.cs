
using UnityEngine;
using StardropTools;
using StardropTools.Audio;

public class AudioManager : SingletonAudioManager<AudioManager>
{
    [TextArea][SerializeField] string descrition;
    [SerializeField] AudioSourceDB[] sourceDatabases;

    [Header("Music")]
    [SerializeField] AudioSourceClip musicStart;
    [SerializeField] AudioSourceClip musicLoop;

    public override void Initialize()
    {
        base.Initialize();
    }

    public void PlayHit()
        => PlayFromDatabase(sourceDatabases[0], 0);
    public void PlayDeath()
        => PlayFromDatabase(sourceDatabases[0], 1);
    public void PlayRandomized()
        => PlayFromDatabase(sourceDatabases[0], 2);
}
