using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
public class GrabSystem : MonoBehaviour
{
    [SerializeField]
    private Camera characterCamera;

    [SerializeField]
    private Transform slot;
    private PickableItem pickedItem;
    public StarterAssetsInputs _input;


    private void Update()
    {
        if (_input.pick)
        {
            if(!pickedItem)
            {
                var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, 2.0f))
                {
                    var pickable = hit.transform.GetComponent<PickableItem>();

                    if(pickable)
                    {
                        PickItem(pickable);
                    }
                }
            }
        }
        
        if(_input.drop)
        {
            if(pickedItem)
            {
                DropItem(pickedItem);
            }
        }
    }

    private void PickItem(PickableItem item)
    {
        pickedItem = item;

        item.Rb.isKinematic = true;

        item.transform.SetParent(slot);

        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    private void DropItem(PickableItem item)
    {
        pickedItem = null;

        item.transform.SetParent(null);

        item.Rb.isKinematic = false;

        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }
}
}