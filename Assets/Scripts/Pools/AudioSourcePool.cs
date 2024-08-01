using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public class AudioSourcePool : MonoBehaviour
    {
        public GameObject sfxPrefab;
        private readonly Queue<GameObject> audioSources = new();
        
        #region Singleton

        public static AudioSourcePool Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion

        public void QueueAudioSource(GameObject obj)
        {
            obj.SetActive(false);
            audioSources.Enqueue(obj);
        }

        public AudioSource GetAudioSource()
        {
            GameObject source;
            if (audioSources.Count > 0)
            {
                source = audioSources.Dequeue();
                source.SetActive(true);
            }
            else
            {
                source = Instantiate(sfxPrefab);
                source.SetActive(true);
            }
            return source.GetComponent<AudioSource>();
        }
    }
}

