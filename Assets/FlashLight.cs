using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : MonoBehaviour
{
    public GameObject light;
    public AudioSource turnOn;
    public AudioSource turnOff;
    public bool state;

    void Update()
    {
        if(transform.parent.parent != null && transform.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(!state)
                {
                    light.SetActive(true);
                    turnOn.Play();
                    state = true;
                }
                else
                {
                    light.SetActive(false);
                    turnOff.Play();
                    state = false;
                }
            }
        }
        else
        {
            light.SetActive(false);
            state = false;
        }
    }
}