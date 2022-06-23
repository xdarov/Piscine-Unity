using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public float bulletForce = 50f;
    public int key;
    public int count_of_bullets;
    public GameObject Bullet;
    public AudioSource Shot;
    public AudioSource No_bullets;


    void Update()
    {
        if (Input.GetMouseButtonDown(key) && count_of_bullets > 0)
        {
            count_of_bullets -= 1;
            Shot.Play();
            GameObject newBullet = Instantiate(Bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        }
        else if (Input.GetMouseButtonDown(key) && count_of_bullets <= 0)
            No_bullets.Play();
    }
}
