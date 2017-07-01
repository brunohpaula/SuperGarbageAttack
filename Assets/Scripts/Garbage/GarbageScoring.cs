using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageScoring : MonoBehaviour
{

    private GameManager gameMng;

    private BoxCollider2D _collider;

    public int scoreMissed = -15;

    
    public int scoreCorrectBin = 20;
    
    public int scoreWrongBin = -15;



    [SerializeField]
    private float missedLine = 6.3f;

    private int numberOfTimes;


	// Use this for initialization
	void Start ()
    {
        gameMng = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("trigger enter " + col.gameObject.tag);

        if (col.gameObject.tag == "MainCamera")
        {

            //if (this.transform.position.x < (col.transform.position.x-col.gameObject.GetComponent<CameraScroller>().xOffset)+(1.5*this.GetComponent<BoxCollider2D>().size.x))
            Vector3 distance = this.transform.position - col.gameObject.transform.position;
            //Debug.Log("distance is " + distance);

            //back of scenario
            if (distance.x >= 6.3f)
            {
                gameMng.ManipulateScore(scoreMissed);
                gameMng.RegisterMiss(this.gameObject.tag);
                Destroy(this.gameObject);
            }


        }
    }

    //everything below here is now deprecated since we're using all trigger colliders for garbage now
    /*
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            //StartCoroutine("RestoreCollider");
        }
    }

    
    IEnumerator RestoreCollider()
    {
        Debug.Log("reached coroutine");
        while (_collider.isTrigger)
        {
            Vector3 distance = this.transform.position - GameObject.FindWithTag("MainCamera").transform.position;
            Debug.Log("dist corout " + distance);
            if (distance.x < -4.0f)
            {
                Debug.Log("ON AGAIN");
                _collider.isTrigger = false;
            }
            else if (numberOfTimes > 9)
            {
                Debug.Log("exit by numtimes " + distance.x);
                _collider.isTrigger = false;
            }
            numberOfTimes++;
        }
        
        yield return null;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("COL enter "+ col.gameObject.tag);

        if (col.gameObject.tag == "MainCamera")
        {
            
            //if (this.transform.position.x < (col.transform.position.x-col.gameObject.GetComponent<CameraScroller>().xOffset)+(1.5*this.GetComponent<BoxCollider2D>().size.x))
            Vector3 distance = this.transform.position - col.gameObject.transform.position;
            //Debug.Log("distance is " + distance);

            //back of scenario
            if (distance.x >= 6.3f )
            {
                gameMng.ManipulateScore(scoreMissed);
                gameMng.RegisterMiss(this.gameObject.tag);
                Destroy(this.gameObject);
            }

            
        }

    }
    */


}
