using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlaBtn ClickedBtn { get; private set; }

    [SerializeField]
    private int currency;

    private int wave = 0;

    private int lives = 2;

    private bool gameOver = false;

    // [SerializeField]
    // private Text livesTxt;
    
    // [SerializeField]
    // private Text waveText;

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject gameOverMenu;

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
        Currency = 150;
        Debug.Log("AWAKE Currency: " + Currency);
    }
    // Start is called before the first frame update
    void Start()
    {
        Lives = 10;
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

        if (!WaveActive && !gameOver)
        {
            waveBtn.SetActive(true);
        }
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
}
