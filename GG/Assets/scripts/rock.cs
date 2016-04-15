﻿using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

    public gameManager gameManager;
    public int hp;
    public Animator anim;

    public GameObject rockDirt;

    public bool killed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void OnMouseDown()
    {
        if(!killed)
        {
            doEffect();
            gameManager.rock++;
            gameManager.updateUI();
            anim.Play("treeAnim");
            if (hp != 0) hp--;
            else Dead();

        }

    }

    void doEffect()
    {
        Vector3 v2 = Input.mousePosition;
        v2.z = 1f;

        v2 = Camera.main.ScreenToWorldPoint(v2);

        GameObject instance = (GameObject)Instantiate(rockDirt, v2, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 15f));

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
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

}
