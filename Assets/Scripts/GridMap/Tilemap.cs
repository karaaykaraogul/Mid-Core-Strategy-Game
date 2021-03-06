using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Tilemap{

    public event EventHandler OnLoaded;

    private Grid<TilemapObject> grid;
    public static Tilemap Instance {get; private set;}

    private List<TilemapObject> openList;
    private List<TilemapObject> closedList;
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Tilemap(int width, int height, float cellSize, Vector3 originPosition) 
    {
        Instance = this;
        //initialize the grid with the given parametres from grid manager and with the tilemapobject type
        grid = new Grid<TilemapObject>(width, height, cellSize, originPosition, (Grid<TilemapObject> g, int x, int y) => new TilemapObject(g, x, y));
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
    public void SetTileNotWalkable(int x, int y)
    {
        var tile = grid.GetGridObject(x,y);
        tile.isWalkable = false;
    }
    public void SetTileBuildable(int x, int y)
    {
        var tile = grid.GetGridObject(x,y);
        tile.isBuildable = true;
    }
    public void SetTileWalkable(int x, int y)
    {
        var tile = grid.GetGridObject(x,y);
        tile.isWalkable = true;
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

    private TilemapObject GetNode(int x, int y)
    {
        return grid.GetGridObject(x,y);
    }

    //pathfinding is intertwined with tilemap and tilemapobjects
    //therefore A* pathfinding algorithms are listed below in pathfinding region
    #region Pathfinding
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition) {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<TilemapObject> path = FindPath(startX, startY, endX, endY);
        if (path == null) {
            return null;
        } else {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (TilemapObject pathNode in path) {
                vectorPath.Add(new Vector3(pathNode.GetX(), pathNode.GetY()) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }

    public List<TilemapObject> FindPath(int startX, int startY, int endX, int endY)
    {
        TilemapObject startNode = grid.GetGridObject(startX, startY);
        TilemapObject endNode = grid.GetGridObject(endX, endY);
        
        if (startNode == null || endNode == null) {
            // Invalid Path
            return null;
        }

        
        var neighbourList = GetNeighbourList(endNode);
        int i = 0;
        while(endNode.isOccupiedByUnit || !endNode.isWalkable)
        {
            foreach(var neighbour in neighbourList)
            {
                if(!neighbour.isOccupiedByUnit || !endNode.isWalkable)
                {
                    endNode = neighbour;
                    break;
                }
            }
            if(endNode.isOccupiedByUnit || !endNode.isWalkable && i <= (neighbourList.Count-1))
            {
                endNode = neighbourList[i];
                i++;
            }
            if(i >= (neighbourList.Count-1))
            {
                neighbourList = GetNeighbourList(neighbourList[i]);
                i = 0;
            }
        }

        startNode.isOccupiedByUnit = false;

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
        //search until there is no nodes in openlist
        while(openList.Count > 0)
        {
            TilemapObject currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                //calculate path is called when you reach the end node
                currentNode.isOccupiedByUnit = true;
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(TilemapObject neighbourNode in GetNeighbourList(currentNode))
            {
                if(closedList.Contains(neighbourNode)) continue;
                //automatically add unwalkable nodes to closed list
                if(!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }
                //temp G cost is assigned to neighbour nodes
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                //if the temp g is lower than assigned g cost assigns the new g cost
                if(tentativeGCost < neighbourNode.gCost)
                {
                    //assigns the previous node to next node so we can reverse track while calculating path
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

    //gets the neighbours of a given current node
    //with current node in the middle and 3x3 square neighbours around the current node
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

    
    //Represents a single Tilemap Object that exists in each Grid Cell Position
    public class TilemapObject : IBuildable, IWalkable {

        public enum TilemapSprite {
            None,
            BuildDefault
        }

        private Grid<TilemapObject> grid;
        private int x;
        private int y;
        //every object has these three variables in order to use a* pathfinding
        //G = walking cost from the start node
        public int gCost;
        //H = heuristic cost to reach end node this, estimated calculation is done without adding walls or any blocking objects
        public int hCost;
        //F = G + H
        public int fCost;
        
        public TilemapObject cameFromNode;

        private TilemapSprite tilemapSprite;
        bool _isBuildable = true;
        bool _isWalkable;
        bool _isOccupiedByUnit;
        public bool isBuildable{
            get { return _isBuildable; }
            set { _isBuildable = value; }
        }

        public bool isWalkable {
            get { return _isWalkable; }
            set { _isWalkable = value; }
        }

        public bool isOccupiedByUnit {
            get { return _isOccupiedByUnit; }
            set { _isOccupiedByUnit = value; }
        }


        public TilemapObject(Grid<TilemapObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            _isWalkable = true;
            _isOccupiedByUnit = false;
        }

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

        public Vector3 GetTileWorldPositions()
        {
            Debug.Log("x: " + x + "y: " + y + "world Pos: " + grid.GetWorldPosition(GetX(),GetY()));
            return grid.GetWorldPosition(GetX(),GetY());
        }

        public Vector3 GetXY()
        {
            return new Vector3(x,y,0);
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
