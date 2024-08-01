using Sounds;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class ClickMainScreen : MonoBehaviour
    {
        public Sound menuMusic;
        public UnityEvent onClickScreen;

        private void Start()
        {
            AudioManager.Instance.Play(menuMusic);
        }
        
        private void Update()
        {
            if (Input.touches.Length <= 0 && !Input.GetMouseButtonDown(0)) return;
            onClickScreen.Invoke();
            gameObject.SetActive(false);
        }
    }
}
