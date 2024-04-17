using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private float _TileSize = 0;

    [SerializeField]
    public CameraMovement cameraMovement;

    private Point greenSpawn, coral;

    [SerializeField]
    private GameObject greenPortalPrefab;

    [SerializeField]
    private GameObject coralPrefab;

    public Portal GreenPortal { get; set; }

    [SerializeField]
    private Transform map;

    private Point mapSize;

    private Stack<Node> path;

    public Stack<Node> Path
    {
        get
        {
            if (path == null)
            {
                GeneratePath();
            }
            return new(new Stack<Node>(path));
        }
    }

    private Stack<Node> defaultPath;

    public Stack<Node> DefaultPath
    {
        get
        {
            return new(new Stack<Node>(defaultPath));
        }
    }

    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float TileSize
    {
        get {
            if (_TileSize > 0) return _TileSize;
            else return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
         }
    }

    // Start is called before the first frame update
    void Start()
    {
        createLevel();
        defaultPath = AStar.GetPath(greenSpawn, coral);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void createLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelText();
        char[] rowData = null;

        mapSize = new Point(mapData[0].ToCharArray().Length, mapData.Length);

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));


        for (int y = 0; y < mapData.Length; y++)
        {
            rowData = mapData[y].ToCharArray();

            for (int x = 0; x < rowData.Length; x++)
            {
                PlaceTile(rowData[x].ToString(), x, y, worldStart);
            }
        }
        cameraMovement.SetCam(BoundingBox);
        // cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);
    }



    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    private void SpawnPortals()
    {
        greenSpawn = new Point(1, 3);
        GameObject tmp = (GameObject)Instantiate(greenPortalPrefab, Tiles[greenSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        GreenPortal = tmp.GetComponent<Portal>();
        GreenPortal.name = "GreenPortal";

        coral = new Point(10, 1);
        Instantiate(coralPrefab, Tiles[coral].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    public bool InBounds(Point a)
    {
        return a.X >= 0 && a.Y >= 0 && a.X < mapSize.X && a.Y < mapSize.Y;
    }

    public void GeneratePath()
    {
        path = AStar.GetPath(greenSpawn, coral);
    }

    public bool CanPlacePla(Point target)
    {
        return AStar.CanPlacePla(greenSpawn, coral, target);
    }

    public Point GreenSpawn
    {
        get
        {
            return greenSpawn;
        }
    }
    
    public Bounds BoundingBox{
        get{
            Vector3 bottomRight = Tiles[new Point(mapSize.X - 1, mapSize.Y - 1)].transform.position;
            bottomRight += new Vector3(TileSize,-TileSize,0);
            Vector3 topLeft = Tiles[new Point(0, 0)].transform.position;

            Vector3 center = (topLeft + bottomRight)/2;
            Vector3 size = bottomRight - topLeft;
            Vector3 absSize = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));
            Bounds boundingBox = new Bounds(center, absSize);
            return boundingBox;
        }
    }

    public Point GreenSpawn1 { get => greenSpawn; set => greenSpawn = value; }
}
