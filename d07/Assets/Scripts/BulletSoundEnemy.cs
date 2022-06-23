using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSoundEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Audio;

    private void OnCollisionEnter (Collision collision)
    {
        Audio.Play();
    }
}
