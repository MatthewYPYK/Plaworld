using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    public void Spawn(int health)
    {
        transform.position = LevelManager.Instance.GreenPortal.transform.position;

        this.health.MaxVal = health;
        this.health.CurrentVal = this.health.MaxVal;

        StartCoroutine(Scale(new Vector3(0.1f,0.1f),new Vector3(1,1), false));

        SetPath(LevelManager.Instance.Path);
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
    }

    public void TakeDamage(int damage)
    {
        if (IsActive)
        {
            health.CurrentVal -= damage;
            Debug.Log(health.CurrentVal);
        }
        if (health.CurrentVal <= 0)
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
        }
    }
}
