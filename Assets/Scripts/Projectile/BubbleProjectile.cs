using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleProjectile : Projectile
{
    // Start is called before the first frame update
    [SerializeField]
    private float missRate;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Random.Range(0.0f, 1.0f) > missRate)
            {
                if (target.gameObject == other.gameObject)
                {
                    target.TakeDamage(parent.Damage);
                    GameManager.Instance.Pool.ReleaseObject(gameObject);
                }
            }
            else
            {
                GameManager.Instance.Pool.ReleaseObject(gameObject);
            }

        }
    }
}
