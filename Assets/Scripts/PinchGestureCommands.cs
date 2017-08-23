using System.Collections;
using UnityEngine;

public class PinchGestureCommands : MonoBehaviour
{
    public GameObject bombItem;
    public float time = 3f;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect(Vector3 hitPoint)
    {
        if (bombItem != null)
        {
            Object gameObj = Instantiate(bombItem, hitPoint, bombItem.transform.rotation);
            Destroy(gameObj, time);

        }
    }
}