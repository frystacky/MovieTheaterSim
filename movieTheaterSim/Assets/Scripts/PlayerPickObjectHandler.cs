using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickObjectHandler : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private float pickUpDistance = 2f;

    [SerializeField] public bool isHoldingObject = false;

    private ObjectGrabbable objectGrabbable;

    private void Start()
    {
        //Find the child named "PlayerCamera" of the gameobject "Joint" (Joint is a child of "FirstPersonController").
        playerCameraTransform = this.transform.Find("Joint/PlayerCamera");
        //Find the child named "ObjectGrabPoint" of the gameobject "PlayerCamera" (PlayerCamera is a child of "Joint")
        objectGrabPointTransform =  this.transform.Find("Joint/PlayerCamera/ObjectGrabPoint");
    }

    private void Update()
    {
        //pick up object with objectGrabbable script attached, key is mapped in input manager
        if (Input.GetButtonDown("PickUp"))
        {
            //not carrying object, try to grab one
            if (objectGrabbable == null)
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        isHoldingObject = true;
                        objectGrabbable.Grab(objectGrabPointTransform, this.gameObject);                      
                    }
                }
            }
            else
            {
                //carrying a grabbed object, drop it
                objectGrabbable.Drop(this.gameObject);
                objectGrabbable = null;
                isHoldingObject = false;
            }
            
        }
        //throw carring object, key is mapped in input manager
        if (Input.GetButtonDown("Throw") && objectGrabbable != null)
        {
            objectGrabbable.ThrowObject(this.gameObject);
            objectGrabbable = null;
            isHoldingObject = false;
        }
    }
}
