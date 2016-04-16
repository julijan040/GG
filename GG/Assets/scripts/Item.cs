using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    GameObject cilj;

    public string whatItem;

    gameManager gameManager;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();

        if (whatItem == "wood") cilj = GameObject.Find("wood");
        else if (whatItem == "stone") cilj = GameObject.Find("stone");
        else if (whatItem == "food") cilj = GameObject.Find("food");
    }

    void OnMouseDown()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        if (whatItem == "wood") gameManager.wood++;
        else if (whatItem == "stone") gameManager.rock++;
        else if (whatItem == "food") gameManager.food++;
        gameManager.updateUI();

        StartCoroutine(WaitAndMove());

        cilj.GetComponentsInChildren<Transform>()[1].GetComponent<Animator>().Play("getPoint") ;
    }
    


    IEnumerator WaitAndMove()
    {
        float startTime = Time.time; 
        while (Time.time - startTime <= 3)
        { // until one second passed

            Vector2 pos = Camera.main.ScreenToWorldPoint(cilj.GetComponent<RectTransform>().transform.position);
            

            transform.position = Vector2.Lerp(this.transform.position, pos, (Time.time - startTime) ); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }

        Destroy(this.gameObject);
    }


}
