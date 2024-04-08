using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }
    public bool WalkAble { get; set; }

    public bool IsEmpty { get; private set; }

    private GameObject pla = null;

    private PlaRange myPla;

    private Color32 fullColor = new Color32(255, 118, 118, 255);

    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    private SpriteRenderer spriteRenderer;

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(
                transform.position.x - (GetComponent<SpriteRenderer>().bounds.size.x / 2) + 2.134f,
                transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2)

            );
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        WalkAble = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Debug.Log("TileScript: OnMouseOver: GameManager.Instance.ClickedBtn: " + GameManager.Instance.ClickedBtn);
            // Debug.Log("Mouse over tile: " + "(" + GridPosition.X + ", " + GridPosition.Y + ")");
            if (GameManager.Instance.ClickedBtn != null)
            {
                bool IsBlocked = !LevelManager.Instance.CanPlacePla(GridPosition);
                if (IsBlocked){
                    ColorTile(fullColor);
                }
                else if (IsEmpty)
                {
                    ColorTile(emptyColor);
                }
                if (!IsEmpty || IsBlocked)
                {
                    ColorTile(fullColor);
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    PlacePla();
                    // TODO : set new path for enemy
                }
            }
            else if (GameManager.Instance.ClickedBtn == null
            && Input.GetMouseButtonDown(0))
            {
                //Debug.Log("TileScript: OnMouseOver: myPla: " + myPla);
                if (myPla != null)
                {
                    GameManager.Instance.SelectPla(myPla);
                }
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlacePla()
    {

        pla = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.PlaPrefab, transform.position, Quaternion.identity);
        pla.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y + 1;

        pla.transform.SetParent(transform);

        // set myPla to PlaTower script
        myPla = pla.transform.GetChild(0).GetComponent<PlaRange>();

        //Debug.Log("TileScript: PlacePla: pla: " + pla);
        //Debug.Log("TileScript: PlacePla: myPla: " + myPla);
        IsEmpty = false;

        ColorTile(Color.white);

        GameManager.Instance.BuyPla();

        WalkAble = false;
    }

    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    public void RefreshTile()
    {
        // Debug.Log("Pla at (" + GridPosition.X + ", " + GridPosition.Y + ") is destroy");
        IsEmpty = true;
        WalkAble = true;
        if (pla != null)
        {
            Destroy(pla);
            pla = null;
        }
        myPla = null;
    }
}
