using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace StarterAssets
{
public class NoteItem : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject descUI;
    public GameObject slot;
    public GameObject health;
    public AudioSource pickUpSound;

    void Update()
    {
        if(noteUI.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                noteUI.SetActive(false);
                descUI.SetActive(true);
                slot.SetActive(true);
                health.SetActive(true);
                player.GetComponent<FirstPersonController>().enabled = true;
            }
        }
    }

    public void Use()
    {
        if(pickUpSound) pickUpSound.Play();
        noteUI.SetActive(true);
        descUI.SetActive(false);
        slot.SetActive(false);
        health.SetActive(false);
        player.GetComponent<FirstPersonController>().enabled = false;
    }
}
}