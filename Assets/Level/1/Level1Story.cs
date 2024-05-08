using UnityEngine;
using UnityEngine.UI;

public class Level1Story : StoryBase
{
    private Color32 greenColor = new Color32(255, 255, 118, 255);
    private Color32 whiteColor = new Color32(255, 255, 255, 255);
    [SerializeField] private Color32 blinkColor = new Color32(255, 118, 118, 255);
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject sellButton;
    int curHP;
    int curColor = 0;
    [SerializeField] private int intervalBlinkTime = 100;

    protected override void Start() {
        if (isnull(this.startButton)) this.startButton = UIUpdater.Instance.startButton;
        if (isnull(this.sellButton)) this.sellButton = UIUpdater.Instance.sellButton;
        base.Start();
    }
    protected override void UpdateDialogue(){
        if (step == 0) {
            dialogueText.text = "Help! Some oil company just hired an army to attack us!\nListen to me now! I'll tell you everything you need to know!\nClick anywhere on this screen to continue.\n(You can pause the game using 'esc' key.)";
            startButton.SetActive(false);
            SetDialogueActive(true);
        }
        else if (step == 1) {
            dialogueText.text = "I'll guide you step by step. Start by placing a goldfish in the highlighted tile. \nClick on the goldfish on the right panel, then click again inside the highlighted tile to place it.";
            LevelManager.Instance.Tiles[new Point(1, 1)].ColorTile(greenColor);
        }
        else if (step == 3) {
            sellButton.SetActive(false);
            dialogueText.text = "Good job. Now, click the start button to begin the first wave.";
            SetDialogueActive(true);
            startButton.SetActive(true);
        }
        else if (step == 5) {
            startButton.SetActive(false);
            dialogueText.text = "Now that the first wave has ended and a soldier has reached our coral, decreasing our heart by 1.";
            SetDialogueActive(true);
        }
        else if (step == 6) {
            dialogueText.text = "But what's done is done. \nNow, place 3 more goldfish in the highlighted area.";
            LevelManager.Instance.Tiles[new Point(1, 0)].ColorTile(greenColor);
            LevelManager.Instance.Tiles[new Point(1, 2)].ColorTile(greenColor);
            LevelManager.Instance.Tiles[new Point(3, 3)].ColorTile(greenColor);
            sellButton.SetActive(true);
        }
        else if (step == 8) {
            sellButton.SetActive(false);
            dialogueText.text = "Good job. Now, click the start button to begin the wave again.";
            SetDialogueActive(true);
            startButton.SetActive(true);
            curHP = GameManager.Instance.Lives;
        }
        else if (step == 10) {
            if (curHP == GameManager.Instance.Lives) dialogueText.text = "Excellent work! Now try to survive a few waves. \nAfter witnessing our success, surely other fish will want to join us to fight against the army.";
            else dialogueText.text = "Unfortunately, we've lost some lives, but we need to survive a few waves.\nIf we succeed, there will be a high chance that other fish will want to join us to fight against the army.";
            SetDialogueActive(true);
            sellButton.SetActive(true);
        }
        else if (step == 12) {
            dialogueText.text = "I think the next wave is the last wave for today. Keep up the good job.";
            SetDialogueActive(true);
        }
        else if (step == 14) {
            dialogueText.text = "This is the end for today. Rest well.";
            SetDialogueActive(true);
        }
        else if (step == 15) LoadNextScene();
        else {
            sellButton.GetComponent<Image>().color = whiteColor;
            SetDialogueActive(false);
        }
        step += 1;
    }
    private void flashSellButton(bool condition){
        if (intervalBlinkTime <= 0) intervalBlinkTime = 100;
        if (curColor<intervalBlinkTime/2) sellButton.GetComponent<Image>().color = whiteColor;
        else sellButton.GetComponent<Image>().color = blinkColor;
        if (condition) curColor +=1;
        else curColor = 0;
        curColor %= intervalBlinkTime;
    }
    protected override bool EventTriggered(){
        if (step == 3){
            flashSellButton(GameManager.Instance.Balance == 0);
            LevelManager.Instance.Tiles[new Point(1, 1)].ColorTile(greenColor);
            return !LevelManager.Instance.Tiles[new Point(1, 1)].IsEmpty;
        }
        if (step == 5){
            return GameManager.Instance.Wave == 1 && !GameManager.Instance.WaveActive;
        }
        if (step == 8){
            flashSellButton(GameManager.Instance.Balance == 0);
            Point[] points = { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(3, 3) };
            var ret = true;
            foreach ( var point in points){
                var tile = LevelManager.Instance.Tiles[point];
                if (tile.IsEmpty) {
                    if (tile.TileColor().Equals(whiteColor)) tile.ColorTile(greenColor);
                    ret = false;
                }
            }
            return ret;
        }
        if (step == 10){
            return GameManager.Instance.Wave == 2 && !GameManager.Instance.WaveActive;
        }
        if (step == 12){
            return GameManager.Instance.Wave == 4 && !GameManager.Instance.WaveActive;
        }
        if (step == 14){
            return GameManager.Instance.Wave == 5 && !GameManager.Instance.WaveActive;
        }
        return false;
    }

}
