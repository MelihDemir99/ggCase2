using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Grinder is the main logic engine of our simulation.
    //This script handles the event queue system for grinding
    //the coffee beans, as well as keeping track of the size
    //of the coffee pile in the powder box. The falling coffee
    //effect is part of the event queue, which is also handled
    //here.
public class Grinder : MonoBehaviour
{
    public GameObject coffeePile;
    public Transform pouringEffect;
    Queue<GameObject> grindables;
    
    #region MeshStuff
    //These variables concern the shape and size of
    //the pile of coffee gathered in the powder box.
    Mesh coffeeMesh;
    public Vector3[] coffeeVertices;
    int[] coffeeTriangles = {0,1,5, 5,1,6, 1,3,6, 3,4,6, 3,2,4, 4,2,5, 2,0,5, 4,5,6};
    Vector2[] coffeeUvs;
    public float beanToPile = 0.03f;
    
    #endregion 
    
    public float waitTime = 5f;

    //The amount of rise for each coffee bean that
    //is grinded and the time between two beans
    //being grinded is left as public variables, as
    //these are preference-based variables and does
    //not change the functionality.

    float powderBoxHeight = 0.25f;
    float maxPileHeight = 0.9f;
    //These are used to prevent overflow, and can be 
    //changed depending on the size of the objects on
    //the scene.
    void Awake()
    {
        grindables = new Queue<GameObject>();

        #region MeshInit
        coffeeMesh = new Mesh();
        coffeeUvs = new Vector2[coffeeVertices.Length];
        for (int i = 0; i < coffeeUvs.Length; i++)
        {
            coffeeUvs[i] = new Vector2(coffeeVertices[i].x, coffeeVertices[i].z);
        }
        coffeeMesh.vertices = coffeeVertices;
        coffeeMesh.triangles = coffeeTriangles;
        coffeeMesh.uv = coffeeUvs;
        coffeePile.GetComponent<MeshFilter>().mesh = coffeeMesh;
        #endregion 

        StartCoroutine(GrindJob());
    }

    void Update(){
        //The position of the vertices can be changed
        //by both the event queue system and the ClickListener,
        //therefore these need to be updated on update.
        coffeeMesh.vertices = coffeeVertices;

        coffeeMesh.RecalculateBounds();
        coffeeMesh.RecalculateNormals();
    }

    //Main coffee bean grinding queue system.
    //Every bean is added to 'grindables' queue
    //and then taken into processing here. A bean
    //is processed by destroying itself, triggering
    //the pouring effect and lastly increasing the
    //coffee pile height.
    IEnumerator GrindJob()
    {
        while(true){
            yield return new WaitForSeconds(waitTime);
            if(grindables.Count > 0){
                GameObject go = grindables.Dequeue();
                Destroy(go);
                pouringEffect.GetComponent<ParticleSystem>().Play();
                if(coffeeVertices[4].y < maxPileHeight){
                    adjustPeak(beanToPile);
                }else if(coffeeVertices[0].y < powderBoxHeight){
                    adjustBottom(beanToPile);
                }
            }
        }
    }

    //adjustPeak & adjustBottom:
    //A couple of handy functions to handle the 
    //positions of coffee pile vertices. If the shape
    //of the mesh changes for any reason, these functions
    //can be updated without changing the main logic.
    void adjustPeak(float f){
        coffeeVertices[4].y += f;
        coffeeVertices[5].y += f;
        coffeeVertices[6].y += f;
    }

    void adjustBottom(float f){
        coffeeVertices[0].y += f;
        coffeeVertices[1].y += f;
        coffeeVertices[2].y += f;
        coffeeVertices[3].y += f;
    }

    //Public function for Grindable objects to call.
    //Basically adds the object to the grinding queue.
    public void QueueThis(GameObject go){
        if(!grindables.Contains(go))
            grindables.Enqueue(go);
    }

    //TapEvent & SwipeEvent:
    //Behaviors expected when tap or swipe event is
    //detected in the ClickListener.
    public void TapEvent(){
        if(coffeeVertices[4].y > coffeeVertices[0].y + beanToPile && coffeeVertices[0].y < powderBoxHeight){
            adjustPeak(-beanToPile);
            adjustBottom(beanToPile);
        }
    }
    public void SwipeEvent(){
        if(coffeeVertices[4].y > coffeeVertices[0].y + beanToPile){
            adjustPeak(-beanToPile);
            if(coffeeVertices[0].y < powderBoxHeight)
                adjustBottom(beanToPile);
        }
    }
}
