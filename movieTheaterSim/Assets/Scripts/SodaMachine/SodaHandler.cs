using UnityEngine;

public class SodaHandler : MonoBehaviour
{
    private Transform objectGrabPointTransform;
    private ObjectGrabbable objectGrabbable;

    [Header("Soda object configurations")]
    [SerializeField] private float sodaPlaceDistance = 3f; //The distance the grabbed object needs to to to supply items
    LayerMask layerMask; //layer needed on the object that is able to take supplys
    private Transform hitItemObjectHandler; // will be set by raycast, the object able to be supplied

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectGrabbable = GetComponent<ObjectGrabbable>();
        layerMask = LayerMask.GetMask("Soda");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        if (Input.GetButtonDown("Supply"))
        {
            this.objectGrabPointTransform = objectGrabbable.objectGrabPointTransform;
            //checks if player is holding an item and the item is able to supply objects
            if (objectGrabPointTransform != null)
            {
                //cast ray to see if you hit shelf and then if box has item try and add to self
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit raycastHit, sodaPlaceDistance, layerMask))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.green, 3f);

                    if (raycastHit.transform.TryGetComponent(out hitItemObjectHandler))
                    {
                        Debug.Log(hitItemObjectHandler.gameObject.name + " HIT2");
                        objectGrabbable.Drop(this.gameObject);
                        this.transform.position = hitItemObjectHandler.transform.position;
                        transform.localRotation = Quaternion.identity;  //resets rotations on pick up to center object
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
