using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Add these variables at the top of the class
    public float shakeDuration = 0.2f;       // Duration of the shake
    public float shakeMagnitude = 0.1f;      // Magnitude of the shake

    public Transform spawnPos;               // Bullet spawn position
    public GameObject chargingBulletPrefab;  // Prefab for the charging bullet
    public GameObject activeBulletPrefab;    // Prefab for the active bullet
    public Transform cameraTransform;        // Reference to the camera's transform
    public AudioClip shootSound;             // Sound to play when shooting
    public AudioSource audioSource;          // AudioSource to play the sound
    public Transform barrelTransform;        // Reference to the barrel's transform

    private GameObject chargingBullet;       // Reference to the charging bullet
    private float chargeTime = 0f;           // Time the button is held
    private float maxChargeTime = 2f;        // Time needed to reach max charge
    private bool isCharging = false;         // Whether charging is happening
    private Vector3 initialCameraPosition;   // Store initial camera position

    private float bulletDirection = 1f; // Default to right (1f for right, -1f for left)

    void Update()
    {
        // Track barrel flip
        if (Input.GetKey(KeyCode.A))
        {
            // Flip barrel to the left
            barrelTransform.localScale = new Vector3(-1f, 1f, 1f);
            bulletDirection = -1f; // Bullets go left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Flip barrel to the right
            barrelTransform.localScale = new Vector3(1f, 1f, 1f);
            bulletDirection = 1f; // Bullets go right
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCharging();
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            UpdateCharging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FireChargedBullet();
        }
    }

    void StartCharging()
    {
        isCharging = true;
        chargeTime = 0f;

        // Create the charging bullet without resetting its scale
        chargingBullet = Instantiate(chargingBulletPrefab, spawnPos.position, spawnPos.rotation);
    }

    void UpdateCharging()
    {
        chargeTime += Time.deltaTime;
        chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime); // Limit to max charge time

        // Make the charging bullet follow the spawn position
        if (chargingBullet != null)
        {
            chargingBullet.transform.position = spawnPos.position;

            // Scale the charging bullet based on charge time
            float chargeScale = Mathf.Lerp(1f, 2f, chargeTime / maxChargeTime);
            chargingBullet.transform.localScale = chargingBulletPrefab.transform.localScale * chargeScale;
        }
    }

    void FireChargedBullet()
    {
        if (chargingBullet != null)
        {
            Destroy(chargingBullet); // Remove the charging bullet
        }

        isCharging = false;

        // Instantiate the active bullet
        GameObject activeBullet = Instantiate(activeBulletPrefab, spawnPos.position, spawnPos.rotation);

        // Destroy the active bullet after 5 seconds
        Destroy(activeBullet, 5f);

        // Get the Bullet script and set the charge-based damage
        Bullet bulletScript = activeBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            float chargeMultiplier = Mathf.Lerp(1f, 2f, chargeTime / maxChargeTime);
            bulletScript.SetCharge(chargeMultiplier); // Apply charge multiplier to the active bullet
        }

        // Add movement direction to the bullet based on character's facing direction
        Rigidbody2D bulletRb = activeBullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = new Vector2(bulletDirection * 10f, bulletRb.linearVelocity.y); // Adjust speed here (10f is the speed)
        }

        // Trigger camera shake
        StartCoroutine(ShakeCamera());

        // Play the shooting sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    System.Collections.IEnumerator ShakeCamera()
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera Transform is not assigned!");
            yield break;
        }

        initialCameraPosition = cameraTransform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.position = new Vector3(initialCameraPosition.x + xOffset, initialCameraPosition.y + yOffset, initialCameraPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.position = initialCameraPosition; // Reset camera position
    }
}
