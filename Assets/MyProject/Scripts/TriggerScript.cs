using UnityEngine;

namespace StarterAssets
{

public class TriggerScript : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public int[] indexes;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(indexes);
            gameObject.SetActive(false);
        }
    }
}}