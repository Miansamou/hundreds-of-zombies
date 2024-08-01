using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class SceneGameManager : MonoBehaviour
    {
        private static SceneGameManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
    
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadGame(int num)
        {
            SceneManager.LoadScene(num);
        }
    }
}
