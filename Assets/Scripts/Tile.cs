using UnityEngine;
using System.Collections;

public class Tile { // TODO rename to TileController?

    public GameObject tileObject;
    public TilePartialBehaviour behaviour;
    public bool isVoid;
    public int northEastWall;
    public int northWestWall; // TODO make private...
    public int x, y;
    public int roomId; // TODO roomId

    public Tile(GameObject tileObject, int x, int y, bool isVoid, int northEastWall, int northWestWall) { // TODO select coordinates
        this.tileObject = tileObject;
        this.x = x;
        this.y = y;
        this.isVoid = isVoid;
        this.northEastWall = northEastWall;
        this.northWestWall = northWestWall;
        
        behaviour = tileObject.GetComponent<TilePartialBehaviour>();
        behaviour.isVoid = isVoid; // TODO make unshit
        behaviour.northEastWall = northEastWall;
        behaviour.northWestWall = northWestWall;
        behaviour.northEastDoorStatus = TilePartialBehaviour.CLOSED;
        behaviour.northWestDoorStatus = TilePartialBehaviour.CLOSED;
        behaviour.northEastDoorOpen = 0;
        behaviour.northWestDoorOpen = 0;

        behaviour.selected = false;
    }

    public GameObject getTileObject() {
        return tileObject;
    }

    

    public bool isSelected(){
        return behaviour.selected;
    }

    public void openNorthEastDoor() {

    }

    public void openNorthWestDoor() {

    }

    public const int BLANK = 0;
    public const int WALL = 1;
    public const int DOOR = 2;

}
