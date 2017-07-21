using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {
    public float lifeTime;
    public float flameLifeTime;
    public GameObject blastObject;
    private GameObject bomb;

    // Use this for initialization
    void Start () {
        StartCoroutine(WaitAndDestroy(lifeTime));
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator WaitAndDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        blastObject.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        //yield return new WaitForSeconds(flameLifeTime);
        //Destroy(blastObject);
    }
}
