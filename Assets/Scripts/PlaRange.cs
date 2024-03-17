using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaRange : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        Debug.Log("Tower selected");
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
