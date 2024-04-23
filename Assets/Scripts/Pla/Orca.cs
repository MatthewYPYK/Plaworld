using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orca : Shark
{
    // Start is called before the first frame update
    public new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy target = other.GetComponent<Enemy>();
            if (target.gameObject == other.gameObject)
            {
                target.TakeDamage(damage);
                AudioManager.instance.Play("Eat");
            }
        }
    }
}
