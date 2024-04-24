using UnityEngine;
using TMPro;

public class EasyModeDialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;

    private int action = 0;

    void Start() => UpdateDialogue();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) UpdateDialogue();
    }

    void UpdateDialogue()
    {
        if (action == 0) dialogueText.text = "Some oil company just hired an army to attack you.\nI'll tell you everything you need to know.\nFirst, click anywhere on this screen to continue.";
        else if (action == 1) dialogueText.text = "Enemies will spawn from their portal and run to your coral. Protect it.\nClick on a fish to buy it. Click again on a tile to place it.";
        else if (action == 2) dialogueText.text = $"If an enemy reaches your coral, you will lose a life point.\nYou have {GameManager.Instance.Lives} lives. \nIf your life points reach zero... No, that's impossible."; 
        else if (action == 3) dialogueText.text = "But please note that bosses can instantly kill you if they reach your coral.";
        else if (action == 4) dialogueText.text = $"Now you have {PlaManager.Instance.PlaBtnsCount-1} types of Pla and a rock. \nWhat does each Pla do? \nYou can point your mouse at each Pla.";
        else if (action == 5) dialogueText.text = $"Here are shortcuts:\nNum 1-{PlaManager.Instance.PlaBtnsCount} to select Pla\n-/+ to speed up or slow down the game speed\nSpace to hide the Pla panel on the right";
        else if (action == 6) dialogueText.text = "That is all you need to know.\nPlease have fun.";
        else dialogueBox.SetActive(false);
        action += 1;
    }

        

}
