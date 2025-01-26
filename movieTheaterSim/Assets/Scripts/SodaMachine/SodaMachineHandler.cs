using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SodaMachineHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> sodaSlotsList = new List<GameObject> { }; //tracks frontend data
    [SerializeField] private Transform sodaSlots;
    [SerializeField] private string sodaTag = "SodaSlot"; //tags on Slot objects

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //finds soda slots gameObject and adds them to a list
        sodaSlots = this.transform.Find("SodaMachineButtons");
        foreach (Transform sodaSlot in sodaSlots.transform.GetComponentsInChildren<Transform>())
        {
            if(sodaSlot.CompareTag(sodaTag))
            {
                sodaSlotsList.Add(sodaSlot.gameObject);
            }
        }
        //Sets name on soda button from soda type
        foreach (GameObject slot in sodaSlotsList)
        {
            //grabs the name from SodaSlotHandler componet on the slot gameObject and sets the text of the button
            slot.transform.Find("Button/ButtonText").GetComponent<TextMeshProUGUI>().SetText(slot.transform.Find("Button").GetComponent<SodaSlotHandler>().sodaTypeName);
        }
    }
}
