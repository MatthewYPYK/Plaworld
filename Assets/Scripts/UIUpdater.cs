using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIUpdater : Singleton<UIUpdater>
{
    // Start is called before the first frame update
    public GameObject BalanceValue;
    public TextMeshProUGUI textMesh_balance;

    public void UpdateCurrency(int value)
    {
        textMesh_balance.text = value.ToString();
    }



    void Awake()
    {
        textMesh_balance = BalanceValue.GetComponent<TextMeshProUGUI>();
        Debug.Log("AWAKE UIUpdater: " + textMesh_balance.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
