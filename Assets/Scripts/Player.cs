using UnityEngine;

public class Player : MonoBehaviour
{
    // general setup for variables
    [SerializeField]
    public int playerLives = 3;
    [SerializeField]
    private float playerSpeed = 10f;
    [SerializeField]
    private float playerFireRate = 0.5f;
    [SerializeField]
    private float playerCanFire = -1f; // "cooldown" time between shots
    [SerializeField]
    private int score = 0;

    private UIManager uIManager;

    //set prefabs
    [SerializeField]
    private GameObject laserPrefab;


    void Start()
    {
        // reset player position
        transform.position = new Vector3(0f, 1f, -7f);
        //make sure UI Manager is on the scene
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uIManager == null)
            Debug.Log("Player: UIManager is empty!");
    }

    void Update()
    {
        playerMove();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > playerCanFire)
            ShootLaser();
    }

    // player movement and restrictions
    void playerMove()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalMove * playerSpeed * Time.deltaTime);
        //restrict player movement to visible area
        if (transform.position.x > 13.5f)
            transform.position = new Vector3(13.5f, 1, -7f);
            //Debug.Log("Player moved past > 14.5f");
        else if (transform.position.x < -13.5f)
            transform.position = new Vector3(-13.5f, 1, -7f);
            //Debug.Log("Player moved past < -14.5f");
    }

    void ShootLaser()
    {
        playerCanFire = Time.time + playerFireRate;
        Instantiate(laserPrefab, transform.position + (Vector3.forward * 2f), Quaternion.identity);
    }

    //damage player. For each hit take away one life. 
    public void Damage()
    {
        playerLives--;
        uIManager.UpdateLives(playerLives);
        if (playerLives < 1)
        {
            Destroy(this.gameObject);
            uIManager.GameOver();
        }
    }

    //update the sccore 
    public void AddScore(int Points)
    {
        score += Points;
        uIManager.UpdateScore(score);
    }
}
