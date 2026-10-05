// ============================================================================
// InventoryManager.cs
// Keeps track of the items the player is carrying. There is only one in the
// scene; other scripts reach it through InventoryManager.instance.
// ============================================================================

using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Shared access point so any script can call InventoryManager.instance.AddItem(...)
    public static InventoryManager instance { get; private set; }

    [Header("Inventory")]
    [Tooltip("Items the player is holding. Shown here so you can watch it change while testing.")]
    [SerializeField] private List<string> items = new List<string>();

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Picked up: " + itemName);
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        items.Remove(itemName);
    }
}
