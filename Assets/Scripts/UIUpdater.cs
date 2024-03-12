using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIUpdater : Singleton<UIUpdater>
{
    // Start is called before the first frame update
    public GameObject BalanceValue;

    TextMeshProUGUI textMesh_balance;

    public void UpdateCurrency()
    {
        textMesh_balance.text = GameManager.Instance.Currency.ToString();
    }
    void Start()
    {
        textMesh_balance = BalanceValue.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
