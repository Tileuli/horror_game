using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    public int healAmount = 50;
    public AudioSource healSound;
    public PlayerHealth playerHealth;
    public GrabSystem grabSystem;

    void Update()
    {
        if (transform.parent.parent != null && transform.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                healSound.Play();
                playerHealth.Heal(healAmount);
                StartCoroutine(DestroyAfterDelay());
            }
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        grabSystem.DropItem(grabSystem.pickedItem[grabSystem.selectedItemIndex]);
    }
}
