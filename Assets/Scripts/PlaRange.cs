using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaRange : MonoBehaviour
{

    // private SpriteRenderer spriteRenderer;




    private bool onStartEnableRange;
    private bool isEnableRange;


    void Start()
    {
        //Debug.Log("Initial Projectile speed: " + projectileSpeed);
        setRangeEnable(onStartEnableRange);
        isEnableRange = onStartEnableRange;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Select()
    {
        //Debug.Log("Tower selected");
        // spriteRenderer.enabled = !spriteRenderer.enabled;
        setRangeEnable(!isEnableRange);
    }

    public void setRangeEnable(bool enable)
    {
        //Debug.Log("Tower selected");
        // spriteRenderer.enabled = !spriteRenderer.enabled;
        foreach (Transform child in transform)
        {
            SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
            if (childSpriteRenderer != null)
            {
                childSpriteRenderer.enabled = enable;
            }
        }
        isEnableRange = enable;
    }

    public abstract void Attack();

    public abstract void OnTriggerEnter2D(Collider2D other);

    public abstract void OnTriggerExit2D(Collider2D other);
}
