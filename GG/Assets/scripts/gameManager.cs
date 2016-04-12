using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

    public int wood;
    public Text woodText;
    public int rock;
    public Text rockText;
    public int food;
    public Text foodText;


    public Texture2D cursorTexture;
    CursorMode cursorMode = CursorMode.ForceSoftware;
    Vector2 hotSpot = Vector2.zero;

    void Start ()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
	
	void Update ()
    {
	
	}

    public void updateUI()
    {
        woodText.text = wood.ToString() + " wood";
        rockText.text = rock.ToString() + " rock";
        foodText.text = food.ToString() + " food";
    }
}
