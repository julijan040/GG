using UnityEngine;
using System.Collections;

public class abonimacija : MonoBehaviour {

    public gameManager gameManager;
    public float hp;
    public Animator anim;
    public Rigidbody2D rigid;

    public GameObject bloodDirt;

    public bool killed;

    public GameObject Item;

    float maxHp;
    

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        maxHp = hp;

        StartCoroutine(moveInRandomDir());

        StartCoroutine(LookForFood());
    }

    IEnumerator LookForFood()
    {
        yield return new WaitForSeconds(5f);
        Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, 1f);
        

        foreach (Collider2D item in col)
        {
            if (item.gameObject.tag == "deer" || item.gameObject.tag == "rabbit")
            {
                StartCoroutine(WaitAndMove(item.gameObject.transform.position));
                break;
            }
               
        }

        StartCoroutine(LookForFood());


    }

    IEnumerator WaitAndMove(Vector2 cilj)
    {
        float startTime = Time.time;
        while (Time.time - startTime <= 3)
        {
            transform.position = Vector2.Lerp(this.transform.position, cilj, (Time.time - startTime)); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
        
    }



    IEnumerator moveInRandomDir()
    {
        yield return new WaitForSeconds(Random.Range(5, 25f));

        if (!killed)
        {
            float x = transform.position.x + Random.Range(-30f, 30f);
            float y = transform.position.y + Random.Range(-30f, 30f);
            
            rigid.AddForce(new Vector2(x, y), ForceMode2D.Force);
            
            StartCoroutine(moveInRandomDir());
        }




    }

    void OnMouseDrag()
    {

        if (gameManager.sword)
        {
            if (!killed)
            {
                doEffect();
                gameManager.updateClick(hp, maxHp);

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

        StopCoroutine(moveInRandomDir());

        Destroy(this.gameObject);


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
        anim.Play("deerAnim");
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "rabbit")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "deer")
        {
            Destroy(col.gameObject);
        }
    }
}
