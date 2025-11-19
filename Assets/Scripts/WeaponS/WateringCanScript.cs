using UnityEngine;

public class WateringCanScript : MonoBehaviour
{
    public GameObject puddlePrefab;
    public float fallDuration = 0.5f; 
    public float fallSpeed = 5f;

    private float timer;

    private void Start()
    {
        timer = fallDuration;
    }

    private void Update()
    {
        if (timer > 0f)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Instantiate(puddlePrefab, transform.position, Quaternion.identity);
                AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.wateringWeaponSpawn);
                Destroy(gameObject);
            }
        }
    }
}
