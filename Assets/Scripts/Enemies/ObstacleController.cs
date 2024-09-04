using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController: MonoBehaviour {
    SpriteRenderer thisSprite;
    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;
    [SerializeField]
    float speed = 1.0f;
    public bool directionRight = true;
    private float upperPosition;
    private float lowerPosition;
    private bool isRotated;
    // Use this for initialization
    private void Awake() {
        thisSprite =gameObject.GetComponent<SpriteRenderer>();
    }
    void Start() {
        if(startPoint.position.x < endPoint.position.x) {
            directionRight = true;
            lowerPosition = startPoint.position.x;
            upperPosition = endPoint.position.x;
        } else {
            directionRight = false;
            lowerPosition = endPoint.position.x;
            upperPosition = startPoint.position.x;
        }
        if(directionRight == false) {
            isRotated = true;
            thisSprite.flipX=false;
            transform.Rotate(Vector2.right,180.0f);
           
            
        } else {
            isRotated = false;
        }
        transform.position = new Vector2(startPoint.position.x,transform.position.y);
        //GetComponent<SpriteRenderer>().enabled = true;

    }

    // Update is called once per frame
    void Update() 
    {
        if(directionRight) 
        {
            thisSprite.flipX = true;
            if(isRotated == true) 
            {
               isRotated = false;
               transform.Rotate(Vector2.right,180.0f);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if(transform.position.x >= upperPosition) 
            {
                directionRight = false;
            }
        } 
        else 
        {
            thisSprite.flipX = false;
            if(isRotated == false) 
            {
                isRotated = true;
                transform.Rotate(Vector2.right,180.0f);
               
            }
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            if(transform.position.x < lowerPosition) 
            {
                directionRight = true;
            }
        }
    }
}
