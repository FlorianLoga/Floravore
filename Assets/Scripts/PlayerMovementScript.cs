using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public static PlayerMovementScript Instance;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private Animator animator;
    public Vector3 playerMoveDirection;
    public Vector3 lastMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;

    private bool isImmune;
    [SerializeField]
    private float immunityDuration;
    [SerializeField]
    private float immunityTimer;

    public int experience;
    public int currentLevel;
    public int maxLevel;
    public List<int> playerLevels;
        
    [SerializeField] private List<WeaponScript> inactiveWeapons;
    public List<WeaponScript> activeWeapons;
    [SerializeField] private List<WeaponScript> upgradeableWeapons;
    public List<WeaponScript> maxLevelWeapons;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
            Instance = this;
    }

    void Start()
    {
        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1]* 1.1f +15));
        }
        playerHealth = playerMaxHealth;
        UIController.Instance.UpdateHelthSlider();
        UIController.Instance.UpdateExperienceSlider();
        ShowInitialWeaponChoice();
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        playerMoveDirection = new Vector2(x, y).normalized;

        if (playerMoveDirection == Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else if (Time.timeScale != 0)
        {
            animator.SetBool("moving", true);
            animator.SetFloat("moveX", x);
            animator.SetFloat("moveY", y);
            lastMoveDirection = playerMoveDirection;
        }
        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        }
        else
        {
            isImmune = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(playerMoveDirection.x * movementSpeed, playerMoveDirection.y * movementSpeed) * Time.deltaTime;
    }
    public void TakeDMG(float damage)
    {
        if (!isImmune)
        {
            isImmune = true;
            immunityTimer = immunityDuration;
            playerHealth -= damage;
            AudioControllerScript.Instance.PlayModifiedSound(AudioControllerScript.Instance.hitSound);
            UIController.Instance.UpdateHelthSlider();
            if (playerHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManagerScript.Instance.GameOver();
            }
        }
    }
    public void GetExp(int expToGet)
    {
        experience += expToGet;
        UIController.Instance.UpdateExperienceSlider();
        if(experience >= playerLevels[currentLevel - 1])
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        experience -= playerLevels[currentLevel - 1];
        currentLevel++;
        UIController.Instance.UpdateExperienceSlider();
        upgradeableWeapons.Clear();

        if (activeWeapons.Count > 0)
        {
            upgradeableWeapons.AddRange(activeWeapons);
        }
        if (inactiveWeapons.Count > 0)
        {
            upgradeableWeapons.AddRange(inactiveWeapons);
        }
        for (int i = 0; i < UIController.Instance.levelUpButtons.Length; i++)
        {
            if (i < upgradeableWeapons.Count)
            {
                UIController.Instance.levelUpButtons[i].ActivateButton(upgradeableWeapons[i]);
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else if (i == UIController.Instance.levelUpButtons.Length - 1)
            {
                UIController.Instance.levelUpButtons[i].ActivateHealthUpgrade(UIController.Instance.healthUpgradeSprite);
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }
        UIController.Instance.LevelUpPanelOpen();
    }

    private void ShowInitialWeaponChoice()
    {
        upgradeableWeapons.Clear();
        upgradeableWeapons.AddRange(inactiveWeapons);

        for (int i = 0; i < UIController.Instance.levelUpButtons.Length; i++)
        {
            if (i < upgradeableWeapons.Count)
            {
                UIController.Instance.levelUpButtons[i].ActivateButton(upgradeableWeapons[i]);
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else if (i == UIController.Instance.levelUpButtons.Length - 1)
            {
                UIController.Instance.levelUpButtons[i].ActivateHealthUpgrade(UIController.Instance.healthUpgradeSprite);
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        UIController.Instance.LevelUpPanelOpen();   
    }
        
    public void ActivateWeapon(WeaponScript weapon)
    {
        if (!activeWeapons.Contains(weapon)) 
        {
            weapon.gameObject.SetActive(true); 
            activeWeapons.Add(weapon);         
            inactiveWeapons.Remove(weapon);    
        }
    }
    public void IncreaseMaxHealth(int value)
    {
        playerMaxHealth += value;
        playerHealth += value;
        UIController.Instance.UpdateHelthSlider();

        UIController.Instance.LevelUpPanelClose();
        AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.selectUpgrade);
    }
} 
