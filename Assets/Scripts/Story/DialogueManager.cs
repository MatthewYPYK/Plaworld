using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
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
        if (action == 0) dialogueText.text = "Help! Some oil company just hired an army to attack us!\n\nListen to me now! I'll tell you everything you need to know!\nFirst, click anywhere on this screen to continue.";
        else if (action == 1) dialogueText.text = "Enemies will spawn from their portal and run to our precious coral. Protect it!\n\nClick on a fish to buy it. Click again on a tile to place it.";
        else if (action == 2) dialogueText.text = $"If an enemy reaches our coral, we will lose a life point.\n\nWe only have {GameManager.Instance.Lives} lives. Make every life count!\nIf our life points reach zero, we will all be doomed!";
        else if (action == 3) dialogueText.text = "Now we have 5 types of fish and a rock. What does each fish do? You will need to ask the Pla division!";
        else if (action == 4) dialogueText.text = "As well as enemies, this will also be told by the Enemy division.";
        else if (action == 5) dialogueText.text = "Here are shortcuts:\nNum 1-6 to select fish\n-/+ to speed up or slow down the game speed\nSpace to hide the Pla panel on the right.\n";
        else if (action == 6) dialogueText.text = "That is all you need to know.\nOur lives depend on you now.";
        else dialogueBox.SetActive(false);
        action += 1;
    }

        

}
