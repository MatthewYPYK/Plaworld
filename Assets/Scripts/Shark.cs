using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // if position is off the camera, return to pool
        DeleteOnExit();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy target = other.GetComponent<Enemy>();
            if (target.gameObject == other.gameObject)
            {
                target.TakeDamage(damage);
                GameManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }

    private void DeleteOnExit()
    {
        if (transform.position.x < -20)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }
}
