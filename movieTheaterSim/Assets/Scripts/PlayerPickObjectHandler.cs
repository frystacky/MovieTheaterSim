using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickObjectHandler : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private float pickUpDistance = 2f;

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
            Debug.Log("e was pressed and " + objectGrabbable);
            //not carrying object, try to grab one
            if (objectGrabbable == null)
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                //carrying a grabbed object, drop it
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
            
        }
        //throw carring object, key is mapped in input manager
        if (Input.GetButtonDown("Throw") && objectGrabbable != null)
        {
            objectGrabbable.ThrowObject();
            objectGrabbable = null;
        }
    }
}
