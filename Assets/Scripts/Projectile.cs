using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Enemy target;

    private PlaRange parent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveToTarget();
    }


    public void Initialize(PlaRange parent)
    {
        this.target = parent.Target;
        this.parent = parent;
    }
    private void moveToTarget()
    {
        if (target != null && target.IsActive)
        {
            Debug.Log(parent);
            Debug.Log(parent.ProjectileSpeed);
            // Debug.Log(Time.deltaTime * parent.ProjectileSpeed);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);

        }

        else if (!target.IsActive)
        {
            Debug.Log("Target is not active");
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }
}
