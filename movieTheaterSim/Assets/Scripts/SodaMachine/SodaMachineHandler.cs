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
        sodaSlots = this.transform.Find("SodaMachineButtons");
        foreach (Transform sodaSlot in sodaSlots.transform.GetComponentsInChildren<Transform>())
        {
            if(sodaSlot.CompareTag(sodaTag))
            {
                sodaSlotsList.Add(sodaSlot.gameObject);
            }
        }
        foreach (GameObject slot in sodaSlotsList)
        {
            //grabs the name from SodaSlotHandler componet on the slot gameObject and sets the text of the button
            slot.transform.Find("Button/ButtonText").GetComponent<TextMeshProUGUI>().SetText(slot.GetComponent<SodaSlotHandler>().sodaName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO:
    // SET script on sodaslot now and get the name




    //TODO
    //buttons fill soda on/off
    //soda has fuel
    //soda machine has a slot for bottle

    //soda bottle is a grabbable
    //soda has a fill level
    

}
