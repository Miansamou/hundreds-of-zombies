using Pools;
using Sounds;
using UnityEngine;

namespace Sounds
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private bool muteSound;
        private Sound currentMusic;
        private AudioSource musicAudioSource;
        
        private void Awake()
        {

            if (Instance == null)
            {
                Instance = this;
            }
            else 
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            musicAudioSource = GetComponent<AudioSource>();
        }
        
        private void PlayMusic(Sound music)
        {
            music.source = musicAudioSource;
            music.source.clip = music.clip;

            music.source.volume = music.volume;
            music.source.pitch = music.pitch;
            music.source.loop = music.loop;
            music.source.Play();
        }

        private void PlaySfx(Sound sfx)
        {
            sfx.source = AudioSourcePool.Instance.GetAudioSource();
            sfx.source.clip = sfx.clip;
            sfx.source.mute = muteSound;
            
            sfx.source.volume = sfx.volume;
            sfx.source.pitch = sfx.pitch;
            sfx.source.loop = sfx.loop;
            sfx.source.Play();
        }

        public void Play (Sound sound)
        {
            if (sound.type == SoundType.Music)
            {
                PlayMusic(sound);
            }
            else
            {
                PlaySfx(sound);
            }
        }

        public void SetMusic(bool isOn)
        {
            currentMusic.source.mute = !isOn;
        }

        public void SetSound(bool isOn)
        {
            muteSound = !isOn;
        }
    }
}