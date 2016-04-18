using UnityEngine;
using System.Collections;

public class buildManager : MonoBehaviour {

    public GameObject selectedItemSprite;
    public int selectedItem = -1;
    public string nameOfSelected;

    public GameObject[] fances;

    public GameObject grid;

    public GameObject axe;
    public GameObject pickaxe;

    public void ClickedBuildFence()
    {
        grid.SetActive(true);
        selectedItemSprite.GetComponent<SpriteRenderer>().sprite = fances[0].GetComponent<SpriteRenderer>().sprite;
        selectedItem = 0;
        nameOfSelected = "fances";
    }


    void Update()
    {
        Vector3 v2 = Input.mousePosition;
        v2.z = 1f;

        selectedItemSprite.transform.position = Camera.main.ScreenToWorldPoint(v2);

        if (Input.GetKeyDown("r"))
        {
            if (selectedItem + 1 != fances.Length)
            {
                selectedItem += 1;
                if(nameOfSelected == "fances") selectedItemSprite.GetComponent<SpriteRenderer>().sprite = fances[selectedItem].GetComponent<SpriteRenderer>().sprite;
            } 
            else
            {
                selectedItem = 0;
                selectedItemSprite.GetComponent<SpriteRenderer>().sprite = fances[selectedItem].GetComponent<SpriteRenderer>().sprite;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedItem = -1;
            nameOfSelected = "";
            selectedItemSprite.GetComponent<SpriteRenderer>().sprite = null;
            grid.SetActive(false);
        }


    }

    public void build()
    {
        if (nameOfSelected == "fances" && GameObject.Find("GameManager").GetComponent<gameManager>().wood >= 3)
        {
            GameObject.Find("GameManager").GetComponent<gameManager>().wood -= 3;
            GameObject.Find("GameManager").GetComponent<gameManager>().updateUI();


            Vector3 v2 = Input.mousePosition;
            v2.z = 1f;

            v2 = Camera.main.ScreenToWorldPoint(v2);


            float snapInverse = 1 / 0.32f;

            v2.x = Mathf.Round(v2.x * snapInverse) / snapInverse;
            v2.y = Mathf.Round(v2.y * snapInverse) / snapInverse;

            if (Physics2D.OverlapCircle(v2, 0.01f) == null)
            {
                if (nameOfSelected == "fances") Instantiate(fances[selectedItem], v2, Quaternion.identity);
            }
        }     
        
    }

    public void BuildExe()
    {
        if (GameObject.Find("GameManager").GetComponent<gameManager>().wood >= 6 && GameObject.Find("GameManager").GetComponent<gameManager>().rock >= 6)
        {
            GameObject.Find("GameManager").GetComponent<gameManager>().wood -= 6;
            GameObject.Find("GameManager").GetComponent<gameManager>().rock -= 6;

            GameObject.Find("GameManager").GetComponent<gameManager>().axe = true;
            axe.SetActive(true);
            GameObject.Find("GameManager").GetComponent<gameManager>().updateUI();
        }
    }

    public void BuildPickAxe()
    {
        if (GameObject.Find("GameManager").GetComponent<gameManager>().wood >= 6 && GameObject.Find("GameManager").GetComponent<gameManager>().rock >= 6)
        {
            GameObject.Find("GameManager").GetComponent<gameManager>().wood -= 6;
            GameObject.Find("GameManager").GetComponent<gameManager>().rock -= 6;

            GameObject.Find("GameManager").GetComponent<gameManager>().pickaxe = true;
            pickaxe.SetActive(true);
            GameObject.Find("GameManager").GetComponent<gameManager>().updateUI();
        }
    }

}
