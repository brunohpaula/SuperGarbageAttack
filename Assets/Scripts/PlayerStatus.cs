using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour {

    public bool poo = false;

    [SerializeField]
    private bool handsOccupied = false;

    private bool action = false;

    private GameObject objInHands;

    private bool isDropping = false;


    public bool GetPoo()
    {
        return poo;
    }

    public void SetPoo(bool b)
    {
        poo = b;
    }

    public void SetHandsOccupied (bool b, GameObject ingameObject)
    {
        handsOccupied = b;
        objInHands = ingameObject;
        /*
        if (objInHands.GetComponent<Rigidbody2D>())
        { 
            objInHands.GetComponent<Rigidbody2D>().Sleep();
            Debug.Log("issleeping " + objInHands.GetComponent<Rigidbody2D>().IsSleeping());
        }
        */
    }

    public bool GetHandsOccupied()
    {
        return handsOccupied;
    }

    
            
    

    public bool GetAction()
    {
        return action;
    }

    //drops objects (either garbage or bins), doing the appropriate parenting/unparenting in the transforms
    void DropObject()
    {        
        if (objInHands.tag.Contains("Garbage"))
        { 
            objInHands.transform.SetParent(null);
            objInHands.GetComponent<GarbagePickup>().RestoreRigidbody();
        }
        //releases bin
        else
        {
            this.transform.parent = null;
            if (GetComponent<PlayerOneControls>())
                GetComponent<PlayerOneControls>().SetAttachedToBin(false);
            else if (GetComponent<PlayerTwoControls>())
                GetComponent<PlayerTwoControls>().SetAttachedToBin(false);
        }
        SetHandsOccupied(false, null);
        
    }
    



    public void SetAction(bool act)
    {
        Debug.Log("Action now is " + act);
        action = act;
        
        if (action && handsOccupied)
        {
            Debug.Log("drop obj");
            DropObject();
            Debug.Log("Finally Dropped");
        }
    }

}
