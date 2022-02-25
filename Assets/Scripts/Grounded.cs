using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private GameObject player;
    private float initMoveSpeed;
    private float initJumpSpeed;


    private void Start()
    {
        player = gameObject.transform.parent.gameObject;
        initMoveSpeed = player.GetComponent<PlayerController>().moveSpeed;
        initJumpSpeed = player.GetComponent<PlayerController>().jumpSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            player.GetComponent<PlayerController>().moveSpeed = initMoveSpeed;
            player.GetComponent<PlayerController>().jumpSpeed = initJumpSpeed;
            player.GetComponent<PlayerController>().jumpCounter = 0;
            player.GetComponent<PlayerController>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.GetComponent<PlayerController>().moveSpeed /= 4;
        player.GetComponent<PlayerController>().jumpSpeed -= 2;
        player.GetComponent<PlayerController>().isGrounded = false;
    }
}
