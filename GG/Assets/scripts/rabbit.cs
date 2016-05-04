using UnityEngine;
using System.Collections;

public class rabbit : MonoBehaviour {

    public gameManager gameManager;
    public float hp;
    public Animator anim;
    public Rigidbody2D rigid;

    float maxHp;

    public bool killed;

    public GameObject Item;

    public GameObject bloodDirt;

    public GameObject rabbitObj;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        maxHp = hp;

        StartCoroutine(moveInRandomDir());
    }
    

    IEnumerator moveInRandomDir()
    {
        yield return new WaitForSeconds(Random.Range(5, 25f));

        float x = transform.position.x + Random.Range(-20f, 20f);
        float y = transform.position.y + Random.Range(-20f, 20f);

        anim.Play("rabbitMoving");
        rigid.AddForce(new Vector2(x,y), ForceMode2D.Force);
        StartCoroutine(changeAnimation());
        StartCoroutine(moveInRandomDir());
    }


    IEnumerator changeAnimation()
    {
        yield return new WaitForSeconds(0.1f);

        float speed = rigid.velocity.magnitude;

        while (speed > Mathf.Epsilon)
        {
            speed = rigid.velocity.magnitude;
            yield return new WaitForSeconds(0.1f);           
        }
        anim.Play("rabbitAnim");
    }

    void OnMouseDrag()
    {

        if (gameManager.sword)
        {
            if (!killed)
            {
                doEffect();
                gameManager.updateClick(hp, maxHp);
                anim.Play("treeAnim");
                if (hp > 0) hp -= Time.deltaTime;
                else Dead();
            }
        }
          



    }

    void OnMouseDown()
    {
        if (gameManager.sword) gameManager.clickedImage.SetActive(true);
    }
    void OnMouseUp()
    {
        if (gameManager.sword) gameManager.clickedImage.SetActive(false);
    }

    void Dead()
    {
        killed = true;

        StopCoroutine(changeAnimation());
        StopCoroutine(moveInRandomDir());

        anim.Play("killAnim");

        StartCoroutine(kill());


    }

    void doEffect()
    {
        Vector3 v2 = Input.mousePosition;
        v2.z = 1f;

        v2 = Camera.main.ScreenToWorldPoint(v2);

        GameObject instance = (GameObject)Instantiate(bloodDirt, v2, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-15f, 15f), Random.Range(-15f, 25f)));

        StartCoroutine(DisableRigidbody(instance));
        StartCoroutine(DestroyInstance(instance));
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

    IEnumerator enableAfterNCollider(GameObject deerObj1)
    {
        yield return new WaitForSeconds(10f);
        if (Physics2D.OverlapCircle(deerObj1.transform.position, 0.25f))
        {

            StartCoroutine(enableAfterNCollider(deerObj1));
        }
        else
        {
            deerObj1.SetActive(true);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "rabbit")
        {
            GameObject deerObj1 = (GameObject)Instantiate(rabbitObj, this.transform.position, Quaternion.identity);
            deerObj1.SetActive(false);
            StartCoroutine(enableAfterNCollider(deerObj1));

        }
    }

}
