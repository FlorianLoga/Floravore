using UnityEngine;

public class DamageNumberControllerScript : MonoBehaviour
{
    public static DamageNumberControllerScript Instance;
    public DamageNumberScript prefab;

     void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void CreateNumber(float value,Vector3 location)
    {
       DamageNumberScript damageNumber = Instantiate(prefab,location,transform.rotation,transform);
        damageNumber.SetValue(Mathf.RoundToInt(value));
     }
}
