using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;
    public Text goodChoice;
    public Text badChoice;

    public Animator animator;

    private DialogueTrigger trigger;

    public bool talking = false;

    void Start()
    {
        
        sentences = new Queue<string>();

    }

    void Update()
    {
        
        if(talking == true && Input.GetKeyDown(KeyCode.Space))
        {

            DisplayNextSentence();

        } 

    }

    public void StartDialogue(Dialogue dialogue)
    {
        talking = true;

        Time.timeScale = 0f;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();
        
        foreach(string sentence in dialogue.sentences)
        {

            sentences.Enqueue(sentence);

        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {

            EndDialogue();
            return;

        }

        string sentence = sentences.Dequeue();

        if (sentence == "[CHOICE]")
        {

            DisplayOptions();
            return;

        }

        StopAllCoroutines();
        StartCoroutine(TypeSentece(sentence));

    }

    IEnumerator TypeSentece(string sentence)
    {

        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {

            dialogueText.text += letter;
            yield return null;

        }

        goodChoice.gameObject.SetActive(false);
        badChoice.gameObject.SetActive(false);
        
    }

    public void DisplayOptions()
    {

        goodChoice.gameObject.SetActive(true);
        badChoice.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(false);
        
    }

    public void goodOption()
    {

        trigger = FindAnyObjectByType<DialogueTrigger>();

        ResourceManager.resource.gold += 50;
        ResourceManager.resource.goldUpdate();

        restOfGood(trigger.lines);

    }

    public void badOption()
    {

        trigger = FindAnyObjectByType<DialogueTrigger>();

        ResourceManager.resource.gold -= 50;
        ResourceManager.resource.goldUpdate();

        restOfBad(trigger.lines);

    }

    public void restOfGood(Dialogue resof)
    {

        Color goodCol = goodChoice.color;
        goodCol.a = 0f;
        goodChoice.color = goodCol;

        Color badCol = badChoice.color;
        badCol.a = 0f;
        badChoice.color = badCol;

        sentences.Clear();

        foreach (string sentence in resof.goodResponse) 
        {

            sentences.Enqueue(sentence);

        }

        DisplayNextSentence();

    }

    public void restOfBad(Dialogue resof)
    {

        Color goodCol = goodChoice.color;
        goodCol.a = 0f;
        goodChoice.color = goodCol;

        Color badCol = badChoice.color;
        badCol.a = 0f;
        badChoice.color = badCol;

        sentences.Clear();

        foreach (string sentence in resof.badResponse)
        {

            sentences.Enqueue(sentence);

        }

       DisplayNextSentence();

    }

    public void EndDialogue()
    {

        animator.SetBool("IsOpen", false);
        Time.timeScale = 1f;
        talking = false;

    }

}
