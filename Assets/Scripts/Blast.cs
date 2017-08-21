using UnityEngine;
using System.Collections;

namespace BomberManGame.Blast
{
    public class Blast : MonoBehaviour
    {
        public float lifeTime;
        public float flameLifeTime;
        //public GameObject blastObject;
        private bool oneTime = true;
        public float time = 3f;
        //internal GameObject explodeObject = null;
        public GameObject fireObject;

        // Use this for initialization
        void Start()
        {
            //var explodeObject = GameObject.FindGameObjectWithTag("GroundExplosion");
            //explodeObject.SetActive(false);
            //Instantiate(fireObject, gameObject.transform.position, gameObject.transform.rotation);
        }

        // Update is called once per frame
        void Update()
        {

            if (oneTime)
            {
                //explodeObject = GameObject.FindGameObjectWithTag("GroundExplosion");
                //this.GetComponent<AudioSource>().Play(132300);

                if (fireObject != null)
                {
                    //Debug.Log(Camera.current.transform.position);
                    //Instantiate(fireObject, gameObject.transform.position, gameObject.transform.rotation);
                    oneTime = false;
                }
                
            }
            //if (time > 0)
            //{
            //    time -= Time.deltaTime;
            //}
            //else
            //{
            //    blastObject.GetComponent<AudioSource>().Play();
            //}
        }

        //public IEnumerator WaitAndDestroy(float time, GameObject currenObject, GameObject explodeObject)
        //{
        //    //explosionSound = GetComponent<AudioSource>().clip = ;

        //    yield return new WaitForSeconds(time);

        //    Destroy(currenObject);

        //    explodeObject.SetActive(true);
        //    Destroy(explodeObject, 3);

        //}

        public IEnumerator WaitAndDestroy(float time)
        {
            //explosionSound = GetComponent<AudioSource>().clip = ;

            yield return new WaitForSeconds(time);

            //Destroy(gameObject);
            Instantiate(this.gameObject, Vector3.MoveTowards(GetComponent<Camera>().transform.position, Vector3.forward, 5.0f), Quaternion.Euler(0, 0, 180));
            //blastObject.SetActive(true);
            //Destroy(blastObject, 3);

        }
    }
}
