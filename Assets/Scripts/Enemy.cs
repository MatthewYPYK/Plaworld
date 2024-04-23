using System;
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

    public Point CurrentPostion { get; set; }
    public Point GridPosition { get; set; }

    private Vector3 destination;

    public bool IsActive { get; set; }

    private float counter = 0.0f;

    private float maxCounter = 8.0f;

    private float soldierCounter = 0.0f;

    private float maxSoldierCounter = 8.0f;

    private void Awake()
    {
        health.Initialize();
    }

    private void Update()
    {
        Move();
        if (IsActive)
        {
            counter += Time.deltaTime;
            if (counter >= maxCounter)
            {
                counter = 0.0f;
                switch (type)
                {
                    case "Tank":
                        GameManager.Instance.TankSkill(GridPosition);
                        break;
                    case "Wizard":
                        GameManager.Instance.WizardSkill(GridPosition);
                        break;
                    case "Cat":
                        GameManager.Instance.CatSkill(GridPosition);
                        break;
                    case "Soldier":
                        soldierCounter += Time.deltaTime;
                        if (soldierCounter >= maxSoldierCounter)
                        {
                            soldierCounter = 0.0f;
                            GameManager.Instance.SoldierSkill(GridPosition);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Spawn(string type, Vector3? spawnpoint=null, Stack<Node>? initialPath=null)
    {
        transform.position = spawnpoint ?? LevelManager.Instance.GreenPortal.transform.position;

        this.health.CurrentVal = this.health.MaxVal;
        this.type = type;
        switch (type)
        {
            case "Tank":
                maxCounter = 8.0f;
                break;
            case "Wizard":
                maxCounter = 5.0f;
                break;
            case "Cat":
                maxCounter = 5.0f;
                break;
            default:
                break;
        }
        StartCoroutine(Scale(new Vector3(0.1f,0.1f),new Vector3(1,1), false));
        GridPosition = LevelManager.Instance.GreenSpawn;
        if (type == "AirShip") SetPath(LevelManager.Instance.DefaultPath);
        else if (initialPath == null) SetPath(LevelManager.Instance.Path);
        else {
            GridPosition = initialPath.Pop().GridPosition;
            SetPath(initialPath);
        }
    }

    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove)
    {
        IsActive = false;

        if (remove) DestroyEffect();

        float progress = 0;

        while (progress <=1)
        {
            transform.localScale = Vector3.Lerp(from,to,progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to; 
        
        IsActive = true;

        if (remove) Release();

    }

    private void Move()
    {
        if (IsActive)
        {
            // TODO : check if the wanted gridposition is still walkable
            if (!AStar.TravelAble(CurrentPostion, GridPosition) && !(this.type == "AirShip"))
            {
                GridPosition = new(CurrentPostion.X, CurrentPostion.Y);
                RefreshPath();
                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    Debug.Log("(" + GridPosition.X + ", " + GridPosition.Y + ") -> (" + path.Peek().GridPosition.X + ", " + path.Peek().GridPosition.Y + ")");
                    if (AStar.TravelAble(GridPosition, path.Peek().GridPosition) || this.type == "AirShip")
                    {
                        CurrentPostion = new(GridPosition.X, GridPosition.Y);
                        GridPosition = path.Peek().GridPosition;
                        destination = path.Pop().WorldPosition;
                    }
                    else
                    {
                        Debug.Log("Find new path");
                        RefreshPath();
                    }
                }
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;
            Debug.Log("setPath");
            Debug.Log(GridPosition.X + ", " + GridPosition.Y);
            if (AStar.TravelAble(GridPosition, path.Peek().GridPosition) || this.type == "AirShip") 
            {
                CurrentPostion = new(GridPosition.X, GridPosition.Y);
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
            }
            else
            {
                Debug.Log("Find new path");
                RefreshPath();
            }
        }
    }

    private void RefreshPath()
    {
        Point start = GridPosition;
        while (path.Count != 1)
        {
            path.Pop();
        }
        Stack<Node> newPath = AStar.GetPath(start, path.Peek().GridPosition);
        // Debug.Log(newPath.Count);
        if (newPath.Count != 0)
        {
            SetPath(newPath);
        }
        else
        {
            Debug.Log("Handle Surrounded Enemy");
            GameManager.Instance.SelfDetonate(GridPosition);
            StartCoroutine(Scale(new Vector3(1,1),new Vector3(0.1f,0.1f), true));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coral")
        {
            StartCoroutine(Scale(new Vector3(1,1),new Vector3(0.1f,0.1f), true));

            this.type = type;
            GameManager.Instance.Lives--;
            if (type == "Cat"){
                GameManager.Instance.Lives = 0;
            }
            UIUpdater.Instance.UpdateLives(GameManager.Instance.Lives);
        }
    }

    private void DestroyEffect()
    {
        if (health.CurrentVal <= 0)
        {
            switch (type)
            {
                case "Jeep":
                    path.Push(AStar.GetNode(GridPosition));
                    GameManager.Instance.JeepDestroy(transform.position, path);
                    break;
                default:
                    break;
            }
        }
    }

    private void Release()
    {
        IsActive = false;
        counter = 0.0f;
        soldierCounter = 0.0f;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveEnemy(this);
    }

    public void TakeDamage(int damage)
    {
        if (IsActive)
        {
            health.CurrentVal -= damage;
            //Debug.Log(health.CurrentVal);
            if (health.CurrentVal <= 0)
            {
                StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
            }
        }
    }
}
