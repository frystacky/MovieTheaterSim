using TMPro;
using UnityEngine;

public class PlayerObjectInteractionHandler : MonoBehaviour
{
    [SerializeField] private Transform playerViewPos; //position of point where player gets moved to
    [SerializeField] private float anglePerSecond = 25.0f; //used to control the speed at which the camera turns at pc
    [SerializeField] private float camMoveSpeed = 3f; //rate at which the cam moves to the object

    private GameObject player; //the interacting player object
    private bool isCameraMoving = false;

    private bool canInteract = false;
    private bool isInteractionLocked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerViewPos = this.transform.Find("../PlayerViewPos"); //make sure you name the empty gameobject "PlayerViewPos" and you place it in the same hierarchy
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Interaction") && canInteract && player != null)
        {
            if (player.GetComponent<FirstPersonController>().enabled == false)
            {
                isInteractionLocked = false; //unlock the interaction so that we can only get one players info at a time
                //stops the update of moving to target
                isCameraMoving = false;
                //resets the local pos of camera back to player, since orginal is 0.0.0 under first person controller
                player.GetComponent<FirstPersonController>().playerCamera.transform.localPosition = new Vector3(0, 0, 0);

                //allows the first person controller to move again
                Cursor.lockState = CursorLockMode.Locked;
                player.GetComponent<FirstPersonController>().enabled = true;
            }
            else
            {
                isInteractionLocked = true; //locks the interaction so that we can only get one players info at a time
                //turns off movement for first person controller character
                Cursor.lockState = CursorLockMode.Confined;
                player.GetComponent<FirstPersonController>().enabled = false;
                //turns on movement of player camera to target pos              
                isCameraMoving = true;
            }
        }

        if (isCameraMoving && player != null)
        {
            //moves to target location smoothly over time
            player.GetComponent<FirstPersonController>().playerCamera.transform.position = Vector3.MoveTowards(player.GetComponent<FirstPersonController>().playerCamera.transform.position, playerViewPos.transform.position, camMoveSpeed * Time.deltaTime);

            //Rotates to target location smoothly over time
            player.GetComponent<FirstPersonController>().playerCamera.transform.rotation = Quaternion.RotateTowards(player.GetComponent<FirstPersonController>().playerCamera.transform.rotation, playerViewPos.transform.rotation, anglePerSecond * Time.deltaTime);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!other.GetComponent<PlayerPickObjectHandler>().isHoldingObject && !isInteractionLocked) 
            {
                canInteract = true;
                player = other.gameObject;
            }         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //makes sure the interacting object is a player and only 1 can interact with the object at a time
        if (other.gameObject.CompareTag("Player") && !isInteractionLocked)
        {
            canInteract = false;
            player = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerPickObjectHandler>().isHoldingObject && !isInteractionLocked)
            {
                canInteract = true;
                player = other.gameObject;
            }
        }

    }
}
