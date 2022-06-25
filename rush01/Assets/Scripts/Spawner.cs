using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] zombies;
    public GameObject zombie;
    public bool spawning;

    // Use this for initialization
    void Start()
    {
        zombie = null;
        spawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombie == null && !spawning)
        {
            StartCoroutine(spawn());
            spawning = true;
        }
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(5f);
        zombie = Instantiate(zombies[Random.Range((int)0, (int)2)], transform.position, transform.rotation);
        spawning = false;
    }
}
