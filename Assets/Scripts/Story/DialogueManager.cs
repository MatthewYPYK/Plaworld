using UnityEngine;
using TMPro;

public class DialogueManager : StoryBase
{
    protected override void UpdateDialogue()
    {
        if (step == 0) dialogueText.text = "Help! Some oil company just hired an army to attack us!\n\nListen to me now! I'll tell you everything you need to know!\nFirst, click anywhere on this screen to continue.";
        else if (step == 1) dialogueText.text = "Enemies will spawn from their portal and run to our precious coral. Protect it!\n\nClick on a fish to buy it. Click again on a tile to place it.";
        else if (step == 2) dialogueText.text = $"If an enemy reaches our coral, we will lose a life point.\n\nWe only have {GameManager.Instance.Lives} lives. Make every life count!\nIf our life points reach zero, we will all be doomed!";
        else if (step == 3) dialogueText.text = $"Now we have {PlaManager.Instance.PlaBtnsCount-1} types of Pla and a rock. \nWhat does each Pla do? \nYou can point your mouse at each Pla.";
        else if (step == 4) dialogueText.text = $"Here are shortcuts:\nNum 1-{PlaManager.Instance.PlaBtnsCount} to select Pla\n-/+ to speed up or slow down the game speed\nSpace to hide the Pla panel on the right.\n";
        else if (step == 5) dialogueText.text = "That is all you need to know.\nOur lives depend on you now.";
        else SetDialogueActive(false);
        step += 1;
    }

        

}
