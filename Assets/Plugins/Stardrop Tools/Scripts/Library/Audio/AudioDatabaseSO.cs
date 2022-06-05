
using UnityEngine;

namespace StardropTools.Audio
{
    [CreateAssetMenu(menuName = "Stardrop / Audio / Audio Database")]
    public class AudioDatabaseSO : ScriptableObject
    {
        [SerializeField] AudioClip[] clips;
        [SerializeField] float minPitch = 1;
        [SerializeField] float maxPitch = 1.2f;
        [Space]
        [SerializeField] bool clearClips;

        public AudioClip[] Clips { get => clips; }
        public float MinPitch { get => minPitch; }
        public float MaxPitch { get => maxPitch; }


        public AudioClip GetRandomClip() => clips[Random.Range(0, clips.Length)];

        public AudioClip GetClipAtIndex(int index) => clips[index];

        public float GetRandomPitch() => Random.Range(minPitch, maxPitch);

        public void OnValidate()
        {
            if (clearClips)
            {
                clips = new AudioClip[0];
                clearClips = false;
            }
        }
    }
}