using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] private TilemapVisual tilemapVisual;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    [SerializeField] private int width = 100;
    [SerializeField] private int height = 100;
    [SerializeField] private float cellSize = 0.33f;

    private void Start() 
    {
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.BuildDefault;
        tilemap = new Tilemap(width, height, cellSize, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
        tilemap.SetDefaultGrid(width, height, tilemapSprite);
    }

    // private void Update()
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
    //         tilemap.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
    //         // Debug.Log(x + "," + y);
    //         List<Tilemap.TilemapObject> path = tilemap.FindPath(0,0,x,y);
    //         if(path != null)
    //         {
    //             for(int i = 0; i <path.Count - 1; i++)
    //             {
    //                 Debug.DrawLine(new Vector3(path[i].GetX(),path[i].GetY()) * cellSize + Vector3.one * cellSize/2, new Vector3(path[i+1].GetX(),path[i+1].GetY()) * cellSize + Vector3.one * cellSize/2, Color.red , 5f);
    //             }
    //         }
    //         else
    //         {
    //             Debug.Log("null aga");
    //         }
    //     }
    // }

    

    public Tilemap.TilemapObject GetBuildingSpawnPoint(Vector3 buildingPos)
    {
        tilemap.GetClickedTilemapObjectNo(buildingPos, out int x, out int y);
        return tilemap.GetGrid().GetGridObject(x,(y-1));
    }

    public Vector3 GetClickedGridPositions()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        return tilemap.GetClickedTilemapPositions(mouseWorldPosition);
    }

    public bool GetGridAvailability(int width, int height)
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        tilemap.GetClickedTilemapObjectNo(mouseWorldPosition, out int x, out int y);
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(!tilemap.GetClickedTilemapBuildAvailability((int)(x+i),(int)(y+j)))
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void SetGridBuilt(int width, int height)
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        tilemap.GetClickedTilemapObjectNo(mouseWorldPosition, out int x, out int y);
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                tilemap.SetTileNotBuildable((int)(x+i),(int)(y+j));
                tilemap.SetTileNotWalkable((int)(x+i),(int)(y+j));
            }
        }
    }
}
