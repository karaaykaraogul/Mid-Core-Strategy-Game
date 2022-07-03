using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap {

    public event EventHandler OnLoaded;

    private Grid<TilemapObject> grid;
    //private Grid<PathNode> pathGrid;

    private List<TilemapObject> openList;
    private List<TilemapObject> closedList;
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Tilemap(int width, int height, float cellSize, Vector3 originPosition) 
    {
        grid = new Grid<TilemapObject>(width, height, cellSize, originPosition, (Grid<TilemapObject> g, int x, int y) => new TilemapObject(g, x, y));
        //pathGrid = new Grid<PathNode>(width, height, cellSize, originPosition, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public void SetDefaultGrid(int width, int height, TilemapObject.TilemapSprite tilemapSprite)
    {
        for(int i = 0; i < width; i++)
        {
            for(int j= 0; j< height; j++)
            {
                TilemapObject tilemapObject = grid.GetGridObject(i,j);
                if (tilemapObject != null) {
                    tilemapObject.SetTilemapSprite(tilemapSprite);
                }
            }
        }
    }

    public void SetTilemapSprite(Vector3 worldPosition, TilemapObject.TilemapSprite tilemapSprite) 
    {
        TilemapObject tilemapObject = grid.GetGridObject(worldPosition);
        if (tilemapObject != null) {
            tilemapObject.SetTilemapSprite(tilemapSprite);
        }
    }

    public void SetTilemapVisual(TilemapVisual tilemapVisual) 
    {
        tilemapVisual.SetGrid(this, grid);
    }

    public TilemapObject GetClickedTilemapInfo(Vector3 worldPosition)
    {
        return grid.GetGridObject(worldPosition);
    }

    public void SetTileNotBuildable(int x, int y)
    {
        var tile = grid.GetGridObject(x,y);
        tile.isBuildable = false;
    }

    public Grid<TilemapObject> GetGrid()
    {
        return grid;
    }

    public Vector3 GetClickedTilemapPositions(Vector3 worldPosition)
    {
        return grid.GetGridObjectPositions(worldPosition);
    }

    public void GetClickedTilemapObjectNo(Vector3 mouseWorldPosition, out int x, out int y)
    {
        var tilemapObject = grid.GetGridObject(mouseWorldPosition);
        x = tilemapObject.GetX();
        y = tilemapObject.GetY();
    }

    public bool GetClickedTilemapBuildAvailability(Vector3 worldPosition)
    {
        var tileMapObject = grid.GetGridObject(worldPosition);
        return tileMapObject.isBuildable;
    }
    public bool GetClickedTilemapBuildAvailability(int x, int y)
    {
        var tileMapObject = grid.GetGridObject(x,y);
        return tileMapObject.isBuildable;
    }

    #region Pathfinding
    public List<TilemapObject> FindPath(int startX, int startY, int endX, int endY)
    {
        TilemapObject startNode = grid.GetGridObject(startX, startY);
        TilemapObject endNode = grid.GetGridObject(endX, endY);

        if (startNode == null || endNode == null) {
            // Invalid Path
            return null;
        }

        openList = new List<TilemapObject> {startNode};
        closedList = new List<TilemapObject>();

        for(int x= 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetHeight(); y++)
            {
                TilemapObject tilemapNode = grid.GetGridObject(x,y);
                tilemapNode.gCost = 99999999;
                tilemapNode.CalculateFCost();
                tilemapNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode,endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            TilemapObject currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                Debug.Log("son node geldi");
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(TilemapObject neighbourNode in GetNeighbourList(currentNode))
            {
                if(closedList.Contains(neighbourNode)) continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();;
                    
                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        //Out of nodes on the openList
        return null;
    }

    private List<TilemapObject> GetNeighbourList(TilemapObject currentNode)
    {
        List<TilemapObject> neighbourList = new List<TilemapObject>();

        if(currentNode.GetX() - 1 >= 0)
        {
            //Left
            neighbourList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetY()));
            //Left Down
            if(currentNode.GetY() - 1 >= 0) neighbourList.Add(GetNode(currentNode.GetX()-1, currentNode.GetY()-1));
            //Left Up
            if(currentNode.GetY() + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.GetX()-1, currentNode.GetY()+1));
        }
        if(currentNode.GetX() + 1 < grid.GetWidth())
        {
            //Right
            neighbourList.Add(GetNode(currentNode.GetX() +1, currentNode.GetY()));
            //Right Down
            if(currentNode.GetY() - 1 >= 0) neighbourList.Add(GetNode(currentNode.GetX()+1, currentNode.GetY()-1));
            //Right Up
            if(currentNode.GetY() + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.GetX()+1, currentNode.GetY()+1));
        }
        //Down
        if(currentNode.GetY() - 1 >= 0) neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY()-1));
        //Up
        if(currentNode.GetY() + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY()+1));

        return neighbourList;
    }

    private TilemapObject GetNode(int x, int y)
    {
        return grid.GetGridObject(x,y);
    }

    private List<TilemapObject> CalculatePath(TilemapObject endNode)
    {
        List<TilemapObject> path = new List<TilemapObject>();
        path.Add(endNode);
        TilemapObject currentNode = endNode;
        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private TilemapObject GetLowestFCostNode(List<TilemapObject> pathNodeList)
    {
        TilemapObject lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private int CalculateDistanceCost(TilemapObject a, TilemapObject b)
    {
        int xDistance = Mathf.Abs(a.GetX() - b.GetX());
        int yDistance = Mathf.Abs(a.GetY() - b.GetY());
        int remaining = Mathf.Abs(xDistance-yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance,yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
    #endregion

    /*
     * Represents a single Tilemap Object that exists in each Grid Cell Position
     * */
    public class TilemapObject : IBuildable {

        public enum TilemapSprite {
            None,
            BuildDefault,
            Barracks,
            // Dirt,
        }

        private Grid<TilemapObject> grid;
        private int x;
        private int y;

        public int gCost;
        public int hCost;
        public int fCost;

        public TilemapObject cameFromNode;

        private TilemapSprite tilemapSprite;
        bool _isBuildable = true;
        public bool isBuildable{
            get { return _isBuildable; }
            set { _isBuildable = value; }
        }
        
        
        public TilemapObject(Grid<TilemapObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        // public override string ToString()
        // {
        //     return x+","+y;
        // }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public void SetTilemapSprite(TilemapSprite tilemapSprite) {
            this.tilemapSprite = tilemapSprite;
            grid.TriggerGridObjectChanged(x, y);
        }

        public TilemapSprite GetTilemapSprite() {
            return tilemapSprite;
        }

        public override string ToString() {
            return tilemapSprite.ToString();
        }
        
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
    }
}
