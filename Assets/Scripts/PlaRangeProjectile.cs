using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaRangeProjectile : PlaRange
{
    [SerializeField]
    private string projectileType;
    private Enemy target;

    public Enemy Target
    {
        get
        {
            return target;
        }
    }

    private Queue<Enemy> enemy = new Queue<Enemy>();

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float attackCooldown;


    [SerializeField]
    private float projectileSpeed;


    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    public int Damage { get => damage; set => damage = value; }
    // // Update is called once per frame

    public override void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        if (target == null && enemy.Count > 0)
        {
            target = enemy.Dequeue();
        }
        if (target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();

                canAttack = false;
            }
        }
    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;
        //Debug.Log("Projectile speed PArent: " + projectileSpeed);
        projectile.Initialize(this);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Enter");
        if (other.tag == "Enemy")
        {
            //Debug.Log("Enter the enemy");
            enemy.Enqueue(other.GetComponent<Enemy>());
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
        }
    }

}
