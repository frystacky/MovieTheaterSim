using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SodaSlotHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public string sodaTypeName;
    [SerializeField] public float sodaSlotFuleLevel;

    [SerializeField] GameObject sodaBottle;
    [SerializeField] private float sodaFillPerCall = .25f;

    public bool buttonPressed;
    public Slider sodaFillSlider;

    private void Start()
    {
        sodaFillSlider = this.transform.Find("../../SodaFillBarCanvas/SodaFillBar").GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        if (buttonPressed && sodaBottle != null)
        {
            //sets up the soda Fill slider
            sodaFillSlider.maxValue = sodaBottle.GetComponent<SodaHandler>().sodaSizeMaxFillLevel;
            sodaFillSlider.gameObject.SetActive(true);

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
                if(sodaBottle.GetComponent<SodaHandler>().sodaFillLevel <= sodaBottle.GetComponent<SodaHandler>().sodaSizeMaxFillLevel)
                {
                    //small soda has a max fule level of 50, fixed update is called 50 times per second
                    //so if we want to fill up a small soda in 4 seconds we add .25 fule per call
                    sodaBottle.GetComponent<SodaHandler>().sodaFillLevel += sodaFillPerCall;
                    //adds to sodaFillSlider
                    sodaFillSlider.value = sodaBottle.GetComponent<SodaHandler>().sodaFillLevel;
                }
                //soda can still be poured even if bottle is filled
                sodaSlotFuleLevel -= sodaFillPerCall;
            }
        }
        //can add else if later to waste soda
    }

    private void OnTriggerEnter(Collider other)
    {
        //checks if soda is in soda slot and is not being held by the player
        if (other.gameObject.CompareTag("SodaBottle") && other.transform.parent == null)
        {
            sodaBottle = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SodaBottle"))
        {
            sodaBottle = null;
            sodaFillSlider.gameObject.SetActive(false);
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
