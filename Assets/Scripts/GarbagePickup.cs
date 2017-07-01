using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbagePickup : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    public bool isPoo = false;

    private GameObject player;
    private bool pickuped = false;

    private void Start()
    {
        player1 = GameObject.FindWithTag("player1");
        player2 = GameObject.FindWithTag("player2");
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("colstay " + collision.gameObject.tag);
        if (player1.tag == collision.gameObject.tag && !player1.GetComponent<PlayerStatus>().poo && !player1.GetComponent<PlayerStatus>().GetHandsOccupied() && player1.GetComponent<PlayerStatus>().GetAction()
            || player2.tag == collision.gameObject.tag && !player2.GetComponent<PlayerStatus>().poo && !player2.GetComponent<PlayerStatus>().GetHandsOccupied() && player2.GetComponent<PlayerStatus>().GetAction()
            
            )
        {

            //we must destroy the rigidbody to assure that pickup/drop will work properly (otherwise w will always fall here and
            //will never drop the object)
            Destroy(GetComponent<Rigidbody2D>());


            Debug.Log("TriggeredAvatar");
            Transform garbage = this.GetComponent<Transform>();
            garbage.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<PlayerStatus>().SetHandsOccupied(true, this.gameObject);
            pickuped = true;
            player = collision.gameObject;
            if (isPoo)
            {
                collision.gameObject.GetComponent<PlayerStatus>().SetPoo(true);
            }

            

        }
    }


    public void RestoreRigidbody()
    {
        this.gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        /*
        if ((player1.tag == collision.gameObject.tag && !player1.GetComponent<PlayerStatus>().poo && !player1.GetComponent<PlayerStatus>().GetGarbageAttached() && player1.GetComponent<PlayerStatus>().GetAction())
            || (player2.tag == collision.gameObject.tag && !player2.GetComponent<PlayerStatus>().poo && !player2.GetComponent<PlayerStatus>().GetGarbageAttached())) 
        {
            Debug.Log("TriggeredAvatar");
            Transform garbage = this.GetComponent<Transform>();
            garbage.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<PlayerStatus>().SetGarbageAttached(true);
            pickuped = true;
            player = collision.gameObject;
            if (isPoo)
            {
                collision.gameObject.GetComponent<PlayerStatus>().SetPoo(true);
            }
        }
        */

    }

    private void OnDestroy()
    {
        if (pickuped)
        {
            player.GetComponent<PlayerStatus>().SetHandsOccupied(false, null);
        }
    }


}
