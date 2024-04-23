using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    private static Dictionary<Point, Node> nodes;

    public static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();
        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static Stack<Node> GetPath(Point start, Point goal, Point? target = null)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new();

        HashSet<Node> closedList = new();

        Stack<Node> finalPath = new Stack<Node>();

        Node currentNode = nodes[start];
        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPos = new(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
                    if (neighbourPos == target) 
                    {
                        continue;
                    }
                    else if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].WalkAble && neighbourPos != currentNode.GridPosition)
                    {
                        int gCost = 0;
                        if (Math.Abs(x - y) == 1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if (!ConnectedDiagonally(currentNode, nodes[neighbourPos], target))
                            {
                                continue;
                            }
                            gCost = 14;
                        }
                        Node neighbour = nodes[neighbourPos];
                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                neighbour.CalcValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            neighbour.CalcValues(currentNode, nodes[goal], gCost);
                        }
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }

            if (currentNode == nodes[goal])
            {
                while(currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;  
                }
                break;
            }
        }

        // for Astar debugging
        //GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList, closedList, finalPath);
        // -------------
        return finalPath;
    }

    public static bool CanPlacePla(Point start, Point goal, Point target)
    {
        Stack<Node> path = GetPath(start,goal,target);
        return path.Count > 0;
    }

    private static bool ConnectedDiagonally(Node currentNode, Node neighbor, Point? target = null)
    {
        Point direction = neighbor.GridPosition - currentNode.GridPosition;

        Point first = new(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);
        Point second = new(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);
        bool targetIsFirst = first == target;
        bool targetIsSecond = second == target;
        return !(LevelManager.Instance.InBounds(first) && (!LevelManager.Instance.Tiles[first].WalkAble || targetIsFirst)) && !(LevelManager.Instance.InBounds(second) && (!LevelManager.Instance.Tiles[second].WalkAble || targetIsSecond));
    }

    public static bool TravelAble(Point current, Point destination)
    {
        if (current.X == destination.X || current.Y == destination.Y)
        {
            return LevelManager.Instance.Tiles[destination].WalkAble;
        } else
        {
            return ConnectedDiagonally(nodes[current],nodes[destination]);
        }
    }

    public static Node GetNode(Point point)
    {
        return nodes[point];
    }
}
