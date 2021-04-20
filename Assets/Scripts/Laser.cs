using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = 8.0f;
    private bool isEnemyLaser = false; // to check if it's laser shot from enemy
    
    void Update()
    {
        // if it's player's laser, shoot up. If it's enemy's, then go down towards the player
        if (isEnemyLaser == true)
            moveDown();
        else if (isEnemyLaser == false)
            moveUp();
    }

    void moveUp()
    {
        transform.Translate(Vector3.forward * laserSpeed * Time.deltaTime);
        if (transform.position.z > 10) // destroy object after it leaves visible area
            Destroy(transform.gameObject);
    }

    void moveDown()
    {
        transform.Translate(Vector3.back * laserSpeed * Time.deltaTime);
        if (transform.position.z < -9) // destroy object after it leaves visible area
            Destroy(transform.gameObject);
    }

    // function used to assign laser shots to enemies
    public void AssignEnemyLaser() => isEnemyLaser = true;


    //check if player was hit. If yes, apply damage.
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with: " + other.name);
        if(other.tag=="Player" && isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.Damage();
        }
        else if(other.tag=="Obstacle")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
