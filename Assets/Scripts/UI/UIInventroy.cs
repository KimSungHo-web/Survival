using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class UIInventroy : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

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

    ItemData selectItem;
    int selectedItemIndex = 0;

    void Start()
    {
        contoller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

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

        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null) 
        {
            emptySlot.item=data;
            emptySlot.quantity=1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData=null;
            return;
        }

        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }
    void UpdateUI() 
    {
        for (int i = 0; i < slots.Length; i++) 
        {
            if (slots[i].item != null) 
            {
                slots[i].Set();
            }
            else 
            {
                slots[i].Clear();
            }
        }
    }
    ItemSlot GetItemStack(ItemData data) 
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity<data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    void ThrowItem(ItemData data) 
    {
        Instantiate(data.dropPrefab, dropPosition.position,Quaternion.Euler(Vector3.one*Random.value*300));
    }

    public void SelectIgem(int index) 
    {
        if (slots[index].item == null) return;

        selectItem =slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text=selectItem.displayName;
        selectedItemDescription.text = selectItem.descripttion;

        selectedStatName.text=string.Empty;
        selectedStatValue.text=string.Empty;

        for (int i = 0; i < selectItem.consumables.Length; i++) 
        {
            selectedStatName.text += selectItem.consumables[i].type.ToString()+"\n";
            selectedStatValue.text += selectItem.consumables[i].value.ToString() + "\n";
        }
        useButton.SetActive(selectItem.type==ItemType.Consumable);
        equipButton.SetActive(selectItem.type == ItemType.Equipable && !slots[index].equipped);
        unequipButton.SetActive(selectItem.type ==ItemType.Equipable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void OnUseButton() 
    {
        if (selectItem.type == ItemType.Consumable) 
        {
            for (int i = 0; i < selectItem.consumables.Length; i++) 
            {
                switch (selectItem.consumables[i].type) 
                {
                    case ConsumableType.Health:
                        condition.Heal(selectItem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectItem.consumables[i].value);
                        break;
                }
            }
            RemoveSelecedItem();
        }
    }

    public void OnDrop()
    {
        ThrowItem(selectItem);
        RemoveSelecedItem();
    }

    void RemoveSelecedItem() 
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0) 
        {
            selectItem=null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex= -1;
            ClearSelcetedItemWindow();
        }
        UpdateUI();
    }
}
