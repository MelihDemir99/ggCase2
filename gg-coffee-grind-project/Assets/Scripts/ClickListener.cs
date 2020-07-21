using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //ClickListener is used to detect the user
    //clicks and to separate between taps and
    //swipes from the user. This class is mainly
    //here to reduce the cumber in the Grinder class.
public class ClickListener : MonoBehaviour
{
    public GameObject grinderObj;
    Grinder grinder;
    Vector2 firstPos, secondPos;

    //swipeThreshold can be modified depending
    //on the screen size.
    public float swipeThreshold = 10;
    void Awake()
    {
        grinder = grinderObj.GetComponent<Grinder>();
        firstPos = new Vector2(0f, 0f);
        secondPos = new Vector2(0f, 0f);    
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            firstPos.x = Input.mousePosition.x;
            firstPos.y = Input.mousePosition.y;
        }
        if(Input.GetMouseButtonUp(0)){
            secondPos.x = Input.mousePosition.x;
            secondPos.y = Input.mousePosition.y;
            if(Mathf.Abs(secondPos.x - firstPos.x) + Mathf.Abs(secondPos.y - firstPos.y) < swipeThreshold){
                grinder.TapEvent();
            }else{
                grinder.SwipeEvent();
            }
        }
    }
}
