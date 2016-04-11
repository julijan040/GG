using UnityEngine;
using System.Collections;

public class cameraScroll : MonoBehaviour {
    
	void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !(Camera.main.orthographicSize < 0.25f))
        {
            Camera.main.orthographicSize-=0.1f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && !(Camera.main.orthographicSize > 1f))
        {
            Camera.main.orthographicSize+=0.1f;
        }

    }
}
