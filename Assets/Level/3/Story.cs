using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Story : StoryBase
{
    protected override void UpdateDialogue(){
        if (step == 0) {
            dialogueText.text = "We are sharks! We are orca! We only charge forward!!!!!!!!";
        }
        else if (step == 1) SetDialogueActive(false);
        else if (step == 2) {
            dialogueText.text = "We are rich! Take this money and try to survive until the end!";
            GameManager.Instance.Balance = GameManager.Instance.Balance + 1000000;
        }
        else if (step == 3) SetDialogueActive(false);
        else if (step == 4) {
            dialogueText.text = "This is the end for today. Be prepared for tomorrow!";
            SetDialogueActive(true);
        }
        else if (step == 5) LoadNextScene();
        else SetDialogueActive(false);
        step += 1;
    }
    protected override bool EventTriggered(){
        if (step == 2){
            return GameManager.Instance.Balance <= 10000;
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
