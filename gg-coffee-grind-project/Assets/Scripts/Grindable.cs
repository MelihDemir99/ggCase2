using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Grindable class is for any object that can be grinded through the blades.
    //Though specifically used on coffee beans on the scope of this project,
    //the script can be added to any object later on.
public class Grindable : MonoBehaviour
{
    //To not grind every coffee bean immediately, we need to use a event queue system.
    //This is held on the Grinder object, though it could also have been handled in
    //a static script.
    public GameObject grinderObj;
    Grinder grinder;
    private void Awake() {
        grinder = grinderObj.GetComponent<Grinder>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Blade"){
            grinder.QueueThis(gameObject);
        }
    }
}

