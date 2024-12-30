using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DesktopControllerHandler : MonoBehaviour
{
    public Transform mouseCenter;
    public Transform PCViewPos;
    public GameObject player;
    public float mouseSensitivity;
    public float maxXDistance = 0.5f;
    public float maxYDistance = 0.5f;

    private bool isMoving = false;
    //used to control the speed at which the camera turns at pc
    [SerializeField] private float anglePerSecond = 25.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Mouse.current.position.ReadValue().x;
        //float y = Mouse.current.position.ReadValue().y;
        //transform.localPosition = new Vector3(-x * mouseSensitivity,-y * mouseSensitivity, y);

        Vector3 newPos = mouseCenter.position;
        newPos.x = Mathf.Clamp(newPos.x, mouseCenter.position.x - maxXDistance, mouseCenter.position.x - maxXDistance);
        newPos.y = Mathf.Clamp(newPos.y, mouseCenter.position.y - maxXDistance, mouseCenter.position.y - maxXDistance);

        //transform.position = newPos;

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
            
            //quickly moves and rotates to the target pos
            //player.GetComponent<FirstPersonController>().playerCamera.transform.SetPositionAndRotation(PCViewPos.transform.position, PCViewPos.transform.rotation);
        }

        //TODO
        //when interaction mode, mouse movement will move computer mouse movement
        //Clamp mouse to computer screen
        //have mouse able to interact with stuff on computer screen
        //create some computer logic, maybe buy items?
        //add more protoype UI to computer, time? money? etc..
        //have logic to be able to interact with computer, it will get player info and etc so it can tell who is player 1 and 2

    }
}
