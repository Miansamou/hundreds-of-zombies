using UnityEngine;

namespace Game
{
    public class Actions : MonoBehaviour
    {
        protected void Moving(bool identify)
        {
            if (!identify) return;
            switch (transform.eulerAngles.z)
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
        }
    }
}
