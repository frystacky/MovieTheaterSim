using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ItemObjectHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemSlot = new List<GameObject> { }; //tracks frontend data
    [SerializeField] public Stack<GameObject> stack = new Stack<GameObject>();     //tracks backend data
    [SerializeField] private int maxItemSize;
   
    private void Awake()
    {
        //set the size of the premade list to know how many objects were added
        maxItemSize = itemSlot.Count;

        //add items to referance to it in the backend
        foreach (var item in itemSlot)
        {
            //get item child and add it to stack
            if (item != null && item.gameObject.transform.childCount > 0)
            {
                Debug.Log(item.transform.GetChild(0).gameObject);
                stack.Push(item.transform.GetChild(0).gameObject);
            }
        }
    }

    public void AddItem(GameObject objectToAdd)
    {       
        //get the size of stack to know where we can place object in list
        int itemSize = stack.Count;

        //makes sure that we cannot add more items than the size of objects inventory
        if (itemSize < maxItemSize) //TODO: check for type later
        {

            objectToAdd.transform.parent = itemSlot[stack.Count].transform;   //add to the next empty element
            stack.Push(objectToAdd);
            Debug.Log("IN HERE2");
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
