using System.Collections;
using UnityEngine;

public class PinchGestureCommands : MonoBehaviour
{
    public GameObject bombItem;
    public GameObject fireObject;
    public float time = 3f;
    public Vector3 bombCreateDistance = new Vector3(0, 0, 3);

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        if (bombItem != null)
        {
            Debug.Log("Instantiation Started");

            Object gameObj = Instantiate(bombItem, Camera.current.transform.forward + bombCreateDistance, bombItem.transform.rotation);
            Debug.Log(Camera.current.transform.position + Camera.current.transform.forward);
            Destroy(gameObj, time);
            //WaitAndDestroy(time, bombItem, fireObject);
            Debug.Log("Instantiation Ended");

        }
    }
}