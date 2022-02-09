using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyMovement : MonoBehaviour
{
    private Rigidbody2D rigidTrolley;
    public static bool onRightHand, onLeftHand;

    public static bool moving;
    public float moveSpeed;
    public Transform leftPosition, rightPosition;
    public Transform destination;

    private void Start()
    {
        rigidTrolley = GetComponent<Rigidbody2D>();
        destination = rightPosition;
        moving = false;
        onRightHand = false;
    }

    private void Update()
    {
        Movement();
        if (onLeftHand)
            destination = rightPosition;
        if (onRightHand)
            Invoke("TrolleyBack", 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Right"))
        {
            moving = false;
            onRightHand = true;
            onLeftHand = false;
        }
        if (other.CompareTag("Left"))
        {
            moving = false;
            onLeftHand = true;
        }
    }

    public void Movement()
    {
        if (moving)
        {
            onLeftHand = onRightHand = false;
            if (transform.position.x < destination.position.x)
            {
                rigidTrolley.velocity = new Vector2(moveSpeed, 0);
            }
            else
            {
                rigidTrolley.velocity = new Vector2(-moveSpeed, 0);
            }
        }
    }

    private void TrolleyBack()
    {
        destination = leftPosition;
        moving = true;
        onRightHand = false;
    }
}
