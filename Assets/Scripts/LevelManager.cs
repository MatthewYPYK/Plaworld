using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    public CameraMovement cameraMovement;

    private Point greenSpawn,coral;

    [SerializeField]
    private GameObject greenPortalPrefab;

    [SerializeField]
    private GameObject coralPrefab;

    public Portal GreenPortal {get; set;}

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

    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        createLevel();
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

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));


        for (int y = 0; y < mapData.Length; y++)
        {
            rowData = mapData[y].ToCharArray();

            for (int x = 0; x < rowData.Length; x++)
            {
                PlaceTile(rowData[x].ToString(), x, y, worldStart);
            }
        }
        maxTile = Tiles[new Point(rowData.Length - 1, mapData.Length - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

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
        greenSpawn = new Point(1,3);
        GameObject tmp = (GameObject)Instantiate(greenPortalPrefab,Tiles[greenSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        GreenPortal = tmp.GetComponent<Portal>();
        GreenPortal.name = "GreenPortal";

        coral = new Point(10,1);
        Instantiate(coralPrefab,Tiles[coral].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    public bool InBounds(Point a)
    {
        return a.X >= 0 && a.Y >= 0 && a.X < mapSize.X && a.Y < mapSize.Y;
    }

    public void GeneratePath()
    {
        path = AStar.GetPath(greenSpawn, coral);
    }

    public Point GreenSpawn
    {
        get{
            return greenSpawn;
        }
    }
    
    // public bool InBounds(Point a)
    // {
    //     return a.X >= 0 && a.Y >= 0 && a.X < mapSize.X && a.Y < mapSize.Y;
    // }
}
