using UnityEngine;
using System.Collections;

public class deer : MonoBehaviour {

    public gameManager gameManager;
    public int hp;
    public Animator anim;
    public Rigidbody2D rigid;

    public GameObject bloodDirt;

    public bool killed;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(moveInRandomDir());
    }

    IEnumerator moveInRandomDir()
    {
        yield return new WaitForSeconds(Random.Range(5, 25f));

        if (!killed)
        {
            float x = transform.position.x + Random.Range(-20f, 20f);
            float y = transform.position.y + Random.Range(-20f, 20f);

            anim.Play("deerMoving");
            rigid.AddForce(new Vector2(x, y), ForceMode2D.Force);

            StartCoroutine(changeAnimation());
            StartCoroutine(moveInRandomDir());
        }
            
       
        
        
    }

    void OnMouseDown()
    {

        if (!killed)
        {
            doEffect();
            gameManager.food++;
            gameManager.updateUI();
            anim.Play("treeAnim");
            if (hp != 0) hp--;
            else Dead();
            
        }
           
    }
    
    void Dead()
    {
        killed = true;

        StopCoroutine(changeAnimation());
        StopCoroutine(moveInRandomDir());

        anim.Play("killAnim");

        StartCoroutine(kill());
        

    }

    IEnumerator kill()
    {
       
        yield return new WaitForSeconds(4f);
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
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 15f));

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
}
