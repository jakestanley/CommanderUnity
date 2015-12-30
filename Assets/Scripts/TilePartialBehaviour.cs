using UnityEngine;
using System.Collections;

public class TilePartialBehaviour : MonoBehaviour {

	public GameObject tileObject;
    public BoxCollider collider;
    public Camera camera;
    public Material baseMaterial;
    public Material selectedMaterial;
// TODO make an offset of north and easy doors. if a place is within a certain distance, open the door, as long as other requirements are satisfied, e.g which room they're currently in, etc
    public bool isVoid, selected;
    public int northEastDoorStatus;
    public int northWestDoorStatus;
    public int northEastWall;
    public int northWestWall;
    public float northEastDoorOpen;
    public float northWestDoorOpen;
    public int x, y;

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

        // get the base material
        baseMaterial = tileObject.transform.Find("Base").GetComponent<Renderer>().material;
        collider = tileObject.transform.Find("Base").GetComponent<BoxCollider>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        selected = false;
    }
    
    // Update is called once per frame
    void Update () {

        if(isMobileNearNorthEast()){
            northEastDoorStatus = OPENING;
        } else {
            northEastDoorStatus = CLOSING;
        }

        if(isMobileNearNorthWest()){
            northWestDoorStatus = OPENING;
        } else {
            northWestDoorStatus = CLOSING;
        }

        if(!isVoid){

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

            if(clicked()){
                if(selected){
                    deselect();
                } else {
                    select();
                }
            }

        }

        updateVisibility();

    }

    bool clicked(){
        if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                hit.collider.gameObject.transform.position.Set(hit.collider.gameObject.transform.position.x, (float) (hit.collider.gameObject.transform.position.y - 0.5), hit.collider.gameObject.transform.position.z);
                if (hit.collider.bounds.Contains(collider.transform.position)) {
                    Debug.Log ("Clicked object position = " + hit.collider.gameObject.transform.position);
                    return true;
                } else {
                    return false; // TODO clean this up
                }
            } else {
                return false;
            }
         }
         return false;
    }

    public bool isSelected() {
        return selected;
    }

    public void select(){
        tileObject.transform.Find("Base").GetComponent<Renderer>().material = selectedMaterial;
        selected = true;
    }

    public void deselect(){
        tileObject.transform.Find("Base").GetComponent<Renderer>().material = baseMaterial;
        selected = false;
    }

    public void setTileCoordinates(int x, int y){
        this.x = x;
        this.y = y;
    }

    public bool isMobileNearNorthEast(){
        GameObject dude = GameObject.Find("Dude");
        Vector3 doorPosition = tileObject.transform.position;
        doorPosition.x = doorPosition.x + northEastDoorOffsetX;
        float distance = Vector3.Distance(dude.transform.position, doorPosition);
        return (distance < DOOR_PROXIMITY_MAX_DISTANCE);
    }

    public bool isMobileNearNorthWest(){
        GameObject dude = GameObject.Find("Dude");
        Vector3 doorPosition = tileObject.transform.position;
        doorPosition.z = doorPosition.z + northWestDoorOffsetZ;
        float distance = Vector3.Distance(dude.transform.position, doorPosition);
        return (distance < DOOR_PROXIMITY_MAX_DISTANCE);
    }

    public void updateVisibility() {

        // hide pillars initially
        tileObject.transform.Find("NorthPillar").GetComponent<Renderer>().enabled = false;
        tileObject.transform.Find("EastPillar").GetComponent<Renderer>().enabled = false;
        tileObject.transform.Find("WestPillar").GetComponent<Renderer>().enabled = false;
        tileObject.transform.Find("SouthPillar").GetComponent<Renderer>().enabled = false;
        
        // disable initial collision detection    
        tileObject.transform.Find("NorthEastWall").GetComponent<Collider>().enabled = false;
        tileObject.transform.Find("NorthWestWall").GetComponent<Collider>().enabled = false;

        if (isVoid) {
            tileObject.transform.Find("NorthEastWall").GetComponent<Renderer>().enabled = false; // TODO make references to these variables to reduce code?

            tileObject.transform.Find("NorthEastWallDoor").GetComponent<Renderer>().enabled = false;
            tileObject.transform.Find("NorthEastDoorA").GetComponent<Renderer>().enabled = false;
            tileObject.transform.Find("NorthEastDoorB").GetComponent<Renderer>().enabled = false;
            tileObject.transform.Find("NorthWestWall").GetComponent<Renderer>().enabled = false; // TODO make references to these variables to reduce code?
            tileObject.transform.Find("NorthWestWallDoor").GetComponent<Renderer>().enabled = false;
            tileObject.transform.Find("NorthWestDoorA").GetComponent<Renderer>().enabled = false;
            tileObject.transform.Find("NorthWestDoorB").GetComponent<Renderer>().enabled = false;
            tileObject.transform.Find("Base").GetComponent<Renderer>().enabled = false;
            return;
        } else {
            tileObject.transform.Find("Base").GetComponent<Renderer>().enabled = true;
        }

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

    public const float northEastDoorOffsetX = 1.25f;
    public const float northWestDoorOffsetZ = 1f; // TODO capitalise these

    public const float DOOR_SPEED = 1.6f;
    public const float DOOR_MAX = 0.4f;
    public const float DOOR_PROXIMITY_MAX_DISTANCE = 1f;

    public const float SELECTION_EMISSION = 0.6f;

    public const int CLOSED     = 0;
    public const int OPENING    = 1;
    public const int OPEN       = 2;
    public const int CLOSING    = 3;
    public const int STOPPED    = 4;
    public const int BROKEN     = 5;
    public const int BULKHEAD   = 6;

    public const int BLANK = 0;
    public const int WALL = 1;
    public const int DOOR = 2;

}
