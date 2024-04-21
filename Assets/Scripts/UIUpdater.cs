using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : Singleton<UIUpdater>
{
    // Start is called before the first frame update
    public GameObject BalanceValue;

    public GameObject Lives;
    public TextMeshProUGUI textMesh_balance;

    public TextMeshProUGUI textMesh_lives;
    public GameObject Waves;
    public TextMeshProUGUI textMesh_waves;

    public GameObject sellPla;
    public TextMeshProUGUI textMesh_SellPla;

    public GameObject plaInfoFrame;

    private GameManager gameManager;
    public void UpdateBalance(int value)
    {
        textMesh_balance.text = value.ToString();
    }

    public void UpdateLives(int value)
    {
        textMesh_lives.text = value.ToString();
    }

    public void UpdateWaves(int value)
    {
        textMesh_waves.text = "Wave : " + value.ToString();
    }
    public void UpdateSellMode(bool sellMode)
    {
        if (sellMode)
        {
            textMesh_SellPla.text = "Sell Mode: ON";
        }
        else
        {
            textMesh_SellPla.text = "Sell Button";
        }
    }

    public void UpdatePlaInfoFrame(bool displayInfo)
    {
        Debug.Log("UpdatePlaInfoFrame: " + displayInfo);
        plaInfoFrame.SetActive(displayInfo);
    }


    void Awake()
    {

        gameManager = GameManager.Instance;
        textMesh_balance = BalanceValue.GetComponent<TextMeshProUGUI>();
        //Debug.Log("AWAKE UIUpdater: " + textMesh_balance.text);

        textMesh_lives = Lives.GetComponent<TextMeshProUGUI>();
        UpdateBalance(gameManager.Balance);
        //Debug.Log("AWAKE UIUpdater: " + textMesh_lives.text);

        textMesh_waves = Waves.GetComponent<TextMeshProUGUI>();

        textMesh_SellPla = sellPla.GetComponent<TextMeshProUGUI>();
        //Debug.Log("AWAKE UIUpdater: " + textMesh_waves.text);
    }

    void Start()
    {
        UpdatePlaInfoFrame(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
