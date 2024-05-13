using UnityEngine;

namespace StarterAssets
{

public class TriggerScript : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public int[] dialogueIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(dialogueIndex);
            gameObject.SetActive(false);
        }
    }
}}