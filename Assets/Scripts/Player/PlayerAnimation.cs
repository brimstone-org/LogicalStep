using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

   
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private SpriteRenderer body;
    [SerializeField]
    private SpriteRenderer hat;
    [SerializeField]
    private Animator animator;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (playerController.jumpAnimation)
        {
            animator.Play("Move");
        }
        else if (playerController.deadAnimation)
        {
            body.enabled = true;
            hat.enabled = true;
            //animator.Play("Die");
        }
        else if (playerController.deadAnimation==false) 
            { body.enabled = false;
            hat.enabled = false;
            }
	}
}
