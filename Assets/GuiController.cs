using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {

    public int CURRENT_MODE = 0;
    public Font font;
    private GUIStyle boxStyle, buttonStyle;
    private int width, height;
    private int activeModal;

    // shapes
    private Rect title;
    private Rect buttonBar;
    private Rect crewBox;
    private Rect functionalBox;
    private Rect[] buttons;

	// Use this for initialization
	void Start () {
        activeModal = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Render GUI
    void OnGUI(){

        // set resolution variables
        width = Screen.width;
        height = Screen.height;

        // set boxStyle
        boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.font = font;

        // set buttonStyle
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.font = font;
        
        buildTitleBar();
        buildButtonBar();

        switch(activeModal){
            case CREW_MODAL:
                buildCrewBox();
                break;
            case FUNCTIONAL_MODAL:
                buildFunctionalBox(); // TODO rename to *Modal
                break;
            default:
                
                break;

        }

    }

    private void buildTitleBar(){
        title = new Rect(HORIZONTAL_MARGIN, VERTICAL_MARGIN, width - HORIZONTAL_MARGIN*2, VERTICAL_MARGIN + SINGLE_LINE_HEIGHT);
        GUI.Box(title, "Commander", boxStyle);
    }

    private void buildButtonBar(){
        buttonBar = new Rect(HORIZONTAL_MARGIN, title.yMax + VERTICAL_MARGIN,width - (HORIZONTAL_MARGIN*2),(VERTICAL_MARGIN*2) + BUTTON_HEIGHT);
        GUI.Box(buttonBar, "", boxStyle);

        // TODO button width

        float horizontalSpacing = (buttonBar.width - (2 * HORIZONTAL_MARGIN) - (BUTTON_COUNT * BUTTON_WIDTH)) / (BUTTON_COUNT - 1);

        buttons = new Rect[BUTTON_COUNT];
        for(int i = 0; i < BUTTON_COUNT; i++){
            buttons[i] = new Rect(buttonBar.x + HORIZONTAL_MARGIN + (i * (BUTTON_WIDTH + horizontalSpacing)), buttonBar.y + VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT);
        }

        // should be conditional

        if(GUI.Button(buttons[0], "Map", buttonStyle)){
            activeModal = 0;
        }
        if(GUI.Button(buttons[1], "Cargo", buttonStyle)){
            activeModal = 0;
        }
        if(GUI.Button(buttons[2], "Crew", buttonStyle)){
            activeModal = CREW_MODAL;
        }
        if(GUI.Button(buttons[3], "Place", buttonStyle)){
            activeModal = FUNCTIONAL_MODAL;
        }
        if(GUI.Button(buttons[4], "Button", buttonStyle)){
            activeModal = 0;
        }


    }

    private void buildCrewBox(){
        crewBox = new Rect(HORIZONTAL_MARGIN, buttonBar.yMax + VERTICAL_MARGIN, 600, 500); // TODO or limit at screen height - vertical margin
        GUI.Box(crewBox, "Crew info will display here", boxStyle);
    }

    private void buildFunctionalBox(){
        functionalBox = new Rect(HORIZONTAL_MARGIN, buttonBar.yMax + VERTICAL_MARGIN, 500, 800);
        GUI.Box(functionalBox, "Placeable objects will be listed here, with selectable categories", boxStyle);
    }

    private const int VERTICAL_MARGIN   = 10;
    private const int HORIZONTAL_MARGIN = 10;
    private const int SINGLE_LINE_HEIGHT = 12;
    private const int BUTTON_WIDTH = 200;
    private const int BUTTON_HEIGHT = 24;

    // BUTTON BAR
    private const int BUTTON_COUNT = 5;

    // MODALS
    private const int CREW_MODAL = 1;
    private const int FUNCTIONAL_MODAL = 2;


}
