using UnityEngine;

namespace Game
{
    public class IdentifyObject : MonoBehaviour
    {
        private bool identify = true;

        public bool GetIdentify()
        {
            return identify;
        }
   
        private void OnTriggerEnter2D(Collider2D collision)
        {
            identify = collision.gameObject.tag switch
            {
                "Zombie" => false,
                "Column" => false,
                _ => identify
            };
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            identify = collision.gameObject.tag switch
            {
                "Zombie" => true,
                "Column" => true,
                _ => identify
            };
        }
    }
}