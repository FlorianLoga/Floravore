using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeaponPrefabScript : MonoBehaviour
{
    public AreaWeaponScript weapon;
    private Vector3 targetSize;
    public List<IDamageableEnemy> enemiesInRange = new List<IDamageableEnemy>();

     void OnEnable()
    {
        weapon = GameObject.Find("Area Weapon").GetComponent<AreaWeaponScript>();
        targetSize = Vector3.one * weapon.stats[weapon.weaponLevel].range;
        transform.localScale = Vector3.zero;
        AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.areaWeaponSpawn);
        StartCoroutine(HandleAreaWeapon());
    }
        
     IEnumerator HandleAreaWeapon()
    {
        float range = weapon.stats[weapon.weaponLevel].range;
        float duration = weapon.stats[weapon.weaponLevel].duration;
        float cooldown = weapon.stats[weapon.weaponLevel].cooldown;
        float scaleFactor = 3f;
        targetSize = Vector3.one * range * scaleFactor;

        float growTime = cooldown * 0.1f;     // creste in 10% din cooldown
        float shrinkTime = cooldown * 0.1f;   // micsoreaza în 10% din cooldown

        float growSpeed = targetSize.x / growTime;
        float shrinkSpeed = targetSize.x / shrinkTime;

        //crestere
        while (transform.localScale.x < targetSize.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * growSpeed);
            yield return null;
        }

        //activa
        float counter = 0f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            counter -= Time.deltaTime;

            if (counter <= 0f)
            {
                counter = weapon.stats[weapon.weaponLevel].speed;
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    enemiesInRange[i].TakeDMG(weapon.stats[weapon.weaponLevel].damage);
                }
            }

            yield return null;
        }
        //micsorare
        while (transform.localScale.x > 0)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 5);
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            IDamageableEnemy enemy = collider.GetComponent<IDamageableEnemy>();
            if (!enemiesInRange.Contains(enemy))
                enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            IDamageableEnemy enemy = collider.GetComponent<IDamageableEnemy>();
            if (enemiesInRange.Contains(enemy))
                enemiesInRange.Remove(enemy);
        }
    }
}
