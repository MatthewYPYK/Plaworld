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

    public void UpdateCurrency(int value)
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

    void Awake()
    {
        textMesh_balance = BalanceValue.GetComponent<TextMeshProUGUI>();
        Debug.Log("AWAKE UIUpdater: " + textMesh_balance.text);

        textMesh_lives = Lives.GetComponent<TextMeshProUGUI>();
        Debug.Log("AWAKE UIUpdater: " + textMesh_lives.text);

        textMesh_waves = Waves.GetComponent<TextMeshProUGUI>();
        Debug.Log("AWAKE UIUpdater: " + textMesh_waves.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
