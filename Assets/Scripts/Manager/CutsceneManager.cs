using Sounds;
using UI;
using UnityEngine;

namespace Manager
{
    public class CutsceneManager : MonoBehaviour
    {
        public Sound cutsceneMusic;
        public Animator cam;
        public GameObject playBtn;

        public ShowText rioText;
        public ShowText virusText;
        public ShowText zombiesText;
        public ShowText worldText;
        public ShowText dougalsText;

        private static readonly int OnVirus = Animator.StringToHash("OnVirus");
        private static readonly int OnZombies = Animator.StringToHash("OnZombies");
        private static readonly int OnWorld = Animator.StringToHash("OnWorld");
        private static readonly int OnDouglas = Animator.StringToHash("OnDouglas");

        private float interval = .5f;

        private void Start()
        {
            AudioManager.Instance.Play(cutsceneMusic);
        }

        private void Update()
        {
            if (interval > 0)
            {
                interval -= Time.deltaTime;
            }
        }

        public void NextButton()
        {
            if (VerifyAnimationExcecution(OnVirus, rioText)) return;
            if (VerifyAnimationExcecution(OnZombies, virusText)) return;
            if (VerifyAnimationExcecution(OnWorld, zombiesText)) return;
            if (VerifyAnimationExcecution(OnDouglas, worldText)) return;
            if (!dougalsText.FinishedText()) dougalsText.ShowAllText();
            playBtn.SetActive(true);
        }

        private bool VerifyAnimationExcecution(int keyAnimation, ShowText text)
        {
            if (interval > 0) return true;
            if (cam.GetBool(keyAnimation)) return false;
            if (text.FinishedText())
            {
                interval = .5f;
                cam.SetBool(keyAnimation, true);
                return true;
            }
            text.ShowAllText();
            return true;
        }
    }
}
