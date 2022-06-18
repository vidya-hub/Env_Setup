using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour

{
    public new Rigidbody2D rigidbody;
    public Vector2 playerVel = new Vector2(5, 5);
    Transform startingPosition;
    public Camera mainCamera;
    public float heightLimit = -1f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    { }
    public void jumpPlayer()
    {
        if (mainCamera.transform.position.y < -1)
        {
            rigidbody.velocity = playerVel;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animationChange(collision, false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animationChange(collision, true);
    }
    private void animationChange(Collision2D collision, bool isStart)
    {
        if (collision.gameObject.name == "Ground")
        {
            rigidbody.GetComponent<Animator>().enabled = isStart;
        }
    }
}
