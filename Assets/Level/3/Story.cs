using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Story : StoryBase
{
    protected override void UpdateDialogue(){
        if (step == 0) {
            dialogueText.fontSize = 30;
            SetDialogueText("We are sharks! \nWe are orcas! \nWe only charge forward!!!!!!!!");
        }
        else if (step == 1) {
            SetDialogueActive(false);
            GameManager.Instance.SetTimeScale(2);
        }
        else if (step == 2) SetDialogueText("He who advances is sure of salvation, but he who retreats will go to hell.");
        else if (step == 3) {
            dialogueText.fontSize = 36;
            SetDialogueText("He who advances is sure of salvation! \nBut he who retreats will go to hell!!!");
        }
        else if (step == 4) {
            SetDialogueActive(false);
            GameManager.Instance.SetTimeScale(2);
        }
        else if (step == 5) {
            dialogueText.fontSize = 32;
            SetDialogueText("They have tanks, we have sharks. Answer prevailed without being asked.");
        }
        else if (step == 6) {
            dialogueText.fontSize = 40;
            SetDialogueText("What answer?");
        }
        else if (step == 7) {
            dialogueText.fontSize = 56;
            SetDialogueText("IT'S TIME TO CHARGE!!!!!!");
        }
        else if (step == 8) {
            dialogueText.fontSize = 80;
            SetDialogueText("URAAAAAAAA");
        }
        else if (step == 9) {
            SetDialogueActive(false);
            GameManager.Instance.SetTimeScale(2);
        }
        else if (step == 10) {
            dialogueText.fontSize = 36;
            SetDialogueText("If they send a big thingy, should not we 'sasageyo'?");
        }
        else if (step == 11) {
            nameText.text = "E*win";
            dialogueText.fontSize = 64;
            SetDialogueText("SHINZOU WO...");
        }
        else if (step == 12) {
            dialogueText.fontSize = 80;
            SetDialogueText("SASAGEYO!!!!");
        }
        else if (step == 90) {
            SetDialogueText("We are rich! Take this money and try to survive until the end!");
            GameManager.Instance.Balance = GameManager.Instance.Balance + 1000000;
        }
        else if (step == 100) {
            dialogueText.fontSize = 36;
            SetDialogueText("This is the end for today. Be prepared for tomorrow!!!!!");
        }
        else if (step == 101) LoadNextScene();
        else {
            nameText.text = "Shark";
            dialogueText.fontSize = 24;
            SetDialogueActive(false);
        }
        step += 1;
    }
    protected override bool EventTriggered(){
        if (step == 2)return GameManager.Instance.Wave == 1 && !GameManager.Instance.WaveActive;
        if (step == 5)return GameManager.Instance.Wave == 2 && !GameManager.Instance.WaveActive;
        if (step == 10)return GameManager.Instance.Wave == 4 && !GameManager.Instance.WaveActive;
        if (GameManager.Instance.Balance <= 10000){
            step = 90;
            return true;
        }
        if (GameManager.Instance.Wave == 5 && !GameManager.Instance.WaveActive){
            step = 100;
            return true;
        }
        return false;
    }

}
