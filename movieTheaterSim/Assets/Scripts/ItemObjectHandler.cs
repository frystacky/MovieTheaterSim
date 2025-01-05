using System.Collections.Generic;
using UnityEngine;

public class ItemObjectHandler : MonoBehaviour
{
    [Header("Inventory slot where items will be moved. move Item Slot HERE")]
    [SerializeField] private List<GameObject> itemSlot = new List<GameObject> { }; //tracks frontend data
    [SerializeField] public Stack<GameObject> stack = new Stack<GameObject>();     //tracks backend data
    [Header("Inventory max size, set by child Item Slot# in list")]
    [SerializeField] private int maxItemSize;   //max inventory size of box
    [Header("Item Type In Inventory if Inv has items on load")]
    [SerializeField] private string itemsInBox;    //the items that come in box
   
    private void Awake()
    {
        //set the size of the premade list to know how many objects were added
        maxItemSize = itemSlot.Count;

        //populates the stack if the object such as a box comes with items on start up
        foreach (GameObject item in itemSlot)
        {
            //get item child and add it to stack
            if (item != null && item.gameObject.transform.childCount > 0)
            {
                item.gameObject.transform.GetComponentInChildren<ItemInfo>().setItemType(itemsInBox); //sets item type in items
                stack.Push(item.transform.GetChild(0).gameObject);
            }
        }
    }

    public void AddItem(GameObject objectToAdd)
    {       
        //get the size of stack to know where we can place object in list
        int itemSize = stack.Count;

        //makes sure that we cannot add more items than the size of objects inventory
        if (itemSize > 0)
        {
            GameObject gameObject = stack.Peek();
            //check if object trying to add is the same type as items in stack
            if (gameObject.GetComponent<ItemInfo>().getItemType() == objectToAdd.GetComponent<ItemInfo>().getItemType())
            {
                objectToAdd.transform.parent = itemSlot[stack.Count].transform;   //add to the next empty element
                stack.Push(objectToAdd);
            }
        }
        else
        {
            objectToAdd.transform.parent = itemSlot[stack.Count].transform;   //add to the next empty element
            stack.Push(objectToAdd);
        }
    }

    public GameObject RemoveItem()
    {
        GameObject gameObject = stack.Peek();
        stack.Pop();

        return gameObject;
    }

    //helper function to see if items can be added
    public bool AbleToAddItem()
    {
        if(stack.Count <= maxItemSize)
        {
            return true;
        }

        return false;
    }

    //helper function to see if items can be removed
    public bool AbleToRemoveItem()
    {
        if(stack.Count > 0)
        {
            return true;
        }
        return false;
    }

}
