using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EmptyTileScript : MonoBehaviour {

    private Collider2D col;

	private void Awake()
	{
        
        col = GetComponent<Collider2D>();
        col.enabled = false;


	}
	
	private void Update()
	{
        if (transform.childCount > 0 && col.enabled)
        {
            col.enabled = false;
        }
        else if (transform.childCount == 0 && col.enabled==false)
        {
            col.enabled = true;
        }

	}

}
