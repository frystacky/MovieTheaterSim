using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;

    public float throwForce = 500f; //force at which the object is thrown at

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
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
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); //trys and make sure the object drops stright down
    }

    public void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.transform.parent = null;
        Physics.IgnoreCollision(objectRigidbody.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        objectRigidbody.AddForce(transform.forward * throwForce);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); //trys and make sure the object drops stright down
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

}
