using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipScript : MonoBehaviour
{
    public Transform gunPos;
    public float range = 10f;
    GameObject currentWeapon;
    GameObject weapon;
    public 

    bool canGrab;

    private void Update()
    {
        CheckWeapons();
        
        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentWeapon != null)
                    Drop();

                Pickup();
            }
        }

        if (currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Drop();
        }
    }

    private void CheckWeapons()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Item")
            {
                Debug.Log("Item Equipped");
                canGrab = true;
                weapon = hit.transform.gameObject;
            }
        }
        else
            canGrab = false;
    }
    
    private void Pickup()
    {
        currentWeapon = weapon;
        currentWeapon.transform.position = gunPos.position;
        currentWeapon.transform.parent = gunPos;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon = null;
    }
}