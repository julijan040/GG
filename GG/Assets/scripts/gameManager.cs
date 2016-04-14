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
    public Texture2D cursorTextureDown;

    CursorMode cursorMode = CursorMode.ForceSoftware;
    Vector2 hotSpot = Vector2.zero;

    void Start ()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorTextureDown, hotSpot, cursorMode);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

    }

    public void updateUI()
    {
        woodText.text = wood.ToString() + " wood";
        rockText.text = rock.ToString() + " rock";
        foodText.text = food.ToString() + " food";
    }
}
