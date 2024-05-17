using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GrabSystem : MonoBehaviour
{
    [SerializeField]private Camera characterCamera;
    [SerializeField]private Transform slot;
    [SerializeField] private List<RawImage> itemImages;
    public List<PickableItem> pickedItem = new List<PickableItem>();
    public TextMeshProUGUI description;
    public GameObject guideUI;
    public int selectedItemIndex = -1;


    private void Update()
    {
        if(pickedItem.Count > 0 && pickedItem[selectedItemIndex] != null)
        {
            description.text = pickedItem[selectedItemIndex].description;
        }
        else
        {
            description.text = null;
        }

        if(pickedItem.Count <= 4)
        {
            var ray = characterCamera.ViewportPointToRay(new Vector3(0.5f, 0.49f, 0.5f));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 2.0f))
            {
                var pickable = hit.transform.GetComponent<PickableItem>();

                if(pickable)
                {
                    guideUI.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        guideUI.SetActive(false);
                        PickItem(pickable);
                    }
                }
                else
                {
                    guideUI.SetActive(false);
                }
            }
            else
            {
                guideUI.SetActive(false);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(pickedItem[selectedItemIndex] != null)
            {
                DropItem(pickedItem[selectedItemIndex]);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectItem(i);
            }
        }
    }

    private void PickItem(PickableItem item)
    {
        int lastNonNullIndex = -1;
        for (int i = 0; i < pickedItem.Count; i++)
        {
            if (pickedItem[i] == null)
            {
                lastNonNullIndex = i;
                break;
            }
        }

        if (lastNonNullIndex != -1)
        {
            if(pickedItem[selectedItemIndex] != null)
            {
                pickedItem[selectedItemIndex].gameObject.SetActive(false);
                itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one;
            }

            selectedItemIndex = lastNonNullIndex;

            pickedItem[selectedItemIndex] = item;
            itemImages[selectedItemIndex].texture = item.texture;
            itemImages[selectedItemIndex].color = new Color(1f, 1f, 1f, 1f);
            itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one * 1.25f;
            
            item.GetComponent<Collider>().enabled = false;
            item.Rb.isKinematic = true;
            item.transform.SetParent(slot);
            item.transform.localPosition = Vector3.zero;
            item.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            if(pickedItem.Count < 4)
            {
                if(selectedItemIndex != -1 && pickedItem[selectedItemIndex] != null)
                {
                    pickedItem[selectedItemIndex].gameObject.SetActive(false);
                    itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one;
                }

                pickedItem.Add(item);
                selectedItemIndex = pickedItem.Count - 1;
                itemImages[selectedItemIndex].texture = item.texture;
                itemImages[selectedItemIndex].color = new Color(1f, 1f, 1f, 1f);
                itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one * 1.25f;

                item.GetComponent<Collider>().enabled = false;
                item.Rb.isKinematic = true;
                item.transform.SetParent(slot);
                item.transform.localPosition = Vector3.zero;
                item.transform.localEulerAngles = Vector3.zero;
            }
        }
    }

    public void DropItem(PickableItem item)
    {
        pickedItem[selectedItemIndex] = null;
        itemImages[selectedItemIndex].texture = null;
        itemImages[selectedItemIndex].color = new Color(1f, 1f, 1f, 0f);
        itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one;

        int lastNonNullIndex = -1;
        for (int i = pickedItem.Count - 1; i >= 0; i--)
        {
            if (pickedItem[i] != null)
            {
                lastNonNullIndex = i;
                break;
            }
        }

        if (lastNonNullIndex != -1)
        {
            selectedItemIndex = lastNonNullIndex;
            pickedItem[selectedItemIndex].gameObject.SetActive(true);
            itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one * 1.25f;
        }

        item.GetComponent<Collider>().enabled = true;
        item.Rb.isKinematic = false;
        item.transform.SetParent(null);
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }

    private void SelectItem(int index)
    {
        if(index != selectedItemIndex && index < pickedItem.Count)
        {
            if(pickedItem[index] != null && pickedItem[selectedItemIndex] != null)
            {
                pickedItem[selectedItemIndex].gameObject.SetActive(false);
                itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one;
                selectedItemIndex = index;
                pickedItem[selectedItemIndex].gameObject.SetActive(true);
                itemImages[selectedItemIndex].rectTransform.parent.localScale = Vector3.one * 1.25f;
            }
        }
    }
}