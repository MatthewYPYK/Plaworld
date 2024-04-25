using System;
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
                float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomValue < missRate){
                    GameManager.Instance.Pool.ReleaseObject(gameObject);
                }
                else{
                    if (coinsGainedMin == coinsGainedMax)
                    {
                        coinsGained = coinsGainedMin;
                    }
                    else
                    {
                        coinsGained = UnityEngine.Random.Range(coinsGainedMin, coinsGainedMax + 1);
                    }
                    GameManager.Instance.Balance += coinsGained;
                    GameManager.Instance.activeReward += coinsGained;
                    if (randomValue > 1 - critRate)
                    {
                        target.TakeDamage(Convert.ToInt32(Math.Round(parent.Damage * critMultiplier)));
                    }
                    else
                    {
                        target.TakeDamage(parent.Damage);
                    }
                    target.TakeDamage(parent.Damage);
                    GameManager.Instance.Pool.ReleaseObject(gameObject);
                }

            }
        }
    }
}
