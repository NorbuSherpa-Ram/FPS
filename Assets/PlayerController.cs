using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header ("Movement")]
    float normalSpeed;
    Vector3 moveDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] float airMultiplyer = 0.1f;
    [SerializeField] float groundMultiplyer = 4f;

    [Header ("Jump")]
    bool isJump;
    bool isGround;
    [SerializeField] float jumpForce;
    [SerializeField] float radious = .5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    [Header ("Physic")]
    public float airDrag = .05f;
    public float normalDrag = 6f;
    [SerializeField] Rigidbody playerRb;


    float horInput;
    float verInput;
    void Start()
    {
        normalSpeed = moveSpeed;
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
    }
    void Update()
    {

        InputContoller();

        isGround = Physics.CheckSphere(groundCheck.position, radious, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;
            isGround = false;
        }
        if (isGround)
        {
            playerRb.drag = normalDrag ;
        }
        else
        {
            playerRb.drag = airDrag;
        }
    }

    void InputContoller()
    {
        horInput = Input.GetAxis("Horizontal") * Time.deltaTime;
        verInput = Input.GetAxis("Vertical") * Time.deltaTime;

        moveDirection = transform.TransformDirection(horInput, 0, verInput);

    }
    private void FixedUpdate()
    {
        Move();

        if (isJump)
        {
            Jump();
            isJump = false;
        }
    }
    void Move()
    {
        if (isGround)
        {
            playerRb.AddForce(moveDirection.normalized * moveSpeed * groundMultiplyer , ForceMode.Acceleration);
        }
        else
        {
            playerRb.AddForce(moveDirection.normalized * moveSpeed * airMultiplyer   , ForceMode.Acceleration);
        }
    }
    void Jump()
    {
        playerRb.AddForce(transform.up * jumpForce * Time.deltaTime * groundMultiplyer , ForceMode.Impulse);
    }

}
