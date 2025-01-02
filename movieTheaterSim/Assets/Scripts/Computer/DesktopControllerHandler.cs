using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DesktopControllerHandler : MonoBehaviour
{
    [SerializeField] private Transform PCViewPos; //position of point where player gets moved to
    public GameObject player;  //TODO get dynamically later
    private bool isMoving = false;
    [SerializeField] private float anglePerSecond = 25.0f; //used to control the speed at which the camera turns at pc
    [SerializeField] public Texture2D hoverCursor; //mouse cursor icon that gets switch when viewing pc
    [SerializeField] private TextMeshProUGUI clockText;

    [Header("Button Gameobject to fetch by name")]
    [SerializeField] private GameObject pcIcons;

    [Header("App Objects")]
    [SerializeField] private GameObject wholeSaleApp;
    [SerializeField] private GameObject bankApp;
    [SerializeField] private GameObject testApp3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnityEngine.Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            //foreach (Transform childTransform in player.transform)
            //{
            //    if(childTransform.tag == "MainCamera")
            //    {
            //        Debug.Log("Find maincam!!");
            //        playersCamLocation = childTransform.gameObject;
            //    }
            //}
            
            Debug.Log("Turning off player Controller");


            if (player.GetComponent<FirstPersonController>().enabled == false)
            {
                //stops the update of moving to target
                isMoving = false;
                //resets the local pos of camera back to player, since orginal is 0.0.0 under first person controller
                player.GetComponent<FirstPersonController>().playerCamera.transform.localPosition = new Vector3(0, 0, 0);
                
                //TODO: figure out how to get playerx cursor only
                UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                //allows the first person controller to move again
                player.GetComponent<FirstPersonController>().enabled = true;
            }
            else
            {
                //turns off movement for first person controller character
                player.GetComponent<FirstPersonController>().enabled = false;
                //turns on movement of player camera to target pos              
                isMoving = true;
            }


        }

        if(isMoving && player != null)
        {
            //moves to target location smoothly over time
            player.GetComponent<FirstPersonController>().playerCamera.transform.position = Vector3.MoveTowards(player.GetComponent<FirstPersonController>().playerCamera.transform.position, PCViewPos.transform.position, 3f * Time.deltaTime);

            //Rotates to target location smoothly over time
            player.GetComponent<FirstPersonController>().playerCamera.transform.rotation = Quaternion.RotateTowards(player.GetComponent<FirstPersonController>().playerCamera.transform.rotation, PCViewPos.transform.rotation, anglePerSecond * Time.deltaTime);

            if(player.GetComponent<FirstPersonController>().playerCamera.transform.position == PCViewPos.transform.position)
            {
                UnityEngine.Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
            }
        }

        //change this later to actual game time
        clockText.text = System.DateTime.Now.ToString("h:mm tt");

    }

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
