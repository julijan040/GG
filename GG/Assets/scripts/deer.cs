using UnityEngine;
using System.Collections;

public class deer : MonoBehaviour {

    public gameManager gameManager;
    public int hp;
    public Animator anim;
    public Rigidbody2D rigid;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(moveInRandomDir());
    }

    IEnumerator moveInRandomDir()
    {
        yield return new WaitForSeconds(Random.Range(5, 25f));

        float x = transform.position.x + Random.Range(-20f, 20f);
        float y = transform.position.y + Random.Range(-20f, 20f);

        anim.Play("deerMoving");
        rigid.AddForce(new Vector2(x, y), ForceMode2D.Force);
        StartCoroutine(changeAnimation());
        StartCoroutine(moveInRandomDir());
    }

    void OnMouseDown()
    {
        gameManager.food++;
        gameManager.updateUI();
        if (hp != 0) hp--;
        else Destroy(this.gameObject);
        anim.Play("treeAnim");
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
}
