using UnityEngine;
using UnityEngine.VR.WSA.Input;
using BomberManGame.Blast;
using System.Collections;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }
    //public GameObject blastObject;
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
                //transform.position = hit.point;
                this.BroadcastMessage("OnSelect", hit.point);
                blastPoint = hit.point;
                Invoke("BlastBomb", explodeAfterSeconds);
                Debug.Log("In air tap method");
            }
            
           
        };
        recognizer.StartCapturingGestures();
    }

    private void BlastBomb()
    {
        Debug.Log("In BlastBomb Method");
        Object gameObj = Instantiate(explodeObject, blastPoint, explodeObject.transform.rotation);
        Destroy(gameObj, flameVisibilityInSeconds);
        Debug.Log(gameObj.name);
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
        
        //if (Vector3.Distance(oldHeadPosition, headPosition) > 0)
        //{
            oldHeadPosition = headPosition;
            planeObject.transform.position = headPosition + gazeDirection * 25;
            //Debug.Log("head position: " + headPosition);
        //}
        
        
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

    IEnumerator WaitAndDestroy(float time, GameObject currenObject)
    {
        //explosionSound = GetComponent<AudioSource>().clip = ;

        yield return new WaitForSeconds(time);
        //var explodeObject = GameObject.FindGameObjectWithTag("GroundExplosion");
        if (explodeObject != null)
        {
            currenObject.SetActive(false);
            explodeObject.SetActive(true);
            Destroy(explodeObject, 3);
        }
    }
}