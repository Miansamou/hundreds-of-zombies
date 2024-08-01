using UnityEngine;
using UnityEngine.Localization.Components;

namespace Game
{
    public class LocalizationLog : MonoBehaviour
    {

        public static LocalizationLog Instance;
        private LocalizeStringEvent localization;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            localization = GetComponent<LocalizeStringEvent>();
        }

        public string GetStringByKey(string key)
        {
            localization.SetEntry(key);
            return localization.StringReference.GetLocalizedString();
        }
    }
}
