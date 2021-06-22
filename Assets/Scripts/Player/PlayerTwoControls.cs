using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwoControls : MonoBehaviour
{

    public float moveSpeed = 5.0f;

    private PlayerStatus playerStatus;

    private Vector3 movementDirection;

    private bool attached;

    private Transform playerSpriteTransform;

    [SerializeField]
    private PlayerInput input;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();        
        movementDirection = Vector3.zero;
        attached = false;

        playerSpriteTransform = gameObject.transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //move player according to the movementDirection set via Input
        transform.Translate(movementDirection * Time.deltaTime);

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


    public void OnAction()


    /// <summary>
    /// sets the movement according to the input - uses new InputManager
    /// </summary>
    /// <param name="val">the value read from the InputMapping</param>
    public void OnMovement(InputAction.CallbackContext val)
    {
        Vector2 inputMov = val.ReadValue<Vector2>();
        movementDirection = new Vector3(inputMov.x, inputMov.y, 0) * moveSpeed;

        Debug.Log(movementDirection);

        //flip sprite
        if (movementDirection.x > 0)
        {
            if (movementDirection.y > 0)
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 45f);
            }
            else if (movementDirection.y < 0)
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 315f);
            }
            else
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 0f);
            }
        }
        else if (movementDirection.x < 0)
        {
            if (movementDirection.y > 0)
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 135f);
            }
            else if (movementDirection.y < 0)
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 225f);
            }
            else
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 180f);
            }
        }
        else
        {
            if (movementDirection.y > 0)
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 90f);
            }
            else if (movementDirection.y < 0)
            {
                playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 270f);
            }
        }
               
    }


    
}
