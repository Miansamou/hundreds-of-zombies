using System.Collections;
using UnityEngine;

namespace Game
{
    public class ZombieActions : Actions
    {
        private IdentifyObjectZombie mask;
    
        private void Start()
        {
            mask = gameObject.GetComponentInChildren<IdentifyObjectZombie>();
        }

        public void Rotate(int angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);

            mask.InitialPosition();

            StartCoroutine(WaitAction());
        }

        private IEnumerator WaitAction()
        {
            yield return new WaitForSeconds(1);
            Moving(mask.GetIdentify());
            mask.InitialPosition();
        }
    }
}
