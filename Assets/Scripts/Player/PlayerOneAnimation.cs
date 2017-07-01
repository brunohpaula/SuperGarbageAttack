using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneAnimation : MonoBehaviour {

    private bool up, down, left, right;
    private Vector3 dir;

   

	// Use this for initialization
	void Start () {
        up = false;
        down = false;
        left = false;
        right = false;
        
        dir = new Vector3(0, 0, 0);

    }

    void Update()
    {
        if (Input.anyKey)
        {
            ChangeSpriteDirection();
        }
    }

    void ChangeSpriteDirection()
    {



        //UP
        if (Input.GetKey("w"))
        {
            

            if (!up)
            {

                transform.rotation.Set(0, 0, -90, -90);
                //dir = new Vector3(0, 0, -90);
                //transform.Rotate(dir);
                Debug.Log(dir.ToString());
                up = true;
                down = false;
                left = false;
                right = false;
            }
        }
        //DOWN
        else if (Input.GetKey("s"))
        {
            if (!down)
            {
                dir = new Vector3(0, 0, 90);
                transform.Rotate(dir);
                up = false;
                down = true;
                left = false;
                right = false;
            }


        }
        //LEFT
        else if (Input.GetKey("a"))
        {

            if (!left)
            {
                dir = new Vector3(0, 0, -180);
                transform.Rotate(dir);
                up = false;
                down = false;
                left = true;
                right = false;
            }

        }
        else if (Input.GetKey("d"))
        {
            if (!right)
            {
                dir = new Vector3(0, 0, 180);
                transform.Rotate(dir);
                up = false;
                down = false;
                left = false;
                right = true;
            }

        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {

        }
        else
        {

        }

      
       
    }
}
