using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemy;
    private Vector2 startingPosition;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = enemy.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemy.transform.position = startingPosition;
    }

}
