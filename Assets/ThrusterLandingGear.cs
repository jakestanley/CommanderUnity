using UnityEngine;
using System.Collections;

public class ThrusterLandingGear : MonoBehaviour {

    public GameObject gearObject;
    public ParticleSystem leftParticles, rightParticles;
    public int thrusterStatus, gearStatus;
    public float thrusterOffset, doorOffset, strutOffset;

    private Vector3 leftUpperDoorInitialPosition;
    private Vector3 leftLowerDoorInitialPosition;
    private Vector3 leftThrusterInitialPosition;
    private Vector3 leftThrusterEmitterInitialPosition;
    private Vector3 rightUpperDoorInitialPosition;
    private Vector3 rightLowerDoorInitialPosition;
    private Vector3 rightThrusterInitialPosition;
    private Vector3 rightThrusterEmitterInitialPosition;
    private Vector3 landingStrutInitialPosition;
    private Vector3 footRearInitialPosition;
    private Vector3 footFrontInitialPosition;

	// Use this for initialization
	void Start () {
	   thrusterStatus = RETRACTED;
       gearStatus = RETRACTED;

       thrusterOffset = 0;
       doorOffset = 0;
       strutOffset = 0;

       leftUpperDoorInitialPosition = gearObject.transform.Find("LeftUpperDoor").position;
       rightUpperDoorInitialPosition = gearObject.transform.Find("RightUpperDoor").position;
       leftLowerDoorInitialPosition = gearObject.transform.Find("LeftLowerDoor").position;
       rightLowerDoorInitialPosition = gearObject.transform.Find("RightLowerDoor").position;
       leftThrusterInitialPosition = gearObject.transform.Find("LeftThruster").position;
       rightThrusterInitialPosition = gearObject.transform.Find("RightThruster").position;
       leftThrusterEmitterInitialPosition = gearObject.transform.Find("LeftThrusterEmitter").position;
       rightThrusterEmitterInitialPosition = gearObject.transform.Find("RightThrusterEmitter").position;
       landingStrutInitialPosition = gearObject.transform.Find("LandingStrut").position;
       footRearInitialPosition = gearObject.transform.Find("FootRear").position;
       footFrontInitialPosition = gearObject.transform.Find("FootFront").position;
	}
	
	// Update is called once per frame
	void Update () {

        // retro thrusters
        if(Input.GetKeyUp(KeyCode.T)){ // TODO fix so it works on button press, not button hold
            switch(thrusterStatus) {
                case RETRACTED:
                    thrusterStatus = DEPLOYED;
                    break;
                case DEPLOYED:
                    thrusterStatus = RETRACTED;
                    break;
            }
        }

        // landing gear
        if(Input.GetKeyUp(KeyCode.G)){ // TODO fix so it works on button press, not button hold
            switch(gearStatus) {
                case RETRACTED:
                    gearStatus = DEPLOYED;
                    break;
                case DEPLOYED:
                    gearStatus = RETRACTED;
                    break;
            }
        }

        float increaseAmount = Time.deltaTime * GEAR_SPEED;
        float thrusterIncreaseAmount = Time.deltaTime * THRUSTER_SPEED;

        switch(thrusterStatus){
            case RETRACTED:
                thrusterOffset = (thrusterOffset - thrusterIncreaseAmount);
                if(thrusterOffset <= 0){
                    doorOffset = (doorOffset - increaseAmount);
                }
                break;
            case DEPLOYED:
                doorOffset = (doorOffset + increaseAmount);
                if(doorOffset >= THRUSTER_MAX_X_OFFSET){
                    thrusterOffset = (thrusterOffset + thrusterIncreaseAmount);
                }
                break;
        }

        switch(gearStatus){
            case RETRACTED:
                strutOffset = (strutOffset - increaseAmount);
                break;
            case DEPLOYED:
                strutOffset = (strutOffset + increaseAmount);
                break;
        }

        if(thrusterOffset < THRUSTER_MAX_X_OFFSET){
            leftParticles.emissionRate = 0;
            rightParticles.emissionRate = 0;
        } else {
            if (Input.GetKey(KeyCode.Space)){
                leftParticles.emissionRate = BASE_EMISSION_RATE;
                rightParticles.emissionRate = BASE_EMISSION_RATE;
            } else {
                leftParticles.emissionRate = 0;
                rightParticles.emissionRate = 0;
            }
        }

        if(thrusterOffset < 0){
            thrusterOffset = 0;
        } else if(thrusterOffset > THRUSTER_MAX_X_OFFSET){
            thrusterOffset = THRUSTER_MAX_X_OFFSET;
        }

        if(doorOffset < 0){
            doorOffset = 0;
        } else if(doorOffset > DOOR_MAX_Y_OFFSET){
            doorOffset = DOOR_MAX_Y_OFFSET;
        }

        if(strutOffset < 0){
            strutOffset = 0;
        } else if(strutOffset > GEAR_MAX_Y_OFFSET){
            strutOffset = GEAR_MAX_Y_OFFSET;
        }


        updateTransformPositions();
	
	}

    private void updateTransformPositions(){
        // move landing strut/gear
        gearObject.transform.Find("LandingStrut").position = new Vector3(landingStrutInitialPosition.x, landingStrutInitialPosition.y - strutOffset, landingStrutInitialPosition.z);
        gearObject.transform.Find("FootRear").position = new Vector3(footRearInitialPosition.x, footRearInitialPosition.y - strutOffset, footRearInitialPosition.z);
        gearObject.transform.Find("FootFront").position = new Vector3(footFrontInitialPosition.x, footFrontInitialPosition.y - strutOffset, footFrontInitialPosition.z);

        // move thruster doors
        gearObject.transform.Find("LeftUpperDoor").position = new Vector3(leftUpperDoorInitialPosition.x, leftUpperDoorInitialPosition.y + doorOffset, leftUpperDoorInitialPosition.z);
        gearObject.transform.Find("LeftLowerDoor").position = new Vector3(leftLowerDoorInitialPosition.x, leftLowerDoorInitialPosition.y - doorOffset, leftLowerDoorInitialPosition.z);
        gearObject.transform.Find("RightUpperDoor").position = new Vector3(rightUpperDoorInitialPosition.x, rightUpperDoorInitialPosition.y + doorOffset, rightUpperDoorInitialPosition.z);
        gearObject.transform.Find("RightLowerDoor").position = new Vector3(rightLowerDoorInitialPosition.x, rightLowerDoorInitialPosition.y - doorOffset, rightLowerDoorInitialPosition.z);

        // move thrusters
        gearObject.transform.Find("LeftThruster").position = new Vector3(leftThrusterInitialPosition.x, leftThrusterInitialPosition.y, leftThrusterInitialPosition.z - thrusterOffset);
        gearObject.transform.Find("RightThruster").position = new Vector3(rightThrusterInitialPosition.x, rightThrusterInitialPosition.y, rightThrusterInitialPosition.z + thrusterOffset);
        gearObject.transform.Find("LeftThrusterEmitter").position = new Vector3(leftThrusterEmitterInitialPosition.x, leftThrusterEmitterInitialPosition.y, leftThrusterEmitterInitialPosition.z - thrusterOffset);
        gearObject.transform.Find("RightThrusterEmitter").position = new Vector3(rightThrusterEmitterInitialPosition.x, rightThrusterEmitterInitialPosition.y, rightThrusterEmitterInitialPosition.z + thrusterOffset);
    }

    public const float GEAR_SPEED = 2.5f;
    public const float THRUSTER_SPEED = 1f;

    public const float DOOR_MAX_Y_OFFSET = 0.7f;
    public const float GEAR_MAX_Y_OFFSET = 1.6f;
    public const float THRUSTER_MAX_X_OFFSET = 0.5f;

    public const int RETRACTED = 0;
    public const int DEPLOYED = 1;
    public const int BASE_EMISSION_RATE = 18;

}
