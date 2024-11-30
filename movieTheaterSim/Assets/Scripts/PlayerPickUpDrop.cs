using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private float pickUpDistance = 2f;

    private ObjectGrabbable objectGrabbable;

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
                    //Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        //Debug.Log(objectGrabbable);
                    }
                }
            }
            else
            {
                // carrying a grabbed object, drop it
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
