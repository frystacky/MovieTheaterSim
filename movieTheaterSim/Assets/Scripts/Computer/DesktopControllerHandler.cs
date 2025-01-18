using TMPro;
using UnityEngine;

public class DesktopControllerHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText; //clock text in taskbar - TODO: set to gameTime player

    [Header("Button Gameobject to fetch by name")]
    [SerializeField] private GameObject pcIcons;

    [Header("App Objects")]
    [SerializeField] private GameObject wholeSaleApp;
    [SerializeField] private GameObject bankApp;
    [SerializeField] private GameObject testApp3;

    private void Start()
    {
        clockText = this.transform.Find("TaskBarBackground/TaskBarTime_(TMP)").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        //change this later to actual game time
        clockText.text = System.DateTime.Now.ToString("h:mm tt");
    }

    /// |*************************| UI LOGIC |*************************|

    private void ClickOnIcon()
    {
        pcIcons.SetActive(false);
    }

    public void WholeSaleAppIconOnClick()
    {
        ClickOnIcon();
        wholeSaleApp.SetActive(true);
    }

    public void BankAppIconOnClick()
    {
        ClickOnIcon();
        bankApp.SetActive(true);
    }

    public void TestApp3OnClick()
    {
        ClickOnIcon();
        testApp3.SetActive(true);
    }

    public void AppsXButtonOnClick()
    {
        if(wholeSaleApp.activeSelf)
        {
            wholeSaleApp.SetActive(false);
        }
        else if(bankApp.activeSelf) 
        {
            bankApp.SetActive(false);
        }
        else if (testApp3.activeSelf)
        {  
            testApp3.SetActive(false);
        }

        pcIcons.SetActive(true);
    }
}
