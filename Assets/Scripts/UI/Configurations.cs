using System.Collections;
using Sounds;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace UI
{
    public class Configurations : MonoBehaviour
    {
        public Toggle musicToggle;
        public Toggle soundToggle;
        private bool changingLocale;
        private int currentLocale;
        // private static Configurations _instance;
        //
        // private void Awake()
        // {
        //     if (_instance == null)
        //     {
        //         _instance = this;
        //     }
        //     else 
        //     {
        //         Destroy(gameObject);
        //         return;
        //     }
        //     DontDestroyOnLoad(gameObject);
        // }
        
        private void Start()
        {
            StartCoroutine(GetLocale());
        }
        
        public void SetMusic()
        {
            AudioManager.Instance.SetMusic(musicToggle.isOn);
        }

        public void SetSound()
        {
            AudioManager.Instance.SetSound(soundToggle.isOn);
        }

        public void ChangeLocale()
        {
            if (changingLocale) return;
            StartCoroutine(SetLocale());
        }

        private IEnumerator SetLocale()
        {
            currentLocale++;
            if (currentLocale > 1) currentLocale = 0;
            changingLocale = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocale];
            changingLocale = false;
        }
        
        private IEnumerator GetLocale()
        {
            changingLocale = true;
            yield return LocalizationSettings.InitializationOperation;
            currentLocale = LocalizationSettings.SelectedLocale.GetInstanceID();
            changingLocale = false;
        }
    }
}
