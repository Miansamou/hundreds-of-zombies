using Sounds;
using UI;
using UnityEngine;

namespace Game
{
    public class PlayerActions : Actions
    {
        public GameObject bullet;
        public Sound footstep;
        public Sound rotate;
        public Sound reload;

        private InterfaceControl gameInterface;
        private IdentifyObject mask;
        private int enemyKilled; 
        private int stamina = 7;
        private int bullets = 6;
        private GameFlow flow; 
        
        public readonly RaycastHit2D[] Ray = new RaycastHit2D[4];
        
        private void Start()
        {
            gameInterface = GameObject.FindGameObjectWithTag("Interface").GetComponent<InterfaceControl>();
            flow = GameObject.Find("GameFlow").GetComponent<GameFlow>();
            mask = gameObject.GetComponentInChildren<IdentifyObject>();
        }

        public void Rotate(int angle)
        {
            transform.Rotate(0, 0, angle);
            AudioManager.Instance.Play(rotate);
        }

        public void Move()
        {
            if (stamina >= 1 && mask.GetIdentify())
            {
                Moving(mask.GetIdentify());
                AudioManager.Instance.Play(footstep);
                stamina--;
                gameInterface.UpdateStaminaUI();
            }

            else if (!mask.GetIdentify() || stamina < 1)
            {
                DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("InvalidAction"));
            }
        }

        public void Fire()
        {
            if (stamina >= 2 && bullets >= 1)
            {
                bullets--;
                stamina -= 2;
                gameInterface.UpdateBulletUI();
                gameInterface.UpdateStaminaUI();

                var shoot= Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z));
            
                switch (transform.eulerAngles.z)
                {
                    case 0:
                        shoot.GetComponent<BulletControl>().SetDirection(3, 0);
                        break;
                    case 90:
                        shoot.GetComponent<BulletControl>().SetDirection(0, 3);
                        break;
                    case 180:
                        shoot.GetComponent<BulletControl>().SetDirection(-3, 0);
                        break;
                    case 270:
                        shoot.GetComponent<BulletControl>().SetDirection(0, -3);
                        break;
                }
            }
            else
            {
                DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("InvalidAction"));
            }       
        }

        public void Reload()
        {
            if (stamina >= 3)
            {
                AudioManager.Instance.Play(reload);
                bullets = 6;
                stamina -= 3;
                gameInterface.UpdateBulletUI();
                gameInterface.UpdateStaminaUI();
            }
            else
            {
                DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("InvalidAction"));
            }

        }

        public void SetEnemyKilled()
        {
            enemyKilled++;
        }

        public int GetStamina()
        {
            return stamina;
        }

        public int GetBullet()
        {
            return bullets;
        }

        public int GetEnemyKilled()
        {
            return enemyKilled;
        }

        public void EndTurn()
        {
            var position = transform.position;
            Ray[0] = Physics2D.Raycast(position, -Vector2.right, Mathf.Infinity, 1 << 9);
            Ray[1] = Physics2D.Raycast(position, -Vector2.up, Mathf.Infinity, 1 << 9);
            Ray[2] = Physics2D.Raycast(position, Vector2.right, Mathf.Infinity, 1 << 9);
            Ray[3] = Physics2D.Raycast(position, Vector2.up, Mathf.Infinity, 1 << 9);

            flow.UpdateZombies();

            stamina += 2;

            if(stamina > 7)
            {
                stamina = 7;
            }

            gameInterface.UpdateStaminaUI();
            flow.RollDice();
            gameInterface.DesactiveButtons();
            StartCoroutine(gameInterface.WaitAction());
        }
    }
}
