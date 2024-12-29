using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DesktopControllerHandler : MonoBehaviour
{
    public Transform mouseCenter;
    public Transform PCViewPos;
    public GameObject player;
    public float mouseSensitivity;
    public float maxXDistance = 0.5f;
    public float maxYDistance = 0.5f;
    public bool isMoving = false;

    public Quaternion orginalRot;

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

            //if off turn it on
            if (player.GetComponent<FirstPersonController>().enabled == false)
            {
                Debug.Log("in if .. " + player.GetComponent<FirstPersonController>().playerCanMove);
               
                player.GetComponent<FirstPersonController>().playerCamera.transform.localPosition = new Vector3(0, 0, 0);

                Debug.Log("pc view pos local rot is " + PCViewPos.localRotation);
                Debug.Log("pc view pos glob rot is " + PCViewPos.rotation);

                Debug.Log("players local rots is " + player.GetComponent<FirstPersonController>().playerCamera.transform.localRotation);
                player.GetComponent<FirstPersonController>().playerCamera.transform.localRotation = orginalRot;
                Debug.Log("players local rots after set is " + player.GetComponent<FirstPersonController>().playerCamera.transform.localRotation);
                player.GetComponent<FirstPersonController>().enabled = true;
                isMoving = false;
            }
            else
            {
                player.GetComponent<FirstPersonController>().enabled = false;
                Debug.Log("pc postion " + PCViewPos.transform.position);
                Debug.Log("orginalRot before set is " + orginalRot);
                orginalRot = player.GetComponent<FirstPersonController>().playerCamera.transform.localRotation;
                Debug.Log("players rot after set is " + player.GetComponent<FirstPersonController>().playerCamera.transform.localRotation);

                player.GetComponent<FirstPersonController>().transform.localRotation = PCViewPos.transform.rotation;

                isMoving = true;

                //dont ever set global and reset local.
                //when reseting rotation try and reset cameras global to players global
                // or componet reset?
            }


        }

        if(isMoving && player != null)
        {
            player.GetComponent<FirstPersonController>().playerCamera.transform.position = Vector3.MoveTowards(player.GetComponent<FirstPersonController>().playerCamera.transform.position, PCViewPos.transform.position, 3f * Time.deltaTime);
        }
        

    }
}
