using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour

{
    public new Rigidbody2D rigidbody;
    public  Vector2 playerVel = new Vector2(5, 5);

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
    }
    public void jumpPlayer()
    {
        rigidbody.velocity = playerVel; 
    }
}
