using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] public TextAsset mapData;

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private float _TileSize = 0;

    [SerializeField]
    public CameraMovement cameraMovement;

    [SerializeField] public Point greenSpawn, coral;
    
    public Point GreenSpawn { 
        get => greenSpawn;
        set{
            greenSpawn = value;
            GreenPortal.transform.position = Tiles[greenSpawn].GetComponent<TileScript>().WorldPosition;
        }
     }
    public Point Coral { get => coral; }

    [SerializeField]
    private GameObject greenPortalPrefab;

    [SerializeField]
    private GameObject coralPrefab;

    public Portal GreenPortal { get; set; }

    [SerializeField]
    private Transform map;

    private Point mapSize;
    public Point MapSize { get => mapSize; }

    private Vector3 worldStart = new Vector3(0,0,0);

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
        AStar.CreateNodes();
        defaultPath = AStar.GetPath(greenSpawn, coral);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Point WorldPosToGridPos(Vector3 worldPos){
        var x = (worldPos.x - worldStart.x) / TileSize;
        var y = (worldStart.y - worldPos.y) / TileSize;
        return new Point((int)x, (int)y);
    }
    private void createLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelText();
        char[] rowData = null;

        mapSize = new Point(mapData[0].ToCharArray().Length, mapData.Length);

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
    [SerializeField] private GameObject invisibleTile;
    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        TileScript newTile;
        if (tileType == "n"){

            newTile = Instantiate(invisibleTile).GetComponent<TileScript>();
            newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map, true);
        
        } else {
            if (!int.TryParse(tileType, out int tileIndex)) return;
            newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
            newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map, false);
        }
        
    }

    private string[] ReadLevelText()
    {
        string data = mapData.text.Replace(Environment.NewLine, string.Empty);

        return Regex.Replace(data, @"\p{C}", "").Split('-');//.Replace(" ", "").Replace("\n", "").Replace("\r", "")
    }

    private void SpawnPortals()
    {
        GameObject tmp = (GameObject)Instantiate(greenPortalPrefab, Tiles[greenSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        GreenPortal = tmp.GetComponent<Portal>();
        GreenPortal.name = "GreenPortal";
        Instantiate(coralPrefab, Tiles[coral].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    public bool InBounds(Point a)
    {
        return a.X >= 0 && a.Y >= 0 && a.X < mapSize.X && a.Y < mapSize.Y;
    }

    public bool CanPlacePla(Point target)
    {
        return AStar.CanPlacePla(greenSpawn, coral, target);
    }

    public Bounds BoundingBox{
        get{
            Vector3 bottomRight = Tiles[new Point(MapSize.X - 1, MapSize.Y - 1)].transform.position;
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
