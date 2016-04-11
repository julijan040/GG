﻿using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {

    public gameManager gameManager;
    public int hp;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    

    void OnMouseDown()
    {
        gameManager.wood++;
        gameManager.updateUI();
        if (hp != 0) hp--;
        else Destroy(this.gameObject);
        anim.Play("treeAnim");
    }


}