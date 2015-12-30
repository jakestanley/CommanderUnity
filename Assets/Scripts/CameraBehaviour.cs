using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public GameObject camera;
    public float speed;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera"); // TODO use tags
        speed = BASE_SPEED;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.W)){
            camera.transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
        }
        if(Input.GetKey(KeyCode.S)){
            camera.transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
        }
        if(Input.GetKey(KeyCode.A)){
            camera.transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
        }
	    if(Input.GetKey(KeyCode.D)){
			camera.transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        }
	}

    public const float BASE_SPEED = 6;

}
