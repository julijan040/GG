using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public GameObject cilj;

    void OnMouseDown()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(WaitAndMove());

    }
    


    IEnumerator WaitAndMove()
    {
        float startTime = Time.time; 
        while (Time.time - startTime <= 5)
        { // until one second passed

            Vector2 pos = Camera.main.ScreenToWorldPoint(cilj.GetComponent<RectTransform>().transform.position);
            

            transform.position = Vector2.Lerp(this.transform.position, pos, (Time.time - startTime) ); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
    }


}
