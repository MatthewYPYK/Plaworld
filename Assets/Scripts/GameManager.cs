using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlaBtn ClickedBtn { get; private set; }

    [SerializeField]
    private int currency;

    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            this.currency = value;
            UIUpdater.Instance.UpdateCurrency(currency);
        }
    }

    private void Awake() {
        Currency = 150;
        Debug.Log("AWAKE Currency: " + Currency);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Currency = 150;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickPla(PlaBtn plaBtn)
    {
        if (Currency >= plaBtn.Price)
        {
            this.ClickedBtn = plaBtn;
            Hover.Instance.Activate(plaBtn.Sprite);

        }

    }

    public void BuyPla()
    {


        if (Currency >= ClickedBtn.Price)
        {
            Currency = Currency - ClickedBtn.Price;
            Hover.Instance.Deactivate();
            this.ClickedBtn = null;
        }
    }
}
