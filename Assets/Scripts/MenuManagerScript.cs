using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider musicSlider;
    public TMP_Dropdown resolutionDropdown;
    public AudioMixer musicMixer;
    public GameObject leaderboardPanel;

    Resolution[] resolutions;

    void Start()
    {
       
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        musicSlider.onValueChanged.AddListener(SetVolume);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        if (volume <= 0.0001f)
            musicMixer.SetFloat("MusicVolume", -80f);
        else
            musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        LeaderboardScript.Instance.DisplayScores();
    }
    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }
}
