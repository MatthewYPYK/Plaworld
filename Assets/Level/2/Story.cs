using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Story : StoryBase
{
    protected override void UpdateDialogue(){
        if (step == 0) {
            SetDialogueText("Good morning! We have a new member, Puffer Fish. He will continuously attack his surroundings. Use him wisely!");
        }
        else if (step == 1) SetDialogueActive(false);
        else if (step == 2) {
            SetDialogueText($"You only have {GameManager.Instance.Lives} life left! Take this money and try to survive until the end!");
            GameManager.Instance.Balance = GameManager.Instance.Balance + 2000;
        }
        else if (step == 3) SetDialogueActive(false);
        else if (step == 10) {
            SetDialogueText("This is the end for today. Rest well.");
            SetDialogueActive(true);
        }
        else if (step == 11) LoadNextScene();
        else SetDialogueActive(false);
        step += 1;
    }
    protected override bool EventTriggered(){
        if (step == 2){
            if (GameManager.Instance.Lives <= 5)
                return true;
        }
        if (GameManager.Instance.Wave == 5 && !GameManager.Instance.WaveActive){
            step = 10;
            return true;
        }
        return false;
    }

}
