using UnityEngine;
using UnityEngine.SceneManagement;

public class Level10Story : StoryBase
{
    protected override void UpdateDialogue(){
        if (step == 0) {
            SetDialogueText("This is the last level, consisting of 10 waves. Good luck and have fun!");
        }
        else if (step == 1) SetDialogueActive(false);
        else if (step == 2) {
            SetDialogueText($"You only have {GameManager.Instance.Lives} life left! Take this money and try to survive until the end!");
            GameManager.Instance.Balance = GameManager.Instance.Balance + 100000;
        }
        else if (step == 3) SetDialogueActive(false);
        else if (step == 10) {
            SetDialogueText("This is the end. Finally, the Pla world can rest in peace, thanks to you. \nBut our other friends still fight in a faraway land called Endless Mode. Go and join them.");
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
        if (GameManager.Instance.Wave == 10 && !GameManager.Instance.WaveActive){
            step = 10;
            return true;
        }
        return false;
    }

}
