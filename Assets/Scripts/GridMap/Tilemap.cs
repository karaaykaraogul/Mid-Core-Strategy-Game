using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap {

    public event EventHandler OnLoaded;

    private Grid<TilemapObject> grid;

    public Tilemap(int width, int height, float cellSize, Vector3 originPosition) 
    {
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

    public void GetClickedTilemapInfo(Vector3 worldPosition)
    {
        Debug.Log(grid.GetGridObject(worldPosition));
    }

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



        [System.Serializable]
        public class SaveObject {
            public TilemapSprite tilemapSprite;
            public int x;
            public int y;
        }
    }
}
