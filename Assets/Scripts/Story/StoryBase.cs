using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryBase : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] protected Image backGroundImage;
    [SerializeField] protected TMP_Text nameText;
    [SerializeField] protected TMP_Text dialogueText;
    [SerializeField] protected Image characterImage;
    protected bool dialogueStatus = false;
    public bool IsDialogueActive() => dialogueStatus;
    [SerializeField] private int nextSceneId = 0;
    protected int step;

    void Start() {
        step = 0;
        UpdateDialogue();
    }

    void Update()
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
