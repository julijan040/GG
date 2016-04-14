using UnityEngine;
using System.Collections;

public class buildManager : MonoBehaviour {

    public GameObject selectedItemSprite;
    public int selectedItem = -1;

    public GameObject[] fances;
        
    public void ClickedBuildFence()
    {

        selectedItemSprite.GetComponent<SpriteRenderer>().sprite = fances[0].GetComponent<SpriteRenderer>().sprite;
        selectedItem = 0;
    }


    void Update()
    {
        Vector3 v2 = Input.mousePosition;
        v2.z = 10.0f;

        selectedItemSprite.transform.position = Camera.main.ScreenToWorldPoint(v2);

        if (Input.GetKeyDown("r"))
        {
            selectedItem += 1;
            selectedItemSprite.GetComponent<SpriteRenderer>().sprite = fances[selectedItem].GetComponent<SpriteRenderer>().sprite;
            
        }
            

    }

}
