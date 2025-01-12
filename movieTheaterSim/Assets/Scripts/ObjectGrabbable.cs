using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private GameObject player;
    private Rigidbody objectRigidbody;
    public Transform objectGrabPointTransform;
    [SerializeField] private float throwForce = 500f; //force at which the object is thrown at
    Vector3 globalScale;
    private int LayerNumber; //layer index

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        globalScale = this.transform.lossyScale;
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }
    public void Grab(Transform objectGrabPointTransform, GameObject player)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        this.player = player;
        objectRigidbody.isKinematic = true;
        objectRigidbody.transform.parent = objectGrabPointTransform; //add the picked up object as a child to ObjectGrabPoint
        this.gameObject.layer = LayerNumber;    //change the gameObject layer to the holdLayer for cam render
        Physics.IgnoreCollision(objectRigidbody.GetComponent<Collider>(), player.GetComponent<Collider>(), true); //turn off collistion with playern
        transform.localRotation = Quaternion.identity;  //resets rotations on pick up to center object
        transform.localPosition = Vector3.zero;         //reset postion on pick up to be in pick up pos
        this.player = null;
    }

    public void Drop(GameObject player)
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.isKinematic = false;
        objectRigidbody.transform.parent = null;
        Physics.IgnoreCollision(objectRigidbody.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        this.gameObject.layer = 0;  //changes the grabbed gameObject back to default layer
        this.player = null;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ThrowObject(GameObject player)
    {
        //same as drop function, but adds force to object before undefining it
        this.objectGrabPointTransform = null;   
        objectRigidbody.transform.parent = null;
        objectRigidbody.isKinematic = false;
        Physics.IgnoreCollision(objectRigidbody.GetComponent<Collider>(), player.GetComponent<Collider>(), false); //turn on collistion with playern
        this.gameObject.layer = 0;  //changes the grabbed gameObject back to default layer
        objectRigidbody.AddForce(transform.forward * throwForce); //adds throw force
        this.player = null;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        if (objectGrabPointTransform != null)
        {
            transform.localScale = new Vector3(1 / objectGrabPointTransform.transform.root.transform.localScale.x, 1 / objectGrabPointTransform.transform.root.transform.localScale.y, 1 / objectGrabPointTransform.transform.root.transform.localScale.z);
        }
    }
}
