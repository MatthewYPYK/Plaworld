using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public PlaBtn ClickedBtn { get; set; }

    [SerializeField]
    private PlaRange selectedPla;
    [SerializeField]
    private int currency;

    private int wave = 0;
    
    // [SerializeField]
    // private Text waveText;

    [SerializeField]
    private GameObject waveBtn;

    private List<Enemy> activeEnemies = new List<Enemy>();

    public ObjectPool Pool {get; set;}

    public bool WaveActive
    {
        get {
            return activeEnemies.Count > 0;
        }
    }

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
    }
    // Start is called before the first frame update
    void Start()
    {
        Currency = 150;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickPla(PlaBtn plaBtn)
    {
        if (Currency >= plaBtn.Price)
        {
            this.ClickedBtn = plaBtn;
            Debug.Log("PlaBtn: " + ClickedBtn);
            Hover.Instance.Activate(plaBtn.Sprite);
        }
    }

    public void BuyPla()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency = Currency - ClickedBtn.Price;
            Debug.Log("Currency: " + Currency);
            Hover.Instance.Deactivate();
            Debug.Log("PlaBtn deac: " + ClickedBtn);
        }
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        wave++;
        // waveText.text = string.Format("Wave : ",wave);
        StartCoroutine(SpawnWave());
        
        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();

        for (int i = 0; i < wave; i++){
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

            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(2.5f);
        }

    }

    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);

        if (!WaveActive)
        {
            waveBtn.SetActive(true);
        }
    }

    public void SelectPla(PlaRange plaTower)
    {
        Debug.Log("GameManager: SelectPla: " + plaTower);
        // if (selectedPla != null)
        // {
        //     selectedPla.Select();
        // }
        selectedPla = plaTower;
        selectedPla.Select();
        // Hover.Instance.Activate(selectedPla.plaData.Sprite);

    }
}
