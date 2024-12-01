using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    [SerializeField] private float throwForce = 500f; //force at which the object is thrown at

    [Header("Supply object configurations")]
    [SerializeField] private bool isSupplies; //Set trun if grabbed object is able to supple items
    [SerializeField] private float supplyDistance = 1f; //The distance the grabbed object needs to to to supply items
    [SerializeField] private ItemObjectHandler thisItemObjectHandler; //link of the boxItems object
    LayerMask layerMask; //layer needed on the object that is able to take supplys
    private ItemObjectHandler hitItemObjectHandler; // will be set by raycast, the object able to be supplied

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        layerMask = LayerMask.GetMask("Suppliable");
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.transform.parent = objectGrabPointTransform; //add the picked up object as a child to ObjectGrabPoint
        Physics.IgnoreCollision(objectRigidbody.GetComponent<Collider>(), player.GetComponent<Collider>(), true); //turn off collistion with player
        transform.rotation = Quaternion.identity;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.transform.parent = null;
        Physics.IgnoreCollision(objectRigidbody.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); //trys and make sure the object drops straight down
    }

    public void ThrowObject()
    {
        //same as drop function, but adds force to object before undefining it
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.transform.parent = null;
        Physics.IgnoreCollision(objectRigidbody.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        objectRigidbody.AddForce(transform.forward * throwForce);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); //trys and make sure the object drops straight down
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation,objectGrabPointTransform.rotation, Time.deltaTime * lerpSpeed);
        } 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //checks if player is holding an item and the item is able to supply objects
            if (objectGrabPointTransform != null && isSupplies)
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
                            thisItemObjectHandler.RemoveItem();
                            hitItemObjectHandler.AddItem();
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
