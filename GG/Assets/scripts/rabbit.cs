using UnityEngine;
using System.Collections;

public class rabbit : MonoBehaviour {

    public gameManager gameManager;
    public int hp;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(moveInRandomDir());
    }

    IEnumerator moveInRandomDir()
    {
        yield return new WaitForSeconds(Random.Range(5, 10f));

        anim.Play("rabbitMoving");
        StartCoroutine(MoveToPosition(this.transform, new Vector3(transform.position.x+1f, transform.position.y+1f, transform.position.z), 5f));
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
    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

}
