using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public string[] lines;
        public float textSpeed;

        public void StartDialogue(int[] indexes)
        {
            if(indexes.Length > 0)
            {
                StartCoroutine(TypeLine(indexes));
            }
        }

        IEnumerator TypeLine(int[] indexes)
        {
            foreach(int index in indexes)
            {
                dialogueText.text = string.Empty;

                if(index >= 0 && index < lines.Length)
                {
                    foreach (char c in lines[index].ToCharArray())
                    {
                        dialogueText.text += c;
                        yield return new WaitForSeconds(textSpeed);
                    }
                }

                yield return new WaitForSeconds(2.5f);
                dialogueText.text = string.Empty;
            }
        }
    }
}
