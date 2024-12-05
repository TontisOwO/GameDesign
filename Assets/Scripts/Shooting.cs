using UnityEditor.ShaderGraph;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform spawnPos;               // Bullet spawn position
    public GameObject chargingBulletPrefab;  // Prefab for the charging bullet
    public GameObject activeBulletPrefab;    // Prefab for the active bullet

    private GameObject chargingBullet;       // Reference to the charging bullet
    private float chargeTime = 0f;           // Time the button is held
    private float maxChargeTime = 2f;        // Time needed to reach max charge
    private bool isCharging = false;         // Whether charging is happening

    void Update()
    {
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

    void Blinking()
    {
        if (chargeTime <= maxChargeTime)
        {
            
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

        // Get the Bullet script and set the charge-based damage
        Bullet bulletScript = activeBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            float chargeMultiplier = Mathf.Lerp(1f, 2f, chargeTime / maxChargeTime);
            bulletScript.SetCharge(chargeMultiplier); // Apply charge multiplier to the active bullet
        }
    }
}
