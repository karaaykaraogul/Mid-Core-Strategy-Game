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
        //on start we initialize the tilemap
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.BuildDefault;
        tilemap = new Tilemap(width, height, cellSize, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
        tilemap.SetDefaultGrid(width, height, tilemapSprite);
    }

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
    public void SetGridBuildable(int width, int height, Vector3 buildingWorldPos)
    {
        
        tilemap.GetClickedTilemapObjectNo(buildingWorldPos, out int x, out int y);
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                tilemap.SetTileBuildable((int)(x+i),(int)(y+j));
                tilemap.SetTileWalkable((int)(x+i),(int)(y+j));
            }
        }
    }
}
