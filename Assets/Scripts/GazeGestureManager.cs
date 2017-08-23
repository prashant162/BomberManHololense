using UnityEngine;
using UnityEngine.VR.WSA.Input;
using BomberManGame.Blast;
using System.Collections;
using System.Threading;

public class GazeGestureManager : MonoBehaviour
{

    #region Variables and properties
    public static GazeGestureManager Instance { get; private set; }
    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }
    public GameObject explodeObject;
    public GameObject bombObject;
    GestureRecognizer recognizer;
    public float explodeAfterSeconds = 3f;
    public float flameVisibilityInSeconds = 3f;
    private GameObject planeObject = null;
    Vector3 oldHeadPosition = new Vector3(-1, -1, -1);
    Vector3 blastPoint = new Vector3(0, 0, 0);
    Queue myQueue = new Queue();

    #endregion

    // Use this for initialization
    void Awake()
    {
        Instance = this;

        planeObject = GameObject.FindGameObjectWithTag("plane");

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                this.BroadcastMessage("OnSelect", hit.point);
                //que the differen bomb locations so that in Blast bomb it can be taken out in FIFO order
                myQueue.Enqueue(hit.point);
                Invoke("BlastBomb", explodeAfterSeconds);
            }
            
           
        };
        recognizer.StartCapturingGestures();
    }

    private void BlastBomb()
    {
            Object gameObj = Instantiate(explodeObject, (Vector3)myQueue.Dequeue(), explodeObject.transform.rotation);
            Destroy(gameObj, flameVisibilityInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        
        oldHeadPosition = headPosition;
        planeObject.transform.position = headPosition + gazeDirection * 25;
                
        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}