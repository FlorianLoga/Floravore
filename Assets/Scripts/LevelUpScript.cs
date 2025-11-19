using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScript : MonoBehaviour
{
    public TMP_Text weaponName;
    public TMP_Text weaponDesc;
    public Image weaponIcon;

    private WeaponScript assignedWeapon;
    private bool isHealthUpgrade = false;

    public void ActivateButton(WeaponScript weapon)
    {
        isHealthUpgrade = false;
        assignedWeapon = weapon;

        if (weapon.gameObject.activeSelf)
        {
            weaponName.text = weapon.name;
            weaponDesc.text = weapon.stats[weapon.weaponLevel].description;
        }
        else
        {
            weaponName.text = "NEW " + weapon.name;
            weaponDesc.text = weapon.basicDesc;
        }

        weaponIcon.sprite = weapon.weaponImage;
    }

    public void ActivateHealthUpgrade(Sprite healthSprite)
    {
        isHealthUpgrade = true;
        assignedWeapon = null;

        weaponName.text = "Max Health";
        weaponDesc.text = "Increase Max Health +3";
        weaponIcon.sprite = healthSprite;
    }

    public void SelectUpgrade()
    {
        if (isHealthUpgrade)
        {
            PlayerMovementScript.Instance.IncreaseMaxHealth(3);
            AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.selectUpgrade);
            UIController.Instance.LevelUpPanelClose();
            return;
        } 

        if (!assignedWeapon.gameObject.activeSelf)
        {
            PlayerMovementScript.Instance.ActivateWeapon(assignedWeapon);
        }

        assignedWeapon.LevelUp();
        AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.selectUpgrade);
        UIController.Instance.LevelUpPanelClose();
    }
}
