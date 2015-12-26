using UnityEngine;
using System.Collections;

public class TilePartialBehaviour : MonoBehaviour {

	public GameObject tileObject;

    public bool isVoid;
    public int northEastDoorStatus;
    public int northWestDoorStatus;
    public float northEastDoorOpen;
    public float northWestDoorOpen;

    public Vector3 northEastDoorAInitialPosition;
    public Vector3 northEastDoorBInitialPosition;
    public Vector3 northWestDoorAInitialPosition;
    public Vector3 northWestDoorBInitialPosition;

	// Use this for initialization
	void Start () {
        northEastDoorAInitialPosition = tileObject.transform.Find("NorthEastDoorA").position;
        northEastDoorBInitialPosition = tileObject.transform.Find("NorthEastDoorB").position;
        northWestDoorAInitialPosition = tileObject.transform.Find("NorthWestDoorA").position;
        northWestDoorBInitialPosition = tileObject.transform.Find("NorthWestDoorB").position;
    }
	
	// Update is called once per frame
	void Update () {

        // door open and closing code. stick in a separate method and smooth out the animation
        switch(northEastDoorStatus) {
            case OPENING:
                northEastDoorOpen = (northEastDoorOpen + (Time.deltaTime * DOOR_SPEED));
                if(northEastDoorOpen > DOOR_MAX){
                    northEastDoorStatus = OPEN;
                }
                break;
            case CLOSING:
                northEastDoorOpen = (northEastDoorOpen - (Time.deltaTime * DOOR_SPEED));
                if(northEastDoorOpen < 0){
                    northEastDoorStatus = OPEN;
                }
                break;
            default: 

                break;
        }

        switch(northWestDoorStatus) {
            case OPENING:
                northWestDoorOpen = (northWestDoorOpen + (Time.deltaTime * DOOR_SPEED));
                if(northWestDoorOpen > DOOR_MAX){
                    northWestDoorStatus = OPEN;
                }
                break;
            case CLOSING:
                northWestDoorOpen = (northWestDoorOpen - (Time.deltaTime * DOOR_SPEED));
                if(northWestDoorOpen < 0){
                    northWestDoorStatus = OPEN;
                }
                break;
            default: 

                break;
        }

        // bound the door open values
        if(northEastDoorOpen < 0){
            northEastDoorOpen = 0;
        } else if(northEastDoorOpen > DOOR_MAX){
            northEastDoorOpen = DOOR_MAX;
        }

        if(northWestDoorOpen < 0){
            northWestDoorOpen = 0;
        } else if(northWestDoorOpen > DOOR_MAX) {
            northWestDoorOpen = DOOR_MAX;
        }

        // set the new offsets/positions for the doors
        tileObject.transform.Find("NorthWestDoorA").position = new Vector3(northWestDoorAInitialPosition.x-northWestDoorOpen,northWestDoorAInitialPosition.y,northWestDoorAInitialPosition.z);
        tileObject.transform.Find("NorthWestDoorB").position = new Vector3(northWestDoorAInitialPosition.x+northWestDoorOpen,northWestDoorAInitialPosition.y,northWestDoorAInitialPosition.z);
        tileObject.transform.Find("NorthEastDoorA").position = new Vector3(northEastDoorAInitialPosition.x,northEastDoorAInitialPosition.y,northEastDoorAInitialPosition.z+northEastDoorOpen);
        tileObject.transform.Find("NorthEastDoorB").position = new Vector3(northEastDoorBInitialPosition.x,northEastDoorBInitialPosition.y,northEastDoorBInitialPosition.z-northEastDoorOpen);
        
    }

	// initially hide the walls and doors of this tile
	void hide(){

        // TODO flag so you don't need to do extra operations each update

        // // hide pillars at the start, because i'm lazy
        // tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = false;
        // tileObject.transform.Find("EastPillar").GetComponent<Renderer>().enabled = false;
        // tileObject.transform.Find("WestPillar").GetComponent<Renderer>().enabled = false;

        // // north
        // if((int) TileType.WALL == northEastWall){
        //     tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("EastPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthEastDoorA").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthEastDoorB").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthEastWall").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthEastWallDoor").GetComponent<Renderer>().enabled = false;
        // } else if((int) TileType.DOOR == northEastWall){
        //     tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("EastPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthEastDoorA").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthEastDoorB").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthEastWall").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthEastWallDoor").GetComponent<Renderer>().enabled = true;
        // } else {
        //     tileObject.transform.Find("NorthEastDoorA").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthEastDoorB").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthEastWall").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthEastWallDoor").GetComponent<Renderer>().enabled = false;
        // }

        // // west
        // if((int) TileType.WALL == northWestWall){
        //     tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("WestPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthWestDoorA").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthWestDoorB").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthWestWall").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthWestWallDoor").GetComponent<Renderer>().enabled = false;
        // } else if((int) TileType.DOOR == northWestWall){
        //     tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("WestPillar").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthWestDoorA").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthWestDoorB").GetComponent<Renderer>().enabled = true;
        //     tileObject.transform.Find("NorthWestWall").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthWestWallDoor").GetComponent<Renderer>().enabled = true;
        // } else {
        //     tileObject.transform.Find("NorthWestDoorA").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthWestDoorB").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthWestWall").GetComponent<Renderer>().enabled = false;
        //     tileObject.transform.Find("NorthWestWallDoor").GetComponent<Renderer>().enabled = false;
        // }

	}

    public const float DOOR_SPEED = 1.6f;
    public const float DOOR_MAX = 0.4f;

    public const int CLOSED     = 0;
    public const int OPENING    = 1;
    public const int OPEN       = 2;
    public const int CLOSING    = 3;
    public const int STOPPED    = 4;
    public const int BROKEN     = 5;
    public const int BULKHEAD   = 6;

}
