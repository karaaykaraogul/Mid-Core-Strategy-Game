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
    [SerializeField] private float cellSize = 1f;

    private void Start() 
    {
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.BuildDefault;
        tilemap = new Tilemap(width, height, cellSize, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
        tilemap.SetDefaultGrid(width, height, tilemapSprite);
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
            }
        }
    }
}
