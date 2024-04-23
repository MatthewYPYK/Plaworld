using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownProjectile : Projectile
{
    // Start is called before the first frame update
    [SerializeField]
    private int coinsGainedMin;
    [SerializeField]
    private int coinsGainedMax;
    private int coinsGained = 0;

    protected new void Start()
    {
        base.Start();
        if (coinsGainedMin > coinsGainedMax)
        {
            Debug.LogError("Coins gained min is greater than coins gained max");
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (target.gameObject == other.gameObject)
            {
                if (coinsGainedMin == coinsGainedMax)
                {
                    coinsGained = coinsGainedMin;
                }
                else
                {
                    coinsGained = Random.Range(coinsGainedMin, coinsGainedMax + 1);
                }
                GameManager.Instance.Balance += coinsGained;
                GameManager.Instance.activeReward += coinsGained;
                target.TakeDamage(parent.Damage);
                GameManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }
}
