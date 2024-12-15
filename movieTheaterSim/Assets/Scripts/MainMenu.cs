using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resultionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutionList;
    private UnityEngine.RefreshRate currentRefreshRate;
    private int currentResolutionIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions;

        filteredResolutionList = new List<Resolution>();

        resultionDropdown.ClearOptions();

        currentRefreshRate = Screen.currentResolution.refreshRateRatio;

        foreach (Resolution resolution in resolutions)
        {
            filteredResolutionList.Add(resolution);
        }

        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutionList.Count; i++)
        {
            {
                string resolutionOption = filteredResolutionList[i].width + "x" + filteredResolutionList[i].height + " " + filteredResolutionList[i].refreshRateRatio + " Hz";
                options.Add(resolutionOption);

                if (filteredResolutionList[i].width == Screen.width && filteredResolutionList[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }

        resultionDropdown.AddOptions(options);
        resultionDropdown.value = currentResolutionIndex;
        resultionDropdown.RefreshShownValue();
        resultionDropdown.itemText.fontSize = 12;
    }

    public void SetResolution(int resolutionIndex)
    {
        var fullScreen = FullScreenMode.FullScreenWindow;
        Resolution resolution = filteredResolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullScreen, resolution.refreshRateRatio);
    }

    public void SetWindowMode(int modeIndex)
    {
        // Map dropdown index to FullScreenMode
        switch (modeIndex)
        {
            case 0: // Fullscreen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Debug.Log("Switched to Fullscreen");
                break;

            case 1: // Windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Debug.Log("Switched to Windowed");
                break;

            case 2: // Windowed Fullscreen
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                Debug.Log("Switched to Windowed Fullscreen");
                break;

            default:
                Debug.LogWarning("Invalid window mode index");
                break;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Quit the Game");
    }
}
