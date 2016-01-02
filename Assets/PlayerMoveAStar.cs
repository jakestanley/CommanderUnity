using UnityEngine;
using System.Collections;
using Pathfinding;

public class PlayerMoveAStar : MonoBehaviour {
    public Vector3 targetPosition;
    public void Start () {
        //Get a reference to the Seeker component we added earlier
        Seeker seeker = GetComponent<Seeker>();
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        Path path = seeker.StartPath(transform.position, targetPosition);
        Debug.Log("path: " + path.ToString());

    }

} 