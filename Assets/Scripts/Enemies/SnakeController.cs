using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SnakeController : MonoBehaviour {

    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;
    [SerializeField]
    float speed=1.0f;
    private bool directionUp = true;
    private float upperPosition;
    private float lowerPosition;
    private bool isRotated;
	// Use this for initialization
	void Start () {
        if(startPoint.position.y<endPoint.position.y){
            directionUp = true;
            lowerPosition = startPoint.position.y;
            upperPosition = endPoint.position.y;
        }else{
            directionUp = false;
            lowerPosition = endPoint.position.y;
            upperPosition = startPoint.position.y;
        }
        if (directionUp==false)
        {
            isRotated = true;
            transform.Rotate(Vector2.right, 180.0f);
        }else{
            isRotated = false;
        }
        transform.position =new Vector2(transform.position.x, startPoint.position.y);
        //GetComponent<SpriteRenderer>().enabled = true;

	}
	
	// Update is called once per frame
    void Update()
    {
        if (directionUp)
        {
            
            if(isRotated==true){
                isRotated = false;
                transform.Rotate(Vector2.right, 180.0f);
            }
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y>=upperPosition)
            {
                directionUp = false;
            }
        }
        else
        {
            if(isRotated==false){
                isRotated = true;
                transform.Rotate(Vector2.right,180.0f);
            }
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y<lowerPosition)
            {
                directionUp = true;
            }
        }
    }
}
