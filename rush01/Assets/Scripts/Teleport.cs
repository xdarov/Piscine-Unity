using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public float TransitionTimeInSec = 2f;
    public Light Light;
    public string scene_name = null;

    private bool _changingColor = false;
    private Color _color1;
    private Color _color2;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(BeginToChangeColor());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player enter the trigger, let's load the other scene");
            SceneManager.LoadScene(scene_name);
        }
        Debug.Log("collider as the tag " + other.tag);
    }

    private IEnumerator BeginToChangeColor()
    {
        _color1 = Random.ColorHSV(Random.value, Random.value);
        _color2 = Random.ColorHSV(Random.value, Random.value);

        while (true)
        {
            yield return LerpColor(Light, _color1, _color2, TransitionTimeInSec);
            _color1 = Light.color;
            _color2 = Random.ColorHSV(Random.value, Random.value);
        }
    }

    private IEnumerator LerpColor(Light targetLight, Color fromColor, Color toColor, float duration)
    {
        if (_changingColor)
            yield break;
        _changingColor = true;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            // float colorTime = counter / duration;
            targetLight.color = Color.Lerp(fromColor, toColor, counter / duration);
            yield return null;
        }
        _changingColor = false;
    }
}
