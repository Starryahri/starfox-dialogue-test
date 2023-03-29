using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image characterPortrait;
    [SerializeField] private DialogueLoader dialogueLoader;
    [SerializeField] private float typingSpeed = 0.02f;
    // [SerializeField] private float portraitAnimationSpeed = 0.2f;

    private List<DialogueSO> dialogues;
    private int currentDialogueIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        dialogues = dialogueLoader.LoadDialogues();
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        if (isTyping) return;

        if (currentDialogueIndex < dialogues.Count)
        {
            DialogueSO currentDialogue = dialogues[currentDialogueIndex];
            characterPortrait.sprite = currentDialogue.characterPortraits[0];

            StartCoroutine(TypeDialogueText(currentDialogue.dialogueText));
            currentDialogueIndex++;
        }
        else
        {
            // End of the dialogue, you can perform other actions here
            Debug.Log("End of dialogue");
        }
    }

    private IEnumerator TypeDialogueText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        Sprite[] characterPortraits = dialogues[currentDialogueIndex].characterPortraits;
        int portraitIndex = 0;

        if (characterPortraits.Length == 0)
        {
            Debug.LogError("Character portraits array is empty. Make sure it's set in the DialogueSO.");
        }

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;

            // Animate the character portrait
            characterPortrait.sprite = characterPortraits[portraitIndex];
            portraitIndex = (portraitIndex + 1) % characterPortraits.Length;

            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ShowNextDialogue();
        }
    }
}
