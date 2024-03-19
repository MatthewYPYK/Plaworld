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

    public void UpdateCurrency(int value)
    {
        textMesh_balance.text = value.ToString();
    }

    public void UpdateLives(int value)
    {
        textMesh_lives.text = value.ToString();
    }

    void Awake()
    {
        textMesh_balance = BalanceValue.GetComponent<TextMeshProUGUI>();
        Debug.Log("AWAKE UIUpdater: " + textMesh_balance.text);

        textMesh_lives = Lives.GetComponent<TextMeshProUGUI>();
        Debug.Log("AWAKE UIUpdater: " + textMesh_lives.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
