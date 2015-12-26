using UnityEngine;
using System.Collections;

public class Tile { // TODO rename to TileController?

    public GameObject tileObject;
    public TilePartialBehaviour behaviour;
    public bool isVoid;
    public int northEastWall;
    public int northWestWall;

    public Tile(GameObject tileObject, bool isVoid, int northEastWall, int northWestWall) {
        this.tileObject = tileObject;
        this.isVoid = isVoid;
        this.northEastWall = northEastWall;
        this.northWestWall = northWestWall;
        
        behaviour = tileObject.GetComponent<TilePartialBehaviour>();
        behaviour.northEastDoorStatus = TilePartialBehaviour.CLOSED;
        behaviour.northWestDoorStatus = TilePartialBehaviour.CLOSED;
        behaviour.northEastDoorOpen = 0;
        behaviour.northWestDoorOpen = 0;

        updateVisibility();
    }

    public GameObject getTileObject() {
        return tileObject;
    }

    public void setNorthWestWallType(int northWestWallType) {
        this.northWestWall = northWestWallType;
        updateVisibility();
    }

    public void setNorthEastWallType(int northEastWallType) {
        this.northEastWall = northEastWallType;
        updateVisibility();
    }

    public void updateVisibility()
    {

        if (isVoid) {
            // TODO hide all
            return;
        }

        // hide pillars initially
        tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = false;
        tileObject.transform.Find("EastPillar").GetComponent<Renderer>().enabled = false;
        tileObject.transform.Find("WestPillar").GetComponent<Renderer>().enabled = false;

        // northeast wall
        switch (northEastWall) {
            case BLANK:
                tileObject.transform.Find("NorthEastWall").GetComponent<Renderer>().enabled = false; // TODO make references to these variables to reduce code?
                tileObject.transform.Find("NorthEastWallDoor").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthEastDoorA").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthEastDoorB").GetComponent<Renderer>().enabled = false;
                break;
            case WALL:
                tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("EastPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthEastWall").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthEastWallDoor").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthEastDoorA").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthEastDoorB").GetComponent<Renderer>().enabled = false;
                break;
            case DOOR:
                tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("EastPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthEastWall").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthEastWallDoor").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthEastDoorA").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthEastDoorB").GetComponent<Renderer>().enabled = true;
                break;
        }

        // northwest wall
        switch (northWestWall) {
            case BLANK:
                tileObject.transform.Find("NorthWestWall").GetComponent<Renderer>().enabled = false; // TODO make references to these variables to reduce code?
                tileObject.transform.Find("NorthWestWallDoor").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthWestDoorA").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthWestDoorB").GetComponent<Renderer>().enabled = false;
                break;
            case WALL:
                tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("WestPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthWestWall").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthWestWallDoor").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthWestDoorA").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthWestDoorB").GetComponent<Renderer>().enabled = false;
                break;
            case DOOR:
                tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("WestPillar").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthWestWall").GetComponent<Renderer>().enabled = false;
                tileObject.transform.Find("NorthWestWallDoor").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthWestDoorA").GetComponent<Renderer>().enabled = true;
                tileObject.transform.Find("NorthWestDoorB").GetComponent<Renderer>().enabled = true;
                break;
        }

    }

    public void openNorthEastDoor() {

    }

    public void openNorthWestDoor() {

    }

    public const int BLANK = 0;
    public const int WALL = 1;
    public const int DOOR = 2;



}
