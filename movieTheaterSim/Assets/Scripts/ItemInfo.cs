using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] public float sellPrice {get; private set;}
    [SerializeField] public string itemType;

    public void setItemType(string itemType)
    {
        this.itemType = itemType;
    }

    public string getItemType()
    {
        return this.itemType;
    }
}
