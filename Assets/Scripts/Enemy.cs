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

    public Point GridPositon { get; set; }
    
    public bool FixPath { get; protected set; }

    private Vector3 destination;

    public bool IsActive { get; set; }

    private float tankCounter = 0.0f;

    private float maxTankCounter = 8.0f;

    private void Awake()
    {
        health.Initialize();
    }

    private void Update()
    {
        Move();
        if (IsActive)
        {
            switch (type)
            {
                case "Tank":
                    tankCounter += Time.deltaTime;
                    if (tankCounter >= maxTankCounter)
                    {
                        tankCounter = 0.0f;
                        GameManager.Instance.TankSkill(GridPositon);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void Spawn(string type, Vector3? spawnpoint=null)
    {
        transform.position = spawnpoint ?? LevelManager.Instance.GreenPortal.transform.position;

        this.health.CurrentVal = this.health.MaxVal;
        this.type = type;

        StartCoroutine(Scale(new Vector3(0.1f,0.1f),new Vector3(1,1), false));
        UpdatePath();
    }
    public void UpdatePath(){
        var currentPos = transform.position;
        var gridPos = LevelManager.Instance.WorldPosToGridPos(currentPos);
        if (type == "AirShip"){
            SetPath(LevelManager.Instance.DefaultPath);
            FixPath = true;
        }
        else SetPath(AStar.GetPath(gridPos, LevelManager.Instance.Coral));
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
        if (FixPath) return;
        
        if (newPath != null && newPath.Count > 0)
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
        }
    }

    private void DestroyEffect()
    {
        if (health.CurrentVal <= 0)
        {
            switch (type)
            {
                case "Jeep":
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
        tankCounter = 0.0f;
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
