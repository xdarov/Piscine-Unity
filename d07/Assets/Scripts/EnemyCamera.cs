using UnityEngine;

namespace Eccentric 
{

    public class EnemyCamera : MonoBehaviour 
    {
        private GameObject player;

        void Start()
        {
            player = GameObject.Find("player_canon");
        }

        void Update() {
            Rotate();
        }

        void Rotate() 
        {
            if (player)
            {
            transform.LookAt(player.transform);
            // transform.rotation.x += 10;
            if (transform.rotation.x > 20)
                transform.rotation = new Quaternion(20, transform.position.y, transform.position.z, 0);
            if (transform.rotation.x < -20)
                transform.rotation = new Quaternion(-20, transform.position.y, transform.position.z, 0);
            }
        }
    }
}


