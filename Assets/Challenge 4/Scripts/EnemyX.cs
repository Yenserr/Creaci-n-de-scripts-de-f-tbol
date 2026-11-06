using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed; 
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private SpawnManagerX spawnManagerScript; // Referencia al script del SpawnManager

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        speed = spawnManagerScript.enemySpeed;
    }

    void Update()
    {
        if (playerGoal != null)
        {
            Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
            
            enemyRb.AddForce(lookDirection * speed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Goal")) 
        {
            Destroy(gameObject);
        }
    }
}