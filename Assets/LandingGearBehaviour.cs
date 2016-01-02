using UnityEngine;
using System.Collections;

public class LandingGearBehaviour : MonoBehaviour {

    public GameObject gearObject;
    public int gearStatus;
    public float footOffset, lowerLegOffset, middleLegOffset;

    public Vector3 footInitialPosition;
    public Vector3 lowerLegInitialPosition;
    public Vector3 middleLegInitialPosition;
    public Vector3 upperLegInitialPosition;

	// Use this for initialization
	void Start () {
        gearStatus = DEPLOYED;
        footOffset = 0;
        lowerLegOffset = 0;
        middleLegOffset = 0;
        footInitialPosition = gearObject.transform.Find("Foot").position;
        lowerLegInitialPosition = gearObject.transform.Find("LowerLeg").position;
        middleLegInitialPosition = gearObject.transform.Find("MiddleLeg").position;
        upperLegInitialPosition = gearObject.transform.Find("UpperLeg").position;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKey(KeyCode.G)){ // TODO fix so it works on button press, not button hold
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

        switch(gearStatus) {
            case RETRACTED:
                footOffset = (footOffset + increaseAmount);
                lowerLegOffset = (lowerLegOffset + increaseAmount);
                middleLegOffset = (middleLegOffset + increaseAmount);
                break;
            case DEPLOYED:
                footOffset = (footOffset - increaseAmount);
                lowerLegOffset = (lowerLegOffset - increaseAmount);
                middleLegOffset = (middleLegOffset - increaseAmount);
                break;
        }

        if(footOffset < 0){
            footOffset = 0;
        } else if(footOffset > FOOT_RETRACTED_MAX_Y_OFFSET){
            footOffset = FOOT_RETRACTED_MAX_Y_OFFSET;
        }

        if(lowerLegOffset < 0){
            lowerLegOffset = 0;
        } else if(lowerLegOffset > LOWER_LEG_RETRACTED_MAX_Y_OFFSET){
            lowerLegOffset = LOWER_LEG_RETRACTED_MAX_Y_OFFSET;
        }

        if(middleLegOffset < 0){
            middleLegOffset = 0;
        } else if(middleLegOffset > MIDDLE_LEG_RETRACTED_MAX_Y_OFFSET){
            middleLegOffset = MIDDLE_LEG_RETRACTED_MAX_Y_OFFSET;
        }        

        updateTransformPositions();

	}

    private void updateTransformPositions(){ // (y - GEAR_MAX - deployValue)
        gearObject.transform.Find("Foot").position      = new Vector3(footInitialPosition.x, footInitialPosition.y + footOffset, footInitialPosition.z);
        gearObject.transform.Find("LowerLeg").position  = new Vector3(lowerLegInitialPosition.x, lowerLegInitialPosition.y + lowerLegOffset, lowerLegInitialPosition.z);
        gearObject.transform.Find("MiddleLeg").position = new Vector3(middleLegInitialPosition.x, middleLegInitialPosition.y + middleLegOffset, middleLegInitialPosition.z);
        // gearObject.transform.Find("UpperLeg").position = new Vector3();
    }

    public const float GEAR_SPEED = 4f;

    public const float MIDDLE_LEG_RETRACTED_MAX_Y_OFFSET  = 3.2f;
    public const float LOWER_LEG_RETRACTED_MAX_Y_OFFSET   = 6f;
    public const float FOOT_RETRACTED_MAX_Y_OFFSET        = 6f;

    public const int RETRACTED = 0;
    public const int DEPLOYED = 1;

}
