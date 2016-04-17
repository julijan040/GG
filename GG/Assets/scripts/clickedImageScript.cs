using UnityEngine;
using System.Collections;

public class clickedImageScript : MonoBehaviour {
    
	void Update ()
    {
        Vector2 pos = Input.mousePosition;
        pos.x += 35f;
        pos.y += 15f;
        transform.position = pos;
    }
}
