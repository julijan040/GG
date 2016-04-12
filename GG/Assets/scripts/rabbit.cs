using UnityEngine;
using System.Collections;

public class rabbit : MonoBehaviour {

    public gameManager gameManager;
    public int hp;
    public Animator anim;
    public Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

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

    void OnMouseDown()
    {
        gameManager.food++;
        gameManager.updateUI();
        if (hp != 0) hp--;
        else Destroy(this.gameObject);
        anim.Play("treeAnim");
    }
    
    

}
