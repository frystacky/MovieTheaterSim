using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SodaSlotHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public string sodaTypeName;
    [SerializeField] public float sodaSlotFuleLevel;

    [SerializeField] GameObject sodaBottle;
    [SerializeField] private float sodaFillPerCall = .25f;

    public bool buttonPressed;

    private void FixedUpdate()
    {
        if (buttonPressed && sodaBottle != null)
        {
            //check if we are mixing soda flavors
            if (string.IsNullOrWhiteSpace(sodaBottle.GetComponent<SodaHandler>().sodaTypeName))
            {
                sodaBottle.GetComponent<SodaHandler>().sodaTypeName = sodaTypeName;
            }
            else if (sodaBottle.GetComponent<SodaHandler>().sodaTypeName != sodaTypeName)
            {
                sodaBottle.GetComponent<SodaHandler>().sodaTypeName = "Mixed";
            }

            if (sodaSlotFuleLevel >= sodaFillPerCall)
            {
                //small soda has a max fule level of 50, fixed update is called 50 times per second
                //so if we want to fill up a small soda in 4 seconds we add .25 fule per call
                sodaBottle.GetComponent<SodaHandler>().sodaFillLevel += sodaFillPerCall;
                sodaSlotFuleLevel -= sodaFillPerCall;
            }
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //checks if soda is in soda slot and is not being held by the player
        if (other.gameObject.CompareTag("SodaBottle") && other.transform.parent == null)
        {
            Debug.Log("HELLO FROM " + other.gameObject.name);
            sodaBottle = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SodaBottle"))
        {
            //Debug.Log("GoodBye FROM " + other.gameObject.name);
            sodaBottle = null;
        }
    }

    public void SodaMachineSlotButtonPush()
    {
        Debug.Log("PUSHING BUTTON!");
                                    
    }

    //this function checks if the mouse is pressured on the button
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    

}
