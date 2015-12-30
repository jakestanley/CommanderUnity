using UnityEngine;
using System.Collections;

public class Room {

    private int roomId;
    private ArrayList tiles;

    public Room(int roomId) {
        this.roomId = roomId;
        tiles = new ArrayList();
    }

    public void addTile(Tile tile){
        tiles.Add(tile);
    }

    public int getRoomId(){
        return roomId;
    }

    public int getTileCount(){
        return tiles.Count;
    }

    public void highlightRoomTiles(){ // TODO rename to select?
        foreach(Tile tile in tiles){
            TilePartialBehaviour behaviour = tile.tileObject.GetComponent<TilePartialBehaviour>();
            behaviour.select();
        }
    }

}
