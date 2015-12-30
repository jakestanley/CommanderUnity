using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour { // TODO make this into map?

    public GameObject dude;
    public GameObject tile;
    public Tile[,] tiles;
    public ArrayList rooms;

	// Use this for initialization
	void Start () {
        rooms = new ArrayList();
        loadTileMap(SHIP_PATH_RHOMBUS);
        // generateTileMap();
	}
	
	// Update is called once per frame
	void Update () {
        // dude.transform.Translate(new Vector3(0,-(5f * Time.deltaTime),0));
        // TODO if button is pressed, make the wall?
        // testObject.transform.Translate(new Vector3(0.02f,0f,0f));
	}

    private ArrayList getSelectedTiles(){
        ArrayList selectedTiles = new ArrayList(); 
        for(int x = 0; x < TILES_X; x++){
            for(int z = 0; z < TILES_Z; z++){
                if(tiles[x,z].isSelected()){
                    selectedTiles.Add(tiles[x,z]);
                }
            }
        }
        return selectedTiles;
    }

    private void loadTileMap(string path){ // TODO return a map object?

        // System.IO.StreamReader file = new System.IO.StreamReader(path);
        // while((line = file.ReadLine()) != null) {
        //     Debug.Log(line);
        // }

        // read lines into array
        string[] lines = System.IO.File.ReadAllLines(path);

        // get the dimensions and initialise the map
        string[] parts = lines[1].Split(","[0]);
        int mapX = int.Parse(parts[0]);
        int mapZ = int.Parse(parts[1]);
        tiles = new Tile[mapX,mapZ];

        // populate the tile map
        int i = 2;

        for(int x = 0; x < mapX; x++){
            for(int z = 0; z < mapZ; z++){
                parts = lines[i].Split(","[0]);
                GameObject instance = Instantiate(tile) as GameObject;

                bool isVoid = true;
                if(int.Parse(parts[0]) == 1){ // TODO fix and flip
                    isVoid = false;
                }

                int roomId = int.Parse(parts[1]);

                Room room = null;

                if(roomId >= 0){
                    room = getRoomById(roomId);
                    if(room == null){
                        room = new Room(roomId);
                        rooms.Add(room);
                    }
                }

                int northEastWallType = int.Parse(parts[2]);
                int northWestWallType = int.Parse(parts[3]);

                Tile newTile = new Tile(instance, x, z, isVoid, northEastWallType, northWestWallType);
                newTile.getTileObject().transform.position = new Vector3(x * TILE_WIDTH, 0, z * TILE_WIDTH);
                tiles[x,z] = newTile;
                if(room != null){
                    room.addTile(newTile);
                }
                i++;
            }
        }
        printRoomData();
    }

    // TODO move this shit into a separate map class

    private Room getRoomById(int id){
        foreach(Room room in rooms){
            if(room.getRoomId() == id){
                return room;
            }
        }
        return null;
    }

    private void printRoomData(){
        
        Debug.Log("Printing room data");
        foreach(Room room in rooms){
            Debug.Log("Room " + room.getRoomId() + " contains " + room.getTileCount() + " tiles");
        }

        // getRoomById(0).highlightRoomTiles();
        // getRoomById(1).highlightRoomTiles();
        // getRoomById(2).highlightRoomTiles();
        // getRoomById(3).highlightRoomTiles();
        // getRoomById(4).highlightRoomTiles();
        // getRoomById(5).highlightRoomTiles();
        // getRoomById(6).highlightRoomTiles();

    }

    private void generateTileMap(){ // fake testing room hardcoded for now until i create a file parser
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
                    northWest = Tile.WALL;
                    northEast = Tile.WALL;
                }

                if(x == 2 && z == 3){
                    northWest = Tile.WALL;
                }

                if(x == 3 && z == 2){
                    northEast = Tile.WALL;
                }

                if(x == 1 && z == 3){
                    northEast = Tile.WALL;
                }

                if(x == 1 && z == 2){
                    northEast = Tile.WALL;
                }

                if(x == 2 && z == 1){
                    northWest = Tile.DOOR;
                }

                if(x == 3 && z == 1){
                    northWest = Tile.WALL;
                }                

                GameObject instance = Instantiate(tile) as GameObject;
                Tile newTile = new Tile(instance, x, z, isVoid, northEast, northWest);
                newTile.getTileObject().transform.position = new Vector3(x * TILE_WIDTH, 0, z * TILE_WIDTH);

                tiles[x,z] = newTile;
            }
        }
    }

    private const int TILES_X = 5;
    private const int TILES_Z = 5;
    private const float TILE_WIDTH = 2.5f;

    private const string SHIP_PATH_RHOMBUS = "./Assets/Ships/Rhombus.csv";

}
