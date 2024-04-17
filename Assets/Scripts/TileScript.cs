using System;
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
    private int plaPrice = 0;
    private PlaRange myPlaRange;
    private string plaType = null;

    public string PlaType { get => plaType; set => plaType = value; }

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
                if (IsBlocked)
                {
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
                if (myPlaRange != null)
                {
                    GameManager.Instance.SelectPla(myPlaRange);
                }
                if (GameManager.Instance.SellMode)
                {
                    GameManager.Instance.Balance += (int)Math.Floor(plaPrice * GameManager.Instance.SellMultiplier);
                    GameManager.Instance.SellButtonClick();
                    RefreshTile();
                    LevelManager.Instance.GeneratePath();
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


        PlaBtn plaBtn = GameManager.Instance.ClickedBtn;
        PlaType = plaBtn.TowerName;
        pla = (GameObject)Instantiate(plaBtn.PlaPrefab, transform.position, Quaternion.identity);
        pla.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y + 1;


        // if component is not a shark
        if (plaBtn.HasRange)
        {
            myPlaRange = (PlaRange)pla.transform.GetChild(0).GetComponent<PlaRange>();
        }
        if (plaBtn.IsPermanent)
        {
            pla.transform.SetParent(transform);

            IsEmpty = false;
            WalkAble = false;
            ColorTile(Color.white);
        }
        int newPrice = GameManager.Instance.BuyPla();
        plaPrice = newPrice == -1 ? plaPrice : newPrice;

        //Debug.Log("TileScript: PlacePla: pla: " + pla);
        //Debug.Log("TileScript: PlacePla: myPla: " + myPla);
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
            //StartCoroutine(myPla.Scale(new Vector3(1,1),new Vector3(0.1f,0.1f), true));
            Destroy(pla);
            pla = null;
        }
        myPlaRange = null;
        PlaType = null;
        plaPrice = 0;
    }

    public void TransformStone()
    {
        RefreshTile();
        PlaBtn stoneBtn = PlaManager.Instance.getStoneBtn();
        PlaType = stoneBtn.TowerName;
        pla = (GameObject)Instantiate(stoneBtn.PlaPrefab, transform.position, Quaternion.identity);
        pla.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y + 1;

        if (stoneBtn.IsPermanent)
        {
            pla.transform.SetParent(transform);
            IsEmpty = false;
            WalkAble = false;
            ColorTile(Color.white);
        }

        plaPrice = stoneBtn.Price;
    }
}
