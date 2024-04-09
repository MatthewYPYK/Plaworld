using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaRangeDot : PlaRange
{
    // Start is called before the first frame update
    private ArrayList enemies = new ArrayList();
    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float attackCooldown;

    public int Damage { get => damage; set => damage = value; }


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

        else
        {
            canAttack = false;
            foreach (Enemy enemy in enemies)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.GetComponent<Enemy>());
        }


    }

    public override void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.GetComponent<Enemy>());
        }

    }

}
