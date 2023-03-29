using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueSO : ScriptableObject
{
    public string characterName;
    public Sprite[] characterPortraits;
    [TextArea(3, 10)]
    public string dialogueText;
}
