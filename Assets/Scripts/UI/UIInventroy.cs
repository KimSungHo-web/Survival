using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventroy : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject inventoryWindow;
    public Transform slotPanel;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    private PlayerController contoller;
    private PlayerCondition condition;

    void Start()
    {
        contoller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;

        contoller.inventory += Toggle;
        CharacterManager.Instance.Player.additem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventroy = this;
        }
        ClearSelcetedItemWindow();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ClearSelcetedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }
    public void Toggle()
    {
        if (Isopen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    public bool Isopen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        if (data.canStack) 
        {
            ItemSlot slot = GetItemStack(data);
            if (slot !=null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }
        CharacterManager.Instance.Player.itemData = null;
    }
    void UpdateUI() 
    {
        
    }
    ItemSlot GetItemStack(ItemData data) 
    {
        return null;
    }
}
