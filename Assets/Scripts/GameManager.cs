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
    public int Wave => wave; // wave getter

    private int lives;

    private bool gameOver = false;
    private bool sellMode = false;
    public bool SellMode
    {
        get
        {
            return sellMode;
        }
    }
    [SerializeField]

    private float sellMultiplier;

    public float SellMultiplier
    {
        get
        {
            return sellMultiplier;
        }
    }

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject gameOverMenu;

    // TODO : this is a list of active enemies
    private List<Enemy> activeEnemies = new List<Enemy>();

    public ObjectPool Pool { get; set; }

    public int WaveReward;

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
        if (Input.GetKeyDown(KeyCode.Equals) && !gameOver) Time.timeScale += 1;
        if (Input.GetKeyDown(KeyCode.Minus) && Time.timeScale > 0) Time.timeScale -= 1;
    }

    public void PickPla(PlaBtn plaBtn)
    {
        if (Balance >= plaBtn.Price)

        // if (Balance >= plaBtn.Price && !WaveActive)
        {
            this.ClickedBtn = plaBtn;
            //Debug.Log("PlaBtn: " + ClickedBtn);
            Hover.Instance.Activate(plaBtn.Sprite);
        }
    }

    public int BuyPla()
    {
        int price = ClickedBtn.Price;
        if (Balance >= ClickedBtn.Price)
        {
            Balance = Balance - ClickedBtn.Price;
            //Debug.Log("Currency: " + Currency);
            Hover.Instance.Deactivate();
            //Debug.Log("PlaBtn deac: " + ClickedBtn);
            return price;
        }
        return -1;
    }

    public void SellButtonClick()
    {
        if (!WaveActive){
            sellMode = !sellMode;
            UIUpdater.Instance.UpdateSellMode(sellMode);

            selectedPla = null;
            Hover.Instance.Deactivate();
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
        WaveReward = wave * 5; // default WaveReward
        
        LevelManager.Instance.GeneratePath();
        if(WaveManager.Instance.IsWaveDefined(wave))
            WaveManager.Instance.StartWave(wave);
        else
            StartCoroutine(SpawnWave());
        UIUpdater.Instance.UpdateWaves(wave);

        waveBtn.SetActive(false);
    }

    public Enemy SpawnEnemy(string type){
        Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
        enemy.Spawn(type);
        activeEnemies.Add(enemy);
        return enemy;
    }

    private IEnumerator SpawnWave()
    {
        int waveValue = 0;
        int enemyIndex = 0;

        while (waveValue < wave)
        {
            if (wave < 3)
            {
                enemyIndex = 0;
                waveValue++;
            }
            else if (wave < 5)
            {
                enemyIndex = Random.Range(0, 2);
                switch (enemyIndex)
                {
                    case 0:
                        waveValue++;
                        break;
                    case 1:
                        waveValue += 3;
                        if (waveValue > wave)
                        {
                            enemyIndex = 0;
                            waveValue -= 3;
                            waveValue++;
                        }
                        break;
                }
            }
            else if (wave < 7)
            {
                enemyIndex = Random.Range(0, 3);
                switch (enemyIndex)
                {
                    case 0:
                        waveValue++;
                        break;
                    case 1:
                        waveValue += 3;
                        if (waveValue > wave)
                        {
                            enemyIndex = 0;
                            waveValue -= 3;
                            waveValue++;
                        }
                        break;
                    case 2:
                        waveValue += 5;
                        if (waveValue > wave)
                        {
                            enemyIndex = 0;
                            waveValue -= 5;
                            waveValue++;
                        }
                        break;
                }
            }
            else
            {
                enemyIndex = Random.Range(0, 4);
                switch (enemyIndex)
                {
                    case 0:
                        waveValue++;
                        break;
                    case 1:
                        waveValue += 3;
                        if (waveValue > wave)
                        {
                            enemyIndex = 0;
                            waveValue -= 3;
                            waveValue++;
                        }
                        break;
                    case 2:
                        waveValue += 5;
                        if (waveValue > wave)
                        {
                            enemyIndex = 0;
                            waveValue -= 5;
                            waveValue++;
                        }
                        break;
                    case 3:
                        waveValue += 5;
                        if (waveValue > wave)
                        {
                            enemyIndex = 0;
                            waveValue -= 5;
                            waveValue++;
                        }
                        break;
                }
            }

            string type = string.Empty;

            switch (enemyIndex)
            {
                case 0:
                    type = "Soldier";
                    break;
                case 1:
                    type = "Jeep";
                    break;
                case 2:
                    type = "Tank";
                    break;
                case 3:
                    type = "AirShip";
                    break;
            }

            SpawnEnemy(type);

            yield return new WaitForSeconds(1f);
        }


    }

    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);

        if (!WaveActive && !gameOver)
        {
            waveBtn.SetActive(true);
            Debug.Log("currency is added");
            balance += WaveReward;
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

    public void JeepDestroy(Vector3 position, Stack<Node> initialPath)
    {
        int total_number = 2;
        for (int i = 0; i < total_number; i++)
        {
            Enemy enemy = Pool.GetObject("Soldier").GetComponent<Enemy>();
            enemy.Spawn("Soldier", position, new(new Stack<Node>(initialPath)));
            activeEnemies.Add(enemy);
        }
    }

    private List<Point> getPossibleFish(Point currentPos, List<string> unselected)
    {
        List<Point> possibleFish = new();
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                Point neighbourPos = new(currentPos.X - dx, currentPos.Y - dy);
                if (LevelManager.Instance.InBounds(neighbourPos))
                {
                    TileScript neighbourTile = LevelManager.Instance.Tiles[neighbourPos];
                    if (!neighbourTile.WalkAble && !unselected.Contains(neighbourTile.PlaType))
                    {
                        possibleFish.Add(neighbourPos);
                    }
                }
            }
        }
        return possibleFish;
    }

    public void TankSkill(Point currentPos)
    {

        // Debug.Log("shoot some fish");
        List<Point> possibleFish = getPossibleFish(currentPos, new List<string>());
        if (possibleFish.Count != 0)
        {
            int randomIndex = Random.Range(0, possibleFish.Count);
            LevelManager.Instance.Tiles[possibleFish[randomIndex]].RefreshTile();
            LevelManager.Instance.GeneratePath();
        }
    }

    public void WizardSkill(Point currentPos)
    {
        List<Point> possibleFish = getPossibleFish(currentPos, new List<string> { "Stone" });
        if (possibleFish.Count != 0)
        {
            int randomIndex = Random.Range(0, possibleFish.Count);
            LevelManager.Instance.Tiles[possibleFish[randomIndex]].TransformStone();
        }
    }

    public void SoldierSkill(Point currentPos)
    {
        List<Point> possibleFish = new();
        // Debug.Log("shoot some fish");
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                Point neighbourPos = new(currentPos.X - dx, currentPos.Y - dy);
                if (LevelManager.Instance.InBounds(neighbourPos) && !LevelManager.Instance.Tiles[neighbourPos].WalkAble)
                {
                    possibleFish.Add(neighbourPos);
                }
            }
        }
        if (possibleFish.Count != 0)
        {
            int randomIndex = Random.Range(0, possibleFish.Count);
            LevelManager.Instance.Tiles[possibleFish[randomIndex]].RefreshTile();
            LevelManager.Instance.GeneratePath();
        }
    }

    public void SelfDetonate(Point currentPos){
        // Debug.Log("shoot some fish");
        List<Point> possibleFish = getPossibleFish(currentPos, new List<string>());
        for (int i = 0; i < possibleFish.Count; i++)
        {
            //int randomIndex = Random.Range(0, possibleFish.Count);
            LevelManager.Instance.Tiles[possibleFish[i]].RefreshTile();
            LevelManager.Instance.GeneratePath();
        }
        
    }
}
