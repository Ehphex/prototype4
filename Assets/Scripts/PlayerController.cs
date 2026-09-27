using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 150f;
    private Rigidbody playerRB;
    private GameObject focalPoint;

    public GameObject powerupIndicator;

    public bool hasPowerup;

    private float powerUpStrength = 15.0f;

    private InputSystem_Actions controls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controls = new InputSystem_Actions();
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    // Update is called once per frame
    private void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float forwardInput = moveInput.y;

        playerRB.AddForce(focalPoint.transform.forward * forwardInput * Time.deltaTime * playerSpeed);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRB.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
