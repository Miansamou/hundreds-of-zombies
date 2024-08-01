using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace UI
{
    public class InterfaceControl : MonoBehaviour
    {

        public List<Image> staminas;
        public List<Image> bullets;
        public LocalizeStringEvent endText;
        public TMP_Text recordText;
        private PlayerActions player;
        public GameObject end;
        private GameObject[] buttons;
        private GameFlow flow;
        
        private void Start()
        {
            flow = GameObject.Find("GameFlow").GetComponent<GameFlow>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
            buttons = GameObject.FindGameObjectsWithTag("Button");
            UpdateStaminaUI();
            UpdateBulletUI();
        }

        public void UpdateStaminaUI() 
        {
            for (var i = 0; i < staminas.Count; i++)
            {
                var stamina = staminas[i];
                stamina.color = i <= player.GetStamina() - 1 ? Color.yellow : Color.gray;
            }
        }

        public void UpdateBulletUI()
        {
            for (var i = 0; i < bullets.Count; i++)
            {
                var bullet = bullets[i];
                bullet.gameObject.SetActive(i <= player.GetBullet() - 1);
            }
        }

        public void DesactiveButtons()
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Button>().interactable = false;
            }
        }

        public IEnumerator WaitAction()
        {
            yield return new WaitForSeconds(1.5f);

            foreach(var button in buttons)
            {
                button.GetComponent<Button>().interactable = true;
            }

            flow.Status(player.GetEnemyKilled());
            DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("YourTurn"));
        }
        
        public void SetWinText()
        {
            endText.SetEntry("Win");
        }
        
        public void SetLoseText()
        {
            endText.SetEntry("Lose");
        }

        public void EndGame()
        {
            end.SetActive(true);
        }
    }
}
