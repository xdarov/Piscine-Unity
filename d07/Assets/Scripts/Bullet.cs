using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool Active = true;
    public int damage;
    private GameObject collis_obj;
    public AudioSource Hit;
    public AudioSource Miss;


    void Start()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!Active)
            return ;
        Active = false;
        GetComponent<Rigidbody>().useGravity = true;
        collis_obj = GameObject.Find(collision.gameObject.name);
        if (collis_obj.GetComponent<Tank>())
        {
            Hit.Play();
            collis_obj.transform.parent.gameObject.GetComponent<Tank_life>().take_dmg(damage);
        }
        else
            Miss.Play();
        transform.position = new Vector3(0, 0, 0);
        Invoke("remove_object", 4);
    }

    void remove_object()
    {
        if (transform.gameObject)
            Destroy(transform.gameObject);
    }
}