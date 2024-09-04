using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    [HideInInspector]
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    bool isDragging = false;
    [HideInInspector]
    private Vector2 startTouch, swipeDelta;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        tap = swipeLeft = swipeUp = swipeRight = swipeDown = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase==TouchPhase.Canceled ){
                isDragging = false;
                Reset();
            }

        }
        #endregion

        //calculate distance
        swipeDelta = Vector2.zero;
        if(isDragging){
            if(Input.touches.Length>0){
                swipeDelta = Input.touches[0].position - startTouch;
            }else if(Input.GetMouseButton(0)){
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }


        //did we crossed the deadzone?
        if(swipeDelta.magnitude > 20){
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x)>Mathf.Abs(y)){
                //left or right
                if(x<0){
                    swipeLeft = true;
                }else{
                    swipeRight = true;
                }
            }else {
                //up or down
                if (y < 0) { 
                    swipeDown = true; 
                }else{
                    swipeUp = true;
                }
            }
            Reset();
        }
    }

	private void Reset()
	{
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
	}
}
