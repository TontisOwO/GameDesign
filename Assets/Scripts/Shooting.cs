using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform spawnPos;   
    public GameObject bulletPrefab;

    private float chargeTime = 0f;
    private float maxChargeTime = 5f;
    private bool isCharging = false;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargeTime = 0f;
        }

        // Continue charging while mouse button is held
        if (Input.GetMouseButton(0) && isCharging)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
        }

        // Release to shoot
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            Shoot();
            isCharging = false;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet
        GameObject bulletInstance = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation);

        // Get the Bullet script and set damage based on charge time
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            float chargeMultiplier = Mathf.Lerp(1f, 2f, chargeTime / maxChargeTime); // Scales from 1x to 2x
            bulletScript.damage = Mathf.RoundToInt(bulletScript.damage * chargeMultiplier);
            Debug.Log($"Fired bullet with damage: {bulletScript.damage} (Charge: {chargeTime:F2}s)");
        }
    }
}
