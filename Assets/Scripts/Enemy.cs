using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private string type;
    
    [SerializeField]
    private float speed;

    private Stack<Node> path;

    [SerializeField]
    private Stat health;

    public Point GridPositon { get; set; }

    private Vector3 destination;

    public bool IsActive { get; set; }

    private void Awake()
    {
        health.Initialize();
    }

    private void Update()
    {
        Move();
    }

    public void Spawn(int health, string type, Vector3? spawnpoint=null, Stack<Node>? initialPath=null)
    {
        transform.position = spawnpoint ?? LevelManager.Instance.GreenPortal.transform.position;

        this.health.MaxVal = health;
        this.health.CurrentVal = this.health.MaxVal;
        this.type = type;

        StartCoroutine(Scale(new Vector3(0.1f,0.1f),new Vector3(1,1), false));

        if (initialPath == null) SetPath(LevelManager.Instance.Path);
        else SetPath(initialPath);
    }

    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove)
    {
        IsActive = false;
        
        float progress = 0;

        while (progress <=1)
        {
            transform.localScale = Vector3.Lerp(from,to,progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
        IsActive = true;
        if (remove)
        {
            Release();
        }
    }

    private void Move()
    {
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    GridPositon = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;

            GridPositon = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coral")
        {
            StartCoroutine(Scale(new Vector3(1,1),new Vector3(0.1f,0.1f), true));

            GameManager.Instance.Lives--;
            UIUpdater.Instance.UpdateLives(GameManager.Instance.Lives);
        }
    }

    private void Release()
    {
        IsActive = false;
        //GridPosition = LevelManager.Instance.GreenSpawn;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveEnemy(this);
        if (type == "Jeep")
        {
            // Enemy enemy = Pool.GetObject("Soldier").GetComponent<Enemy>();
            // enemy.Spawn(health,"Soldier");
            // activeEnemies.Add(enemy);
            GameManager.Instance.JeepDestroy(transform.position, path);
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsActive)
        {
            health.CurrentVal -= damage;
            //Debug.Log(health.CurrentVal);
        }
        if (health.CurrentVal <= 0)
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
        }
    }
}
