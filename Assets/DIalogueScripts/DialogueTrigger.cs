using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue lines;
    private bool talked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(talked == false && collision.CompareTag("Player"))
        {

            FindAnyObjectByType<DialogueManager>().StartDialogue(lines);
            talked = true;

        }

    }

}
