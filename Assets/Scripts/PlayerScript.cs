using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour

{
    private new Rigidbody2D rigidbody;
    public Vector2 playerVel = new Vector2(5, 5);
    public Camera mainCamera;
    public float heightLimit = -1f;
    private bool isAttacked = false;
    public Vector2 fallForce = new Vector2(-500, -1000);
    private CircleCollider2D circleCollider2D;
    public float bounciNess = 0.2f;
    public float rotationAfterAttack = 30f;
    public GameObject enemyGameObject;
    public int noOfEnimies;
    private List<GameObject> enemyList = new List<GameObject>();
    private int indexOfCrossedEnemy = 0;
    private Vector2 playerPosition;
    private float playerXposition;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        circleCollider2D = this.GetComponent<CircleCollider2D>();
        playerPosition = rigidbody.transform.position;
        playerXposition = playerPosition.x;
        setRandomEnemies();
    }
    void Update()
    {
        Vector2 playerPosition = rigidbody.position;
        // handle the bird enemy system

        handleEnemiesSystem();
    }
    // TODO: when collision happened
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onEnemyCollide(collision);
        animationChange(collision, false);
    }

    // TODO: after collision happened
    private void OnCollisionExit2D(Collision2D collision)
    {
        animationChange(collision, !isAttacked);
    }
    // TODO: button Method
    public void jumpPlayer()
    {
        if (mainCamera.transform.position.y < -1 && !isAttacked)
        {
            rigidbody.velocity = playerVel;
        }

    }
    // TODO: change the animation status of player
    private void animationChange(Collision2D collision, bool isStart, string colliderName = "Ground")
    {
        if (collision.gameObject.name == colliderName)
        {
            rigidbody.GetComponent<Animator>().enabled = isStart;
        }
    }
    // TODO: on Enemy Attack happend
    private void onEnemyCollide(Collision2D collision)
    {
        string collIndexName = "Enemy-" + indexOfCrossedEnemy;
        if (collision.gameObject.name == "Enemy" || collision.gameObject.name == collIndexName)
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.rotation = rotationAfterAttack;
            setBounciness(bounciNess);
            // animationChange(collision, false, "Enemy");
            rigidbody.AddRelativeForce(fallForce);
            isAttacked = true;
        }
    }
    // TODO: set the bounciness of the player when Enemy Attack happend

    private void setBounciness(float bounciness = 0)
    {
        circleCollider2D.sharedMaterial.bounciness = bounciness;
    }
    // TODO: check whether player crossed the enemy or not
    private bool isEnemyCrossed(Vector2 playerPosition, Vector2 enemyPosition)
    {
        Debug.DrawLine(enemyPosition, playerPosition);
        Vector2 vectorDistance = enemyPosition - playerPosition;
        return vectorDistance.x < 0;

    }
    // TODO: set Random Enimies
    private void setRandomEnemies()
    {
        for (int i = 0; i < noOfEnimies; i++)
        {
            playerXposition += 10f;
            GameObject newEnemyGameObject = Instantiate(enemyGameObject, getRandomPosition(playerXposition), Quaternion.identity);
            newEnemyGameObject.name = "Enemy-" + i;
            setEnemyRotaion(newEnemyGameObject);
            enemyList.Add(newEnemyGameObject);
        }
    }
    // TODO: getRandomPosition
    private Vector2 getRandomPosition(float newPlayerX)
    {
        float _xAxis = Random.Range(playerXposition, newPlayerX);
        float _yAxis = Random.Range(playerPosition.y, playerPosition.y - 10);
        return new Vector2(_xAxis, _yAxis);
    }
    // TODO: get Enemy Position
    Vector2 getEnemyPosition(int indexOfEnemy)
    {
        return enemyList[indexOfEnemy].transform.position;
    }

    void setEnemyRotaion(GameObject enemyGm)
    {
        enemyGm.transform.rotation = new Quaternion(0, 0.999961913f, 0, -0.00872646365f);
    }

    void handleEnemiesSystem()
    {
        // check player crossed enemy or not
        if (indexOfCrossedEnemy < noOfEnimies && enemyList[indexOfCrossedEnemy].gameObject != null)
        {
            Debug.Log(indexOfCrossedEnemy);
            Vector2 enemyPosition = getEnemyPosition(indexOfCrossedEnemy);
            bool isEnmCrossed = isEnemyCrossed(playerPosition, enemyPosition);
            if (isEnmCrossed)
            {
                indexOfCrossedEnemy++;
                Debug.Log("Crossed " + indexOfCrossedEnemy);
            }
        }
        else
        {
            Debug.Log("Game End");
        }
    }


    // TODO: destory enemy
    private void destoyEnemyAfterCrossed()
    {

    }

}
