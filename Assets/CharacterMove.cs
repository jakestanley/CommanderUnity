using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 goal;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        // agent.updateRotation = false;
        // goal = new Vector3(13,0,24);
    }
    
    // Update is called once per frame
    void Update () {
        agent.SetDestination(goal);
	}
}
