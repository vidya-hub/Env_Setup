using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public string enemyTagName = "Bird-Enemy";
    public string groundTagName = "Ground";
    public GameObject dashEffect;
    public float dashEffectDelay;
    public int playerHealth = 5;
    public int score = 0;
    public HealthScript healthBar;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        scoreText.text = "Score 0";
        rigidbody = this.GetComponent<Rigidbody2D>();
        circleCollider2D = this.GetComponent<CircleCollider2D>();
        playerPosition = rigidbody.transform.position;
        playerXposition = playerPosition.x;
        healthBar.setMaxHealth(playerHealth);
        dashEffect.SetActive(false);
        setRandomEnemies();
    }
    void Update()
    {
        Vector2 playerPosition = rigidbody.position;
        // handle the bird enemy system

        if (indexOfCrossedEnemy < noOfEnimies && enemyList[indexOfCrossedEnemy].gameObject != null)
        {
            Debug.Log("Crossed " + indexOfCrossedEnemy);
            Vector2 enemyPosition = getEnemyPosition(indexOfCrossedEnemy);
            bool isEnmCrossed = isEnemyCrossed(playerPosition, enemyPosition);
            if (isEnmCrossed)
            {
                indexOfCrossedEnemy++;
                scoreText.text = "Score " + indexOfCrossedEnemy;
            }
        }
    }
    private void LateUpdate()
    {
        if (playerHealth == 0)
        {
            healthBar.gameObject.SetActive(false);
            Debug.Log("Done");
            rigidbody.velocity = Vector2.zero;
            rigidbody.rotation = rotationAfterAttack;
            setBounciness(bounciNess);
            rigidbody.AddRelativeForce(fallForce);
            isAttacked = true;
            playerHealth = 5;
            jumpToGameOverScreen();

        }
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
        if (collision.gameObject.name == enemyTagName)
        {
            if (playerHealth > 0)
            {
                rigidbody.velocity = new Vector2(-5f, 0);
                playerHealth--;
                healthBar.setCurrentHealth(playerHealth);
            }

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
            newEnemyGameObject.gameObject.name = enemyTagName;
            setEnemyRotaion(newEnemyGameObject);
            enemyList.Add(newEnemyGameObject);
        }
    }
    // TODO: getRandomPosition
    private Vector2 getRandomPosition(float newPlayerX)
    {
        float _xAxis = Random.Range(playerXposition, newPlayerX);
        float _yAxis = Random.Range(playerPosition.y - 3, playerPosition.y - 7);
        return new Vector2(_xAxis, _yAxis);
    }
    // TODO: get Enemy Position
    Vector2 getEnemyPosition(int indexOfEnemy)
    {
        Debug.Log(indexOfEnemy);
        return enemyList[indexOfEnemy].transform.position;
    }

    void setEnemyRotaion(GameObject enemyGm)
    {
        enemyGm.transform.rotation = new Quaternion(0, 0.999961913f, 0, -0.00872646365f);
    }


    public void jumpToGameOverScreen()  
    {
        StartCoroutine(gamoverDelay());
        SceneManager.LoadScene("GameOverScreen");
    }

    IEnumerator gamoverDelay()
    {
        yield return new WaitForSeconds(2);
    }
    void handleEnemiesSystem()
    {
        // check player crossed enemy or not

    }
    IEnumerator stopDashEffect()
    {
        print("Started");
        yield return new WaitForSeconds(dashEffectDelay);
        print("Stop");
        dashEffect.SetActive(false);
    }
    private void showDashEffect(Vector2 collisionPosition)
    {
        dashEffect = Instantiate(dashEffect, this.transform.position, Quaternion.identity);
        dashEffect.transform.SetParent(this.transform);
        dashEffect.SetActive(true);
    }
}
