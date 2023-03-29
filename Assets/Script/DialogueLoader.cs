using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    [SerializeField] private string dialoguesCSVFileName;

    public List<DialogueSO> LoadDialogues()
    {
        List<DialogueSO> dialogues = new List<DialogueSO>();
        List<Dictionary<string, string>> csvData = CSVLoader.Load(dialoguesCSVFileName);

        foreach (var row in csvData)
        {
            DialogueSO dialogue = ScriptableObject.CreateInstance<DialogueSO>();
            dialogue.characterName = row["CharacterName"];
            dialogue.characterPortraits = LoadCharacterPortraits(row["CharacterPortrait"]);
            dialogue.dialogueText = row["DialogueText"];
            dialogues.Add(dialogue);
        }

        return dialogues;
    }

    private Sprite[] LoadCharacterPortraits(string characterName)
    {
        // Find the number of portraits for this character in the Resources folder in the their respective folder
        int numberOfPortraits = Resources.LoadAll<Sprite>($"CharacterPortraits/{characterName}").Length;

        // Use a check to see if the character folder exists
        if (numberOfPortraits == 0)
        {
            Debug.LogError($"Character folder not found: {characterName}");
            return null;
        }

        Sprite[] characterPortraits = new Sprite[numberOfPortraits];
        // Load the portraits
        for (int i = 0; i < numberOfPortraits; i++)
        {
            characterPortraits[i] = Resources.Load<Sprite>($"CharacterPortraits/{characterName}/{characterName}_{i}");
        }
        // Return the portraits
        return characterPortraits;
    }
}
