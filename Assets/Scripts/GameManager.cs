using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlaBtn ClickedBtn { get; private set; }

    [SerializeField]
    private int currency;

    public ObjectPool Pool {get; set;}

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
        Pool = GetComponent<ObjectPool>();
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

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int enemyIndex = Random.Range(0,4);

        string type = string.Empty;

        switch (enemyIndex)
        {
            case 0:
                type = "Soldier";
                break;
            case 1:
                type = "Tank";
                break;
            case 2:
                type = "Jeep";
                break;
            case 3:
                type = "AirShip";
                break;
        }

        Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
        enemy.Spawn();
        yield return new WaitForSeconds(2.5f);
    }
}
