using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour
{

    public float bulletForce = 20f;
    public GameObject Bullet;
    public AudioSource Shot;
    private GameObject player;

    void Start()
    {
        float i;
        float j;
        System.Random rand = new System.Random();
        i = (float)rand.Next(3, 10);
        j = 3 + ((float)rand.Next(1, 10) / (float)rand.Next(1, 10));
        InvokeRepeating("fire", j, i);
        player = GameObject.Find("player_body");
    }
    void Update()
    {
    }

    void fire()
    {
        if (Physics.Linecast(transform.position, player.transform.position))
        {
            Shot.Play();
            GameObject newBullet = Instantiate(Bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        }
    }
}
