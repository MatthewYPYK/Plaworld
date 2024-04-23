using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Story : StoryBase
{
    protected override void UpdateDialogue(){
        if (step == 0) {
            dialogueText.text = "Good morning! We have a new member, Puffer Fish. He will continuously attack his surroundings. Use him wisely!";
        }
        else if (step == 1) SetDialogueActive(false);
        else if (step == 2) {
            dialogueText.text = "You only have one life left! Take this money and try to survive until the end!";
            GameManager.Instance.Balance = GameManager.Instance.Balance + 1000;
        }
        else if (step == 3) SetDialogueActive(false);
        else if (step == 4) {
            dialogueText.text = "This is the end for today. Rest well.";
            SetDialogueActive(true);
        }
        else if (step == 5) LoadNextScene();
        else SetDialogueActive(false);
        step += 1;
    }
    protected override bool EventTriggered(){
        if (step == 2){
            return GameManager.Instance.Lives == 1;
        }
        if (step == 4){
            if (GameManager.Instance.Wave == 5 && !GameManager.Instance.WaveActive){
                step = 4;
                return true;
            }
        }
        return false;
    }

}
