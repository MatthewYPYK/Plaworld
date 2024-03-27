using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlaBtn ClickedBtn { get; set; }

    [SerializeField]
    private PlaRange selectedPla;
    [SerializeField]
    private int currency;

    private int wave = 0;

    private int health = 10;

    private int lives;

    private bool gameOver = false;

    // [SerializeField]
    // private Text livesTxt;
    
    // [SerializeField]
    // private Text waveText;

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject gameOverMenu;

    // TODO : this is a list of active enemies
    private List<Enemy> activeEnemies = new List<Enemy>();

    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get
        {
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

    public int Lives
    {
        get {
            return lives;
        }

        set{
            this.lives = value;

            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }
            
            // livesTxt.text = lives.ToString();

        }
    }

    private void Awake() {
        Pool = GetComponent<ObjectPool>();
    }
    // Start is called before the first frame update
    void Start()
    {

        Lives = 10;
        Currency = 150;

    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickPla(PlaBtn plaBtn)
    {
        // TODO : if finish set new path activate place pla between wave
        if (Currency >= plaBtn.Price && !WaveActive)
        {
            this.ClickedBtn = plaBtn;
            //Debug.Log("PlaBtn: " + ClickedBtn);
            Hover.Instance.Activate(plaBtn.Sprite);
        }
    }

    public void BuyPla()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency = Currency - ClickedBtn.Price;
            //Debug.Log("Currency: " + Currency);
            Hover.Instance.Deactivate();
            //Debug.Log("PlaBtn deac: " + ClickedBtn);
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
        UIUpdater.Instance.UpdateWaves(wave);
        
        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();

        for (int i = 0; i < wave; i++)
        {
            int enemyIndex = Random.Range(2, 3);

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
            enemy.Spawn(health,type);

            if (wave % 3 == 0) // monster max health increase every 3 wave
            {
                health += 3;
            }

            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(2.5f);
        }

    }

    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);

        if (!WaveActive && !gameOver)
        {
            waveBtn.SetActive(true);
            Debug.Log("currency is added");
            currency += wave * 10;
            UIUpdater.Instance.UpdateCurrency(GameManager.Instance.Currency);
        }
    }

    public void SelectPla(PlaRange plaTower)
    {
        //Debug.Log("GameManager: SelectPla: " + plaTower);
        // if (selectedPla != null)
        // {
        //     selectedPla.Select();
        // }
        selectedPla = plaTower;
        selectedPla.Select();
        // Hover.Instance.Activate(selectedPla.plaData.Sprite);

    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }

    }

    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void JeepDestroy(Vector3 position, Stack<Node> initialPath)
    {
        int total_number = 2;
        for (int i = 0; i < total_number; i++)
        {
            Enemy enemy = Pool.GetObject("Soldier").GetComponent<Enemy>();
            enemy.Spawn(health, "Soldier", position, new(initialPath));
            activeEnemies.Add(enemy);
        }
    }
}
