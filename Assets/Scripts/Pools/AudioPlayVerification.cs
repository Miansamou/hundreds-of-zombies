using UnityEngine;

namespace Pools
{
    public class AudioPlayVerification : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (audioSource.isPlaying) return;
            AudioSourcePool.Instance.QueueAudioSource(gameObject);
        }
    }
}

