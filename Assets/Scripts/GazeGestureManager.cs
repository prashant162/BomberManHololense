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

    // Use this for initialization
    void Awake()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            this.BroadcastMessage("OnSelect");
            Invoke("BlastBomb", explodeAfterSeconds);
            Debug.Log("In air tap method");
           
        };
        recognizer.StartCapturingGestures();
    }

    private void BlastBomb()
    {
        Debug.Log("In BlastBomb Method");
        Object gameObj = Instantiate(explodeObject, explodeObject.transform.position, explodeObject.transform.rotation);
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