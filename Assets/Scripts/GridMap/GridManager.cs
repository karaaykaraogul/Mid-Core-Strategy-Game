using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GridManager : MonoBehaviour 
{
    [SerializeField] private TilemapVisual tilemapVisual;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 5f;

    private void Start() 
    {
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.BuildDefault;
        tilemap = new Tilemap(width, height, cellSize, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
        tilemap.SetDefaultGrid(width, height, tilemapSprite);
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            tilemap.SetTilemapSprite(mouseWorldPosition, tilemapSprite);
            tilemap.GetClickedTilemapInfo(mouseWorldPosition);
        }
        
        if (Input.GetKeyDown(KeyCode.T)) {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Barracks;
        }
        // if (Input.GetKeyDown(KeyCode.Y)) {
        //     tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        // }
        // if (Input.GetKeyDown(KeyCode.U)) {
        //     tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        // }
        // if (Input.GetKeyDown(KeyCode.I)) {
        //     tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        // }
    }

}
