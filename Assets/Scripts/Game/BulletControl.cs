using Sounds;
using UnityEngine;

namespace Game
{
    public class BulletControl : MonoBehaviour
    {
        public Sound shoot;
        public Sound zombieDeath;

        private PlayerActions player;
        private Rigidbody2D rb2d;
        private float velocityX;
        private float velocityY;
        private GameFlow flow;
        private int dice;
        
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
            flow = GameObject.Find("GameFlow").GetComponent<GameFlow>();
            rb2d = GetComponent<Rigidbody2D>();
            DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("Shoot"));
            AudioManager.Instance.Play(shoot);
        }
        
        private void FixedUpdate()
        {
            rb2d.velocity += new Vector2(velocityX, velocityY) * Time.deltaTime;
        }

        public void SetDirection(int velX, int velY)
        {
            velocityX = velX;
            velocityY = velY;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Zombie":
                    dice = Random.Range(1, 7);
                    switch (dice)
                    {
                        case 1:
                            DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("CriticalError"));
                            Destroy(gameObject);
                            break;
                        case 6:
                            DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("CriticalStrike"));
                            KillZombie(collision);
                            break;
                        default:
                        {
                            if (dice + player.GetStamina() >= 6)
                            {
                                DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("HitEnemy"));
                                KillZombie(collision);
                                Destroy(gameObject);  
                            }
                            else
                            {
                                DataLog.Instance.SendMessagesToLog(LocalizationLog.Instance.GetStringByKey("Miss"));
                            }
                            break;
                        }
                    }
                    break;
                case "Column":
                    Destroy(gameObject);
                    break;
            }
        }

        private void KillZombie(Collider2D collision)
        {
            player.SetEnemyKilled();
            flow.Status(player.GetEnemyKilled());
            AudioManager.Instance.Play(zombieDeath);
            Destroy(collision.gameObject);
        }
    }
}
