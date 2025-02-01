using UnityEngine;

public class SuppliableHandler : MonoBehaviour
{
    private Transform objectGrabPointTransform;
    private ObjectGrabbable objectGrabbable;


    [Header("Supply object configurations")]
    [SerializeField] private float supplyDistance = 1f; //The distance the grabbed object needs to to to supply items
    private ItemObjectHandler thisItemObjectHandler; //link of the boxItems object child component
    [SerializeField] private LayerMask layerMask; //layer needed on the object that is able to take supplys
    private ItemObjectHandler hitItemObjectHandler; // will be set by raycast, the object able to be supplied

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectGrabbable = GetComponent<ObjectGrabbable>();
        layerMask = LayerMask.GetMask("Suppliable");
        this.thisItemObjectHandler = GetComponentInChildren<ItemObjectHandler>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Supply"))
        {
            this.objectGrabPointTransform = objectGrabbable.objectGrabPointTransform;
            //checks if player is holding an item and the item is able to supply objects
            if (objectGrabPointTransform != null)
            {
                //cast ray to see if you hit shelf and then if box has item try and add to self
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit raycastHit, supplyDistance, layerMask))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.green, 3f);
                    Debug.Log("Did Hit");
                    if (raycastHit.transform.TryGetComponent(out hitItemObjectHandler))
                    {
                        //need if item can be removed from grabbable supplier and item can be added
                        bool canAddShelfItem = hitItemObjectHandler.AbleToAddItem();
                        bool canRemoveBoxItem = thisItemObjectHandler.AbleToRemoveItem();

                        if (canAddShelfItem && canRemoveBoxItem)
                        {
                            hitItemObjectHandler.AddItem(thisItemObjectHandler.RemoveItem());
                            this.hitItemObjectHandler = null;
                        }
                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red, 3f);
                    Debug.Log("Did not Hit");
                }
            }
        }
    }
}
