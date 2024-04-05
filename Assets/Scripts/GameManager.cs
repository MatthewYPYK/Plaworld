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
    private int balance;

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

    private List<Enemy> activeEnemies = new List<Enemy>();

    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get
        {
            return activeEnemies.Count > 0;
        }
    }

    public int Balance
    {
        get
        {
            return balance;
        }
        set
        {
            this.balance = value;
            UIUpdater.Instance.UpdateBalance(balance);
        }
    }

    public int Lives
    {
        get
        {
            return lives;
        }

        set
        {
            this.lives = value;

            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }

            // livesTxt.text = lives.ToString();

        }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }
    // Start is called before the first frame update
    void Start()
    {

        Lives = 10;

    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickPla(PlaBtn plaBtn)
    {
        if (Balance >= plaBtn.Price && !WaveActive)
        {
            this.ClickedBtn = plaBtn;
            //Debug.Log("PlaBtn: " + ClickedBtn);
            Hover.Instance.Activate(plaBtn.Sprite);
        }
    }

    public void BuyPla()
    {
        if (Balance >= ClickedBtn.Price)
        {
            Balance = Balance - ClickedBtn.Price;
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
            int enemyIndex = Random.Range(0, 4);

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
            enemy.Spawn(health);

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
            balance += wave * 10;
            UIUpdater.Instance.UpdateBalance(GameManager.Instance.Balance);
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
            Time.timeScale = 0;
        }

    }

    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

}
