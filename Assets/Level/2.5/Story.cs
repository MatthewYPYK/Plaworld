using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2pStory : StoryBase
{
    [SerializeField] protected Sprite SharkImage;
    [SerializeField] protected GameObject SharkButton;
    protected override void UpdateDialogue(){
        if (step == 0) SetDialogueText("When life throws rocks at you, become Pla the rock!");
        else if (step == 1) SetDialogueText("Frankly, we've exhausted our supply of Plas; \nonly rocks remain for us to wield.");
        else if (step == 10) SetDialogueText("Listen up, some enemy types will detonate themselves if they get surrounded. \nExploit that weakness!");
        else if (step == 11) SetDialogueActive(false);
        else if (step == 20) SetDialogueText("The only way to truly understand is to experience it firsthand.");
        else if (step == 21) SetDialogueActive(false);
        else if (step == 25) GameManager.Instance.SetTimeScale(1);
        else if (step == 30) SetDialogueText("Oh no, this one's tough. \nIf that thing breaches our corral, we're done for!");
        else if (step == 31) SetDialogueText("We can't take them down yet; \nall we can do is give it our all and wait for our friend to back us up!");
        else if (step == 32) SetDialogueText("Let's employ all our wisdom to buy some time!");
        else if (step == 33) SetDialogueActive(false);
        else if (step == 40) SetDialogueText("Look, that's the shark hoard! \nThey're the fiercest fighters around here!");
        else if (step == 41) SetDialogueText("They're here to aid us in our battle against the army!");
        else if (step == 42)
        {
            nameText.text = "Shark";
            characterImage.sprite = SharkImage;
            characterImage.rectTransform.sizeDelta = new Vector2(300, 300);
            dialogueText.fontSize = 80;
            SetDialogueText("Uwoghhhhh");
            SharkButton.SetActive(true);
            int x = LevelManager.Instance.MapSize.X - 1;
            for (int y = 0; y < LevelManager.Instance.MapSize.Y; y++) 
                LevelManager.Instance.Tiles[new Point(x, y)].RefreshTile();
        }
        else if (step == 50) {
            PlaBtn plaBtn = SharkButton.GetComponent<PlaBtn>();
            int x = LevelManager.Instance.MapSize.X - 1;
            for (int y = 0; y < LevelManager.Instance.MapSize.Y; y++) 
                LevelManager.Instance.Tiles[new Point(x, y)].PlacePla(plaBtn,false);
        }
        else if (step == 1000)
        {
            dialogueText.fontSize = 24;
            SetDialogueText("This marks the end of today's journey. Rest well. A new chapter awaits for us.");
        }
        else if (step == 1001) LoadNextScene();
        else SetDialogueActive(false);
        step += 1;
    }
    protected override bool EventTriggered(){
        if (GameManager.Instance.Wave == 5 && !GameManager.Instance.WaveActive){
            step = 1000;
            return true;
        }
        if (step == 44){
        }
        return false;
    }

}
