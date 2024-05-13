using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ItemInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 2f;
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI win;
    public GameObject portal;
    public int score = 0;


    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.position, interactionDistance);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Item"))
            {
                Vector3 directionToItem = (collider.transform.position - player.position).normalized;
                if (Vector3.Dot(player.forward, directionToItem) > 0.5f)
                {
                    interactionText.text = "Press E to collect";
                    interactionText.gameObject.SetActive(true);

                    if (Keyboard.current.eKey.wasPressedThisFrame)
                    {
                        CollectItem(collider.gameObject);
                    }
                    return;
                }
            }
            if (collider.CompareTag("End"))
            {
                win.text = "YOU WIN";
            }
        }

        interactionText.gameObject.SetActive(false);
    }

    void CollectItem(GameObject item)
    {
        score = score + 1;
        scoreText.text = "Score: " + score + " / 10";

        if (score >= 10)
        {
            if (portal != null)
            {
                portal.SetActive(true);
            }
        }

        Destroy(item);
        interactionText.gameObject.SetActive(false);
    }
}
