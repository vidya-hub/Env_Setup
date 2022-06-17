using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public Vector3 requiredOffset = new Vector3(2.97f, 2.49f, -0.46f);
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                transform.position = player.transform.position;
            }
        }
    }
    void Update()
    {
        float playerPosition = player.transform.position.x;
        transform.position = player.transform.position + requiredOffset;
    }
}
