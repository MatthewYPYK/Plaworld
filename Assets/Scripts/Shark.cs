using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float speed;

    [SerializeField]
    private float damage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
