using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace StarterAssets
{
public class ItemInteraction : MonoBehaviour
{
    [SerializeField]private Camera characterCamera;
    public GameObject guideUI;

    void Update()
    {
        var ray = characterCamera.ViewportPointToRay(new Vector3(0.5f, 0.49f, 0.5f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 2.0f))
        {
            if (hit.collider.TryGetComponent(out NoteItem item))
            {
                guideUI.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    guideUI.SetActive(false);
                    item.Use();
                }
            }
            else
            {
                guideUI.SetActive(false);
            }
        }
    }
}
}
