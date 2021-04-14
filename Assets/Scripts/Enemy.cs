using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int enemyWorth; //how many points player will get for killing enemy, assigned in editor
    [SerializeField]
    private float enemySpeed = 3f;
    [SerializeField]
    private float enemyFireRate = 0.9f;
    private float canEnemyFire;
    [SerializeField]
    private GameObject EnemyLaserPrefab;
    [SerializeField]
    private Transform enemyContainer;
    private UIManager uIManager;
    private GameManager gameManager;
    private Player player;


    void Start()
    {
        canEnemyFire = Random.Range(1f, 9f);
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(uIManager == null)
        {
            Debug.Log("Enemy: UIManager is empty!");
        }
        if (gameManager == null)
        {
            Debug.Log("Enemy: GameManager is empty!");
        }
        if (player == null)
        {
            Debug.Log("Enemy: Player is empty!");
        }
       

    }

    void Update()
    {
        EnemyShoot();
        if(transform.position.z < -4.5)
        {
            gameManager.GameOver();
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy(); // using FixedUpdate to avoid any "glitches" related to framerate etc. 

    }


    void EnemyShoot()
    {
        if (Time.time > canEnemyFire)
        {

            enemyFireRate = Random.Range(1f, 9f);
            canEnemyFire = Time.time + enemyFireRate;
            Vector3 spawnPosition = transform.position;
            spawnPosition.z -= 1f;
            GameObject enemyLaser = Instantiate(EnemyLaserPrefab, spawnPosition, Quaternion.identity);
            enemyLaser.GetComponent<Laser>().AssignEnemyLaser();
            enemyLaser.tag = "EnemyLaser";
        }
    }


    //move enemies within visible area limits - if they reach the edge, move them closer to the player
    void MoveEnemy()
    {
        bool isGameOver = false;
        if (!isGameOver)
        {
            transform.position += Vector3.right * enemySpeed * Time.deltaTime;
            if (transform.position.x <= -13 || transform.position.x >= 13)
            {
                enemySpeed = -enemySpeed;
                transform.position += (Vector3.back * 1f);
            }
            
        }     
    }

    // if player's laser hits enemy, destroy both
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser" && other.gameObject.tag != "EnemyLaser")
        {
            
            player.AddScore(enemyWorth);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
