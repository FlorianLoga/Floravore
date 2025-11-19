using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
   public static UIController Instance;
    [SerializeField]
    private Slider playerHealthSlider;
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private Slider playerExperienceSlider;
    [SerializeField]
    private TMP_Text experienceText;
    public GameObject gameOverPanel;
    public GameObject levelUpPanel;
    public GameObject pausePanel;
    [SerializeField]
    private TMP_Text timerText;
    public LevelUpScript[] levelUpButtons;
    public TMP_InputField playerNameInput;
    public Sprite healthUpgradeSprite;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
            Instance = this;
    }

    public void UpdateHelthSlider()
    {
        playerHealthSlider.maxValue = PlayerMovementScript.Instance.playerMaxHealth;
        playerHealthSlider.value = PlayerMovementScript.Instance.playerHealth;
        healthText.text = playerHealthSlider.value + " / " + playerHealthSlider.maxValue;
    }

    public void UpdateExperienceSlider()
    {
        playerExperienceSlider.maxValue = PlayerMovementScript.Instance.playerLevels[PlayerMovementScript.Instance.currentLevel-1];
        playerExperienceSlider.value = PlayerMovementScript.Instance.experience;
        experienceText.text = playerExperienceSlider.value + " / " + playerExperienceSlider.maxValue;
    }
    public void UpdateTimer(float timer)
    {
        float min = Mathf.FloorToInt(timer / 60f);
        float sec=Mathf.FloorToInt(timer % 60);
        timerText.text = min + ":" + sec.ToString("00");
    }

    public void LevelUpPanelOpen()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LevelUpPanelClose()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnSubmitScore()
    {
        string playerName = playerNameInput.text;
        if (string.IsNullOrWhiteSpace(playerName))
            playerName = "Player"; 

        GameManagerScript.Instance.SaveScore(playerName);
    }
}
