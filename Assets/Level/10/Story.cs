using UnityEngine;
using UnityEngine.SceneManagement;

public class Level10Story : StoryBase
{
    protected override void UpdateDialogue(){
        if (step == 0) {
            dialogueText.text = "This is the last level, consisting of 20 waves. Good luck and have fun!";
        }
        else if (step == 1) SetDialogueActive(false);
        else if (step == 2) {
            dialogueText.text = "You only have one life left! Take this money and try to survive until the end!";
            GameManager.Instance.Balance = GameManager.Instance.Balance + 10000;
        }
        else if (step == 3) SetDialogueActive(false);
        else if (step == 10) {
            dialogueText.text = "This is the end for today. Rest well.";
            SetDialogueActive(true);
        }
        else if (step == 11) LoadNextScene();
        else SetDialogueActive(false);
        step += 1;
    }
    protected override bool EventTriggered(){
        if (GameManager.Instance.Lives == 1){
            step = 2;
            return true;
        }
        if (GameManager.Instance.Wave == 20 && !GameManager.Instance.WaveActive){
            step = 10;
            return true;
        }
        return false;
    }

}
