using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Search;
using UnityEngine;

public class ItemObjectHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject> { };

    public void AddItem()
    {
        foreach (GameObject item in items)
        {
            if (item.activeSelf == false)
            {
                item.SetActive(true);
                break;
            }
        }
    }
    public void RemoveItem()
    {
        foreach (GameObject item in items)
        {
            if (item.activeSelf == true)
            {
                item.SetActive(false);
                break;
            }
        }
    }

    //helper function to see if items can be added
    public bool AbleToAddItem()
    {
        foreach (GameObject item in items)
        {
            if (item.activeSelf == false)
            {
                Debug.Log("Able to added item");
                return true;
            }
        }
        Debug.Log("NOT Able to added item");
        return false;
    }

    //helper function to see if items can be removed
    public bool AbleToRemoveItem()
    {
        foreach (GameObject item in items)
        {
            if (item.activeSelf == true)
            {
                Debug.Log("Able to remove item");
                return true;
            }
        }
        Debug.Log("NOT Able to remove item");
        return false;
    }
}
