using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {

    public gameManager gameManager;
    public float hp;
    public Animator anim;

    public GameObject treeDirt;

    public bool killed;

    public GameObject Item;

    float maxHp;

    public int numOfDrops;





    void Start()
    {
        anim = GetComponent<Animator>();
        maxHp = hp;
    }

    void Dead()
    {
        killed = true;
        
        anim.Play("killAnim");

        StartCoroutine(kill());


    }


    void OnMouseDrag()
    {
        
       
        if(!killed && gameManager.axe)
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
        if(gameManager.axe) gameManager.clickedImage.SetActive(true);
    }
    void OnMouseUp()
    {
        if (gameManager.axe) gameManager.clickedImage.SetActive(false);
    }

    void doEffect()
    {
        Vector3 v2 = Input.mousePosition;
        v2.z = 1f;

        v2 = Camera.main.ScreenToWorldPoint(v2);

        GameObject instance = (GameObject)Instantiate(treeDirt, v2, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15f, 15f), Random.Range(-15f, 25f)));

        StartCoroutine(DisableRigidbody(instance,0.8f));
        StartCoroutine(DestroyInstance(instance));
    }

    IEnumerator DestroyInstance(GameObject instance)
    {
        yield return new WaitForSeconds(1f);

        instance.GetComponent<Animator>().Play("shrinkAnim");

        yield return new WaitForSeconds(1f);

        Destroy(instance);
    }

    IEnumerator DisableRigidbody(GameObject instance, float time)
    {
        yield return new WaitForSeconds(time);
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
        for(int i = 0; i<numOfDrops; i++)
        {
            GameObject instance = (GameObject)Instantiate(Item, this.transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15f, 15f), 20f));
            StartCoroutine(DisableRigidbody(instance, 0.65f));
        }
        



    }


}
