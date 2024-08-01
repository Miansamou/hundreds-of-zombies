using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
    public class Sound : ScriptableObject
    {
        public AudioClip clip;
        public SoundType type;
        public bool loop;
        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;
        [HideInInspector]
        public AudioSource source;
    }
    
    public enum SoundType 
    {
        Music,
        Sfx
    }
}
