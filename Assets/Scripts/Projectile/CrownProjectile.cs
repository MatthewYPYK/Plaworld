using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownProjectile : Projectile
{
    // Start is called before the first frame update
    [SerializeField]
    private int coinsGained;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (target.gameObject == other.gameObject)
            {
                GameManager.Instance.Balance += coinsGained;
                target.TakeDamage(parent.Damage);
                GameManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }
}
