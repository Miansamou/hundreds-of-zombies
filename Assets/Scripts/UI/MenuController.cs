using UnityEngine;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        private Animator anim;
        private static readonly int MainMenu = Animator.StringToHash("MainMenu");

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void EntryMainMenu()
        {
            anim.SetBool(MainMenu, true);
        }
        
        public void ExitMainMenu()
        {
            anim.SetBool(MainMenu, false);
        }
    }
}
