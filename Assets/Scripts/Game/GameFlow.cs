using Sounds;
using UI;
using UnityEngine;

namespace Game
{
    public class GameFlow : MonoBehaviour
    {
        public Sound zombieRoar;
        public Sound win;
        public Sound lose;
        public Sound gameMusic;

        private int diceResult;
        private PlayerActions player;
        private GameObject[] zombies;
        private InterfaceControl gameInterface;
        private float minTime;
        private int enemiesKilled;
        private bool timeOver;

        private void Start()
        {
            gameInterface = GameObject.FindGameObjectWithTag("Interface").GetComponent<InterfaceControl>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
            minTime = PlayerPrefs.GetFloat("MinTime");
            AudioManager.Instance.Play(gameMusic);
            UpdateZombies();
        }

        public void UpdateZombies()
        {
            zombies = GameObject.FindGameObjectsWithTag("Zombie");
        }

        public void RollDice()
        {
            AudioManager.Instance.Play(zombieRoar);

            DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("ZombieTurn"));

            diceResult = Random.Range(1, 7);

            switch (diceResult)
            {
                case 1:
                {
                    DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("Dice1"));
                    foreach (var zombie in zombies)
                    {
                        zombie.GetComponent<ZombieActions>().Rotate(90);
                    }

                    break;
                }
                case 2:
                {
                    DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("Dice2"));
                    foreach (var zombie in zombies)
                    {
                        zombie.GetComponent<ZombieActions>().Rotate(0);
                    }

                    break;
                }
                case 3:
                {
                    DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("Dice3"));
                    foreach (var zombie in zombies)
                    {
                        zombie.GetComponent<ZombieActions>().Rotate(270);
                    }

                    break;
                }
                case 4:
                {
                    DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("Dice4"));
                    foreach (var zombie in zombies)
                    {
                        zombie.GetComponent<ZombieActions>().Rotate(180);
                    }

                    break;
                }
            }

            if (diceResult != 5 && diceResult != 6) return;
            DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("Dice") + diceResult +
                                               LocalizationLog.Instance.GetStringByKey("Dice56"));
            for (int i = 0, angle = 0; i <= 3; i++, angle += 90)
            {
                if (player.Ray[i].collider != null)
                {
                    player.Ray[i].collider.GetComponent<ZombieActions>().Rotate(angle);
                }
            }
        }

        public void EndTimer()
        {
            timeOver = true;
        }

        public void Status(int enemies = -1)
        {
            if (enemies > 0)
            {
                enemiesKilled = enemies;
            }

            UpdateZombies();
            string newTime;
            var min = (int)Time.timeSinceLevelLoad / 60;
            var seg = (int)Time.timeSinceLevelLoad % 60;
            var currentTime = LocalizationLog.Instance.GetStringByKey("Time") + AdjustMinute(min) + ":" +
                              AdjustMinute(seg);

            if (enemiesKilled >= 24)
            {
                if (Time.timeSinceLevelLoad < minTime || minTime == 0)
                {
                    minTime = Time.timeSinceLevelLoad;
                    PlayerPrefs.SetFloat("MinTime", minTime);
                }

                min = (int)minTime / 60;
                seg = (int)minTime % 60;
                newTime = LocalizationLog.Instance.GetStringByKey("Record") + AdjustMinute(min) + ":" +
                          AdjustMinute(seg);

                AudioManager.Instance.Play(win);
                gameInterface.DesactiveButtons();
                gameInterface.SetWinText();
                gameInterface.recordText.text = currentTime + "\n" + newTime;
                gameInterface.EndGame();
            }

            foreach (var zombie in zombies)
            {
                if (zombie.transform.position != player.transform.position && !timeOver) continue;
                if (minTime == 0)
                {
                    newTime = LocalizationLog.Instance.GetStringByKey("Record") + "--:--";
                }
                else
                {
                    min = (int)minTime / 60;
                    seg = (int)minTime % 60;
                    newTime = LocalizationLog.Instance.GetStringByKey("Record") + AdjustMinute(min) + ":" +
                              AdjustMinute(seg);
                }

                AudioManager.Instance.Play(lose);
                gameInterface.DesactiveButtons();
                gameInterface.SetLoseText();
                gameInterface.recordText.text = currentTime + "\n" + newTime;
                gameInterface.EndGame();
            }
        }

        private static string AdjustMinute(int number)
        {
            string newNumber;

            if (number < 10)
            {
                newNumber = "0" + number;
            }
            else
            {
                newNumber = number.ToString();
            }

            return newNumber;
        }
    }
}