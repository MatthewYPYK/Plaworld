using UnityEngine;
using TMPro;

public class EasyModeDialogueManager : StoryBase
{
    protected override void UpdateDialogue()
    {
        if (step == 0) dialogueText.text = "Some oil company just hired an army to attack you.\nI'll tell you everything you need to know.\nFirst, click anywhere on this screen to continue.";
        else if (step == 1) dialogueText.text = "Enemies will spawn from their portal and run to your coral. Protect it.\nClick on a fish to buy it. Click again on a tile to place it.";
        else if (step == 2) dialogueText.text = $"If an enemy reaches your coral, you will lose a life point.\nYou have {GameManager.Instance.Lives} lives. \nIf your life points reach zero... No, that's impossible."; 
        else if (step == 3) dialogueText.text = "But please note that bosses can instantly kill you if they reach your coral.";
        else if (step == 4) dialogueText.text = $"Now you have {PlaManager.Instance.PlaBtnsCount-1} types of Pla and a rock. \nWhat does each Pla do? \nYou can point your mouse at each Pla.";
        else if (step == 5) dialogueText.text = $"Here are shortcuts:\nNum 1-{PlaManager.Instance.PlaBtnsCount} to select Pla\n-/+ to speed up or slow down the game speed\nSpace to hide the Pla panel on the right";
        else if (step == 6) dialogueText.text = "That is all you need to know.\nPlease have fun.";
        else SetDialogueActive(false);
        step += 1;
    }

        

}
