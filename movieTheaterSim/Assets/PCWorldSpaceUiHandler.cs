//Attach this script to your Canvas GameObject.
//Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
//Also make sure you have an EventSystem in your hierarchy.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class PCWorldSpaceUiHandler : MonoBehaviour
{
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] DesktopControllerHandler desktopControllerHandler;
    
    [Header("Button Gameobject to fetch by name for world space UI interaction")]
    [SerializeField] private GameObject saleButtonName;
    [SerializeField] private GameObject bankButtonName;
    [SerializeField] private GameObject test3ButtonName;


    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        //Fetch the desktop contoller handler from the GameObject
        desktopControllerHandler = GetComponent<DesktopControllerHandler>();
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);

                //if(result.gameObject.name == saleButtonName.name)
                //{
                //    saleButtonName.GetComponent<Button>().onClick.Invoke();            
                //}
            }
        }
    }
}
