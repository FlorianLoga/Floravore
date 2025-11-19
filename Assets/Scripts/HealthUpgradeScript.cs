using UnityEngine;
using UnityEngine.UI;

public class HealthUpgradeButton : MonoBehaviour
{

    public void OnClickHealthUpgrade()
    {
        PlayerMovementScript.Instance.IncreaseMaxHealth(3);
        AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.selectUpgrade);
        UIController.Instance.LevelUpPanelClose();
    }
}
