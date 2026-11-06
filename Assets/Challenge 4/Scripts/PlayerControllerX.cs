using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; 
    private float powerupStrength = 25; 

    public ParticleSystem turboParticles; 
    public float turboForce = 10.0f;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 
if (Input.GetKeyDown(KeyCode.Space))
        {
        
            playerRb.AddForce(focalPoint.transform.forward * turboForce, ForceMode.Impulse);
         
            if (turboParticles != null)
            {
                turboParticles.Play();
            }
        }
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    
turboParticles.transform.position = transform.position;
    }

  private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Powerup"))
    {
        Destroy(other.gameObject); 
        
        hasPowerup = true;
        powerupIndicator.SetActive(true);

        StopCoroutine("PowerupCoolDown");
        StartCoroutine(PowerupCoolDown()); 
    }
}

IEnumerator PowerupCoolDown()
{
    Debug.Log("Corrutina iniciada. Esperando " + powerUpDuration + " segundos...");
    yield return new WaitForSeconds(powerUpDuration);
    
    hasPowerup = false;
    
    if (powerupIndicator != null)
    {
        powerupIndicator.SetActive(false);
        Debug.Log("Indicador desactivado");
    }
}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position).normalized;
            
            if (hasPowerup)
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}