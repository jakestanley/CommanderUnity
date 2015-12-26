using UnityEngine;
using System.Collections;

public class TileBehaviour : MonoBehaviour { // TODO remove

	public GameObject tileObject;

    public int northEastWall;
    public int southEastWall;
    public int southWestWall;
    public int northWestWall;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	// initially hide the walls and doors of this tile
	void hide(){

	}

    public const int BLANK = 0;
    public const int WALL = 1;
    public const int DOOR = 2;

}
