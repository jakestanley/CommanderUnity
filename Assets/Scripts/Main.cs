using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public GameObject tile;
    public Tile[,] tiles;

	// Use this for initialization
	void Start () {
        tiles = new Tile[TILES_X,TILES_Z];

        for(int x = 0; x < TILES_X; x++){
            for(int z = 0; z < TILES_Z; z++){
                int northEast = Tile.BLANK;
                int northWest = Tile.BLANK;
                bool isVoid = false;

                if (z == (TILES_Z - 1)) {
                    northWest = Tile.WALL;
                }

                if (x == (TILES_X - 1)) {
                    northEast = Tile.WALL;
                }

                if(x == 3 && z == 3){
                    northWest = Tile.DOOR;
                    northEast = Tile.DOOR;
                }

                GameObject instance = Instantiate(tile) as GameObject;
                Tile newTile = new Tile(instance, isVoid, northEast, northWest);
                newTile.getTileObject().transform.position = new Vector3(x * TILE_WIDTH, 0, z * TILE_WIDTH);

                tiles[x,z] = newTile;
            }
        }

        // testObject = (GameObject) Instantiate(Resources.Load("Assets/DeckTilePartial"));
        // testObject.transform.Translate(new Vector3(3,3,3));
        // for(int x = 0; x < 5; x++){
        //     for(int z = 0; z < 10; z++){
        //         GameObject tile = GameObject.CreatePrimitive(PrimitiveType)
        //     }
        // }
	}
	
	// Update is called once per frame
	void Update () {
	   // testObject.transform.Translate(new Vector3(0.02f,0f,0f));
	}

    private const int TILES_X = 5;
    private const int TILES_Z = 5;
    private const int TILE_WIDTH = 2;

}
