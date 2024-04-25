using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    protected float missRate;
    [SerializeField]
    protected float critRate;
    [SerializeField]
    protected float critMultiplier;
    protected Enemy target;

    protected PlaRangeProjectile parent;
    // Start is called before the first frame update
    protected void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveToTarget();
    }


    public void Initialize(PlaRangeProjectile parent)
    {
        this.target = parent.Target;
        this.parent = parent;
    }
    private void moveToTarget()
    {
        if (target != null && target.IsActive)
        {
            //Debug.Log(parent);
            //Debug.Log(parent.ProjectileSpeed);
            // Debug.Log(Time.deltaTime * parent.ProjectileSpeed);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);

            Vector2 orientation = target.transform.position - transform.position;
            float angle = Mathf.Atan2(orientation.y, orientation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        else if (!target.IsActive)
        {
            //Debug.Log("Target is not active");
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            if (target.gameObject == other.gameObject)
            {
                if (randomValue > 1 - critRate)
                {
                    target.TakeDamage(Convert.ToInt32(Math.Round(parent.Damage * critMultiplier)));
                    GameManager.Instance.Pool.ReleaseObject(gameObject);
                }
                else if (randomValue > missRate)
                {
                    target.TakeDamage(Convert.ToInt32(parent.Damage));
                    GameManager.Instance.Pool.ReleaseObject(gameObject);
                }
                else
                {
                    GameManager.Instance.Pool.ReleaseObject(gameObject);
                }
            }
        }
    }
}
