using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    public Transform spawnPos;
    public GameObject chargingBulletPrefab;
    public GameObject activeBulletPrefab;
    public Transform cameraTransform;
    public AudioClip shootSound;
    public AudioSource audioSource;
    public Transform barrelTransform;
    public ParticleSystem shootParticles; // Particle system reference

    private GameObject chargingBullet;
    private float chargeTime = 0f;
    private float maxChargeTime = 2f;
    private bool isCharging = false;
    private Vector3 initialCameraPosition;
    private float bulletDirection = 1f;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            barrelTransform.localScale = new Vector3(-1f, 1f, 1f);
            bulletDirection = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            barrelTransform.localScale = new Vector3(1f, 1f, 1f);
            bulletDirection = 1f;
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
        chargingBullet = Instantiate(chargingBulletPrefab, spawnPos.position, spawnPos.rotation);
    }

    void UpdateCharging()
    {
        chargeTime += Time.deltaTime;
        chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);

        if (chargingBullet != null)
        {
            chargingBullet.transform.position = spawnPos.position;
            float chargeScale = Mathf.Lerp(1f, 2f, chargeTime / maxChargeTime);
            chargingBullet.transform.localScale = chargingBulletPrefab.transform.localScale * chargeScale;
        }
    }

    void FireChargedBullet()
    {
        if (chargingBullet != null)
        {
            Destroy(chargingBullet);
        }

        isCharging = false;

        GameObject activeBullet = Instantiate(activeBulletPrefab, spawnPos.position, spawnPos.rotation);
        Destroy(activeBullet, 5f);

        Bullet bulletScript = activeBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            float chargeMultiplier = Mathf.Lerp(1f, 2f, chargeTime / maxChargeTime);
            bulletScript.SetCharge(chargeMultiplier);
        }

        Rigidbody2D bulletRb = activeBullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = new Vector2(bulletDirection * 10f, bulletRb.linearVelocity.y);
        }

        StartCoroutine(ShakeCamera());

        // Play the shooting sound for 1 second
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
            StartCoroutine(StopAudioAfterOneSecond());
        }

        // Play the shooting particle effect
        if (shootParticles != null)
        {
            shootParticles.Play();
        }
    }

    System.Collections.IEnumerator StopAudioAfterOneSecond()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.Stop();
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

        cameraTransform.position = initialCameraPosition;
    }
}
