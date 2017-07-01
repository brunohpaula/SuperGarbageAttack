using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour {

    void Awake()
    {


    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("TRUCKTRIGGER");
        if (collision.gameObject.tag == "bin")
        {
            Debug.Log("WITH BIN");
            BinCollisionScript bc = collision.gameObject.GetComponent<BinCollisionScript>();
            
        }
    }
}
