using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoControls : MonoBehaviour
{

    public float moveSpeed = 5.0f;

    private PlayerStatus playerStatus;


    private Vector3 movementDirection;

    private bool attached;


    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();        
        movementDirection = Vector3.zero;
        attached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            MovePlayer();
        }
    }

    public Vector3 GetMovementVector()
    {
        if (Input.anyKey)
            return movementDirection;
        else
            return Vector3.zero;
    }

    //attaches/detaches player from bin (bin will be the parent, but they are going to move according to player's movement)
    public void SetAttachedToBin(bool isAttached)
    {
        attached = isAttached;
        
        if (attached)
        {
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<Collider2D>().isTrigger = true;
        }

        else
        {
            GetComponent<Collider2D>().isTrigger = false;
                this.gameObject.AddComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

    }

    public bool GetAttachedToBin()
    {
        return attached;
    }


    void MovePlayer()
    {
        movementDirection = Vector3.zero;
        //UP
        if (Input.GetKey("up"))
        {
            //transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            movementDirection = Vector3.up * moveSpeed;            
        }        
        //DOWN
        if (Input.GetKey("down"))
        {

            //transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            movementDirection = -Vector3.up * moveSpeed;
            
        }
        //LEFT
        if (Input.GetKey("left"))
        {

            //transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            movementDirection += Vector3.left * moveSpeed;
            
        }
        if (Input.GetKey("right"))
        {
            movementDirection += Vector3.right * moveSpeed;
            
            //transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }


        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            playerStatus.SetAction(true);
        }
        else if (playerStatus.GetAction())
        {
            playerStatus.SetAction(false);
        }

        
        if (!attached)
            transform.Translate(movementDirection*Time.deltaTime);

    }
}
