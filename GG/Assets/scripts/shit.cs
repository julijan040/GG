using UnityEngine;
using System.Collections;

public class shit : MonoBehaviour
{
    public int hp;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void OnMouseDown()
    {
        if (hp != 0) hp--;
        else Destroy(this.gameObject);
        anim.Play("treeAnim");
    }

}
