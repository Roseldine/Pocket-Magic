
namespace StardropTools.Audio
{
    [System.Serializable]
    public class AudioSourceDB
    {
        [UnityEngine.SerializeField] UnityEngine.AudioSource source;
        [UnityEngine.SerializeField] AudioDatabaseSO database;

        public UnityEngine.AudioSource Source { get => source; }
        public AudioDatabaseSO AudioDB { get => database; }
        public UnityEngine.AudioClip RandomClip { get => database.GetRandomClip(); }
        public float RandomPitch { get => database.GetRandomPitch(); }

        public UnityEngine.AudioClip GetClipAtIndex(int index) => database.GetClipAtIndex(index);

        #region Play Audio
        public void PlayRandom()
        {
            StopPlaying();
            source.clip = RandomClip;
            source.Play();
        }

        public void PlayRandomOneShot(bool randomPitch = false)
        {
            if (randomPitch)
                source.pitch = RandomPitch;

            source.PlayOneShot(RandomClip);
        }


        public void PlayClipAtIndex(int index, bool randomPitch = false)
        {
            StopPlaying();
            source.clip = database.GetClipAtIndex(index);

            if (randomPitch)
                source.pitch = RandomPitch;

            source.Play();
        }

        public void PlayClipOneShotAtIndex(int index, bool randomPitch)
        {
            if (randomPitch)
                source.pitch = RandomPitch;

            source.PlayOneShot(database.GetClipAtIndex(index));
        }

        public void StopPlaying()
        {
            if (source.isPlaying)
                source.Stop();
        }
        #endregion // Play audio

        public void SetVolume(float value) => source.volume = value;
        public void SetPitch(float value) => source.pitch = value;
    }
}