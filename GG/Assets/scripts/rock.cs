using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

    public gameManager gameManager;
    public float hp;
    public Animator anim;

    public GameObject rockDirt;

    public bool killed;

    public GameObject Item;

    float maxHp;

    void Start()
    {
        anim = GetComponent<Animator>();
        maxHp = hp; 
    }


    void OnMouseDrag()
    {


        if (!killed && gameManager.pickaxe)
        {
            doEffect();
            gameManager.updateClick(hp, maxHp);
            anim.Play("treeAnim");
            if (hp > 0) hp -= Time.deltaTime;
            else Dead();
        }



    }

    void OnMouseDown()
    {
        if(gameManager.pickaxe) gameManager.clickedImage.SetActive(true);
    }
    void OnMouseUp()
    {
        if (gameManager.pickaxe) gameManager.clickedImage.SetActive(false);
    }

    void doEffect()
    {
        Vector3 v2 = Input.mousePosition;
        v2.z = 1f;

        v2 = Camera.main.ScreenToWorldPoint(v2);

        GameObject instance = (GameObject)Instantiate(rockDirt, v2, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15f, 15f), Random.Range(-15f, 25f)));

        StartCoroutine(DisableRigidbody(instance));
        StartCoroutine(DestroyInstance(instance));
    }

    void Dead()
    {
        killed = true;

        anim.Play("killAnim");

        StartCoroutine(kill());
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

    IEnumerator kill()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        DropItems();


        yield return new WaitForSeconds(4f);



        Destroy(this.gameObject);
    }

    void DropItems()
    {
        GameObject instance = (GameObject)Instantiate(Item, this.transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15f, 15f), 20f));
        StartCoroutine(DisableRigidbody(instance));

        instance = (GameObject)Instantiate(Item, this.transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15f, 15f), 20f));
        StartCoroutine(DisableRigidbody(instance));

        instance = (GameObject)Instantiate(Item, this.transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15f, 15f), 20f));
        StartCoroutine(DisableRigidbody(instance));



    }

}
