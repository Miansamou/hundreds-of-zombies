using UnityEngine;

namespace Game
{
    public class IdentifyObjectZombie : MonoBehaviour
    {
        public GameObject zombie;
        private bool identify = true;

        public bool GetIdentify()
        {
            return identify;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Player":
                    identify = true;
                    break;
                case "Zombie":
                    identify = false;
                    switch (zombie.transform.eulerAngles.z)
                    {
                        case 0:
                            transform.position += new Vector3(1, 0, 0);
                            break;
                        case 90:
                            transform.position += new Vector3(0, 1, 0);
                            break;
                        case 180:
                            transform.position += new Vector3(-1, 0, 0);
                            break;
                        case 270:
                            transform.position += new Vector3(0, -1, 0);
                            break;
                    }
                    break;
                case "Column":
                    identify = false;
                    break;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Board"))
            {
                identify = true;
            }
        }

        public void InitialPosition()
        {
            var position = zombie.transform.position;
            transform.position = zombie.transform.eulerAngles.z switch
            {
                0 => new Vector3(position.x + 1, position.y, position.z),
                90 => new Vector3(position.x, position.y + 1, position.z),
                180 => new Vector3(position.x - 1, position.y, position.z),
                270 => new Vector3(position.x, position.y - 1, position.z),
                _ => transform.position
            };
        }
    }
}