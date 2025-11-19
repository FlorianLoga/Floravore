using UnityEngine;

public class WaterDropWeaponScript : WeaponScript
{
    public GameObject wateringCanPrefab;
    public float cooldownTimer;

    private void Update()
    {
        if (!PlayerMovementScript.Instance) return;

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            DropWaterCan();
            cooldownTimer = stats[weaponLevel].cooldown;
        }
    }

    private void DropWaterCan()
    {
        Vector2 spawnPos = (Vector2)PlayerMovementScript.Instance.transform.position + Random.insideUnitCircle * 3f;
        spawnPos.y += 3f; 
        Instantiate(wateringCanPrefab, spawnPos, Quaternion.identity);
    }
}
