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
    Vector2 hotSpot = new Vector2(14f,0f);

    public buildManager buildManager;

    public GameObject pixelDirt;
    public GameObject waterDirt;
    public GameObject bloodDirt;

    public GameObject clickedImage;

    public bool axe;

    

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

        if (Input.GetMouseButtonDown(1))
        {

            if (buildManager.selectedItem != -1)
            {
                buildManager.build();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

            createDirtEffect();

            
        }

    }

    void createDirtEffect()
    {

        //Converting Mouse Pos to 2D (vector2) World Pos
        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        if (!hit)
        {
            Vector3 v2 = Input.mousePosition;
            v2.z = 1f;

            v2 = Camera.main.ScreenToWorldPoint(v2);



            GameObject instance = (GameObject)Instantiate(pixelDirt, v2, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 15f));

            StartCoroutine(DisableRigidbody(instance));
            StartCoroutine(DestroyInstance(instance));
        }

        /*
        else if(hit.transform.gameObject.name == "voda")
        {
            Vector3 v2 = Input.mousePosition;
            v2.z = 1f;

            v2 = Camera.main.ScreenToWorldPoint(v2);

            GameObject instance = (GameObject)Instantiate(waterDirt, v2, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 15f));

            StartCoroutine(DisableRigidbody(instance));
            StartCoroutine(DestroyInstance(instance));
        }
        else if (hit.transform.gameObject.tag == "animal")
        {
            Vector3 v2 = Input.mousePosition;
            v2.z = 1f;

            v2 = Camera.main.ScreenToWorldPoint(v2);

            GameObject instance = (GameObject)Instantiate(bloodDirt, v2, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 15f));

            StartCoroutine(DisableRigidbody(instance));
            StartCoroutine(DestroyInstance(instance));
        }
        */




    }

    IEnumerator DestroyInstance(GameObject instance)
    {
        yield return new WaitForSeconds(1f);

        instance.GetComponent<Animator>().Play("shrinkAnim");

        yield return new WaitForSeconds(1f);

        Destroy(instance);
    }

    IEnumerator DisableRigidbody(GameObject instance)
    {
        yield return new WaitForSeconds(0.65f);
        instance.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void updateUI()
    {
        woodText.text = wood.ToString();
        rockText.text = rock.ToString();
        foodText.text = food.ToString();
    }

    public void updateClick(float hp, float maxHp)
    {
        clickedImage.GetComponent<Image>().fillAmount = hp / maxHp;
    }
}
