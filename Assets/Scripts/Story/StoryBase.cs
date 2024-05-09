using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class StoryBase : Singleton<StoryBase>
{
    [SerializeField] bool useDefaultMap = true;
    [SerializeField] TextAsset mapData;
    [SerializeField] OveridableObject<Point> greenSpawn, coral;
    [SerializeField] int balance = -1;
    [SerializeField] int lives = -1;
    [SerializeField] float sellMultiplier = -1;
    [SerializeField] int nextSceneId = 0;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] protected Image backGroundImage;
    [SerializeField] protected TMP_Text nameText;
    [SerializeField] protected TMP_Text dialogueText;
    [SerializeField] protected Image characterImage;
    protected bool dialogueStatus = false;
    public bool IsDialogueActive() => dialogueStatus;
    protected int step;

    protected virtual void Awake(){
        if (!useDefaultMap){
            if (!isnull(mapData)) LevelManager.Instance.mapData = mapData;
            if (greenSpawn.Overide) LevelManager.Instance.greenSpawn = greenSpawn;
            if (coral.Overide) LevelManager.Instance.coral = coral;
        }
    }
    // SerializeField GameObject dialogueBox using only " is null" will return false
    protected bool isnull(object obj){
        return obj is null || obj.ToString() == "null";
    }
    protected virtual void Start() {
        if (balance > 0) GameManager.Instance.Balance = balance;
        if (lives > 0) GameManager.Instance.Lives = lives;
        if (sellMultiplier >= 0) GameManager.Instance.SellMultiplier = sellMultiplier;
        if (isnull(this.dialogueBox)) this.dialogueBox = UIUpdater.Instance.dialogueBox;
        if (isnull(this.backGroundImage)) this.backGroundImage = UIUpdater.Instance.backGroundImage;
        if (isnull(this.nameText)) this.nameText = UIUpdater.Instance.nameText;
        if (isnull(this.dialogueText)) this.dialogueText = UIUpdater.Instance.dialogueText;
        step = 0;
        UpdateDialogue();
    }

    protected virtual void Update()
    {
        if (dialogueBox.activeSelf){
            if (Input.GetMouseButtonDown(0)) UpdateDialogue();
        } else {
            if (EventTriggered()) UpdateDialogue();
        }
    }

    public void CallAction(int _step){
        this.step = _step;
        UpdateDialogue();
    }

    protected virtual void UpdateDialogue(){
        step += 1;
    }
    protected virtual bool EventTriggered(){
        return true;
    }

    public void SetDialogueActive(bool isDialogueActivated) {
        if (isDialogueActivated){
            dialogueStatus = true;
            GameManager.Instance.SetTimeScale(0);
            dialogueBox.SetActive(true);
        }else{
            dialogueStatus = false;
            dialogueBox.SetActive(false);
            GameManager.Instance.SetTimeScale(1);
        }
    }

    protected void SetDialogueText(string text){
        dialogueText.text = text;
        SetDialogueActive(true);
    }

    protected void LoadNextScene()   {
        SceneManager.LoadScene(nextSceneId);
    }

}
