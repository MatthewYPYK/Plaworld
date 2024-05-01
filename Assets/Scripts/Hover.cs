using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    private SpriteRenderer spriteRenderer;
    private GameObject range;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void Activate(Sprite sprite, GameObject range = null)
    {
        this.spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
        float pivot_adjust = LevelManager.Instance.TileSize / 2;
        if (range != null)
        {
            if (this.range != null)
            {
                Destroy(this.range);
            }
            //add range as a child of hover
            this.range = Instantiate(range, transform.position + Vector3.up * pivot_adjust + Vector3.left * pivot_adjust, Quaternion.identity);
            this.range.transform.parent = transform;

        }
    }

    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedBtn = null;

        if (range != null)
        {
            Destroy(range);
        }
    }
}