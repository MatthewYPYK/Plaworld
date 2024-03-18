using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AStarDebugger : MonoBehaviour
{
    [SerializeField]
    private TileScript start, goal;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject debugTilePrefab;

    [SerializeField]
    private GameObject fPrefab;

    [SerializeField]
    private GameObject gPrefab;

    [SerializeField]
    private GameObject hPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AStar.GetPath(start.GridPosition,goal.GridPosition);
        }
    }

    private void ClickTile()
    {
        System.Console.WriteLine("Hello World!");
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();
                if (tmp != null)
                {
                    if (start == null)
                    {
                        start = tmp;
                        //start.ChangeTileColor(Color.red);
                        CreateDebugTile(start.WorldPosition, Color.red);
                    }
                    else if (goal == null)
                    {
                        goal = tmp;
                        //goal.ChangeTileColor(Color.gray);
                        CreateDebugTile(goal.WorldPosition, Color.gray);
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList, Stack<Node> finalPath)
    {
        foreach (Node node in openList)
        {
            if (node.TileRef != start && node.TileRef != goal)
            {
                //node.TileRef.ChangeTileColor(Color.blue);
                CreateDebugTile(node.TileRef.WorldPosition, Color.blue, node);
                PointToParent(node, node.TileRef.WorldPosition);
            }
        }

        foreach (Node node in closedList)
        {
            if (node.TileRef != start && node.TileRef != goal && !finalPath.Contains(node))
            {
                //node.TileRef.ChangeTileColor(Color.blue);
                CreateDebugTile(node.TileRef.WorldPosition, Color.green, node);
                PointToParent(node, node.TileRef.WorldPosition);
            }
        }

        foreach (Node node in finalPath)
        {
            if (node.TileRef != start && node.TileRef != goal)
            {
                CreateDebugTile(node.TileRef.WorldPosition, Color.white, node);
                PointToParent(node, node.TileRef.WorldPosition);
            }
        }
    }

    private void PointToParent(Node node, Vector2 position)
    {
        if (node.Parent != null)
        {
            Vector2 real_position = position + new Vector2((float)2.1344444, 0);
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, real_position, Quaternion.identity);
            arrow.GetComponent<SpriteRenderer>().sortingOrder = 3;

            int x_len = node.GridPosition.X - node.Parent.GridPosition.X;
            int y_len = node.GridPosition.Y - node.Parent.GridPosition.Y;
            if (x_len < 0)
            {
                if (y_len < 0)
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, -45);
                }
                else if (y_len == 0)
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, 45);
                }
            } else if (x_len == 0)
            {
                if (y_len < 0)
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, 90);
                }
            } else
            {
                if (y_len < 0)
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, -145);
                }
                else if (y_len == 0)
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else
                {
                    arrow.transform.eulerAngles = new Vector3(0, 0, 145);
                }
            }
        }
        
    }

    private void CreateDebugTile(Vector3 worldPos, Color32 color, Node node = null)
    {
        GameObject debugTile = (GameObject)Instantiate(debugTilePrefab, worldPos + new Vector3((float)2.1344444, 0,0), Quaternion.identity);
        if (node != null)
        {
            Debug.Log("("+ node.GridPosition.X + "," + node.GridPosition.Y + ")" + " " + node.G);
            GameObject instantiated_G_Canvas = (GameObject)Instantiate(gPrefab, worldPos + new Vector3((float)2.1344444, 0, 0), Quaternion.identity);
            TextMeshProUGUI[] componentG = instantiated_G_Canvas.GetComponentsInChildren<TextMeshProUGUI>();
            componentG[0].text += node.G;
            GameObject instantiated_H_Canvas = (GameObject)Instantiate(hPrefab, worldPos + new Vector3((float)2.1344444, 0, 0), Quaternion.identity);
            TextMeshProUGUI[] componentH = instantiated_H_Canvas.GetComponentsInChildren<TextMeshProUGUI>();
            componentH[0].text += node.H;
            GameObject instantiated_F_Canvas = (GameObject)Instantiate(fPrefab, worldPos + new Vector3((float)2.1344444, 0, 0), Quaternion.identity);
            TextMeshProUGUI[] componentF = instantiated_F_Canvas.GetComponentsInChildren<TextMeshProUGUI>();
            componentF[0].text += node.G + node.H;
        }
        debugTile.GetComponent<SpriteRenderer>().color = color;
    }
}