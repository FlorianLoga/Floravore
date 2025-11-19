using UnityEngine;

public class AreaWeaponScript : WeaponScript
{
    [SerializeField] private GameObject prefab;
    private float spawnCounter;
    private GameObject areaInstance;

    void Start()
    {
        areaInstance = Instantiate(prefab, transform.position, transform.rotation, transform);
        areaInstance.SetActive(false);
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].cooldown;

            areaInstance.transform.position = transform.position;
            areaInstance.SetActive(true);
        }
    }
}
