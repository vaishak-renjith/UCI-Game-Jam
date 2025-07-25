using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<ShopItem> shopItems; // Use ShopItemData instead of GameObject
    private List<ShopItem> activeShopItems = new List<ShopItem>();
    public TextMeshProUGUI promptText;

    public bool isShopOpen = false;

    public int currency;

    void Start()
    {
        promptText.text = "dddddd";
    }

    void Update()
    {
        promptText.text = "";
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            foreach (ShopItem item in activeShopItems)
            {
                if (PromptBuy(item, player))
                    break;
            }
        }   
    }

    // set visibility, will probably have to change this
    public void CheckEnemiesCleared()
    {
        gameObject.SetActive(GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
    }

    public void DisplayShopItems(Vector2 shopCenter, float spacing)
    {
        if (isShopOpen) return;

        // Clear previous shop items
        foreach (var item in activeShopItems)
        {
            Destroy(item);
        }
        activeShopItems.Clear();

        // Pick 3 unique random ShopItemData
        HashSet<ShopItem> uniqueItems = new HashSet<ShopItem>();
        while (uniqueItems.Count < 3)
        {
            uniqueItems.Add(shopItems[Random.Range(0, shopItems.Count)]);
        }

        // Instantiate items and display in a horizontal line
        float startX = shopCenter.x - (spacing * (shopItems.Count - 1) / 2);
        foreach (ShopItem item in shopItems)
        {
            GameObject itemObj = Instantiate(item.gameObject, new Vector2(startX, shopCenter.y), Quaternion.identity, transform);
            ShopItem shopItem = itemObj.GetComponent<ShopItem>();
            activeShopItems.Add(shopItem);
            startX += spacing;
        }

        isShopOpen = true;
    }

    public void CloseShop()
    {
        foreach (var item in activeShopItems)
        {
            if (item != null)
                Destroy(item.gameObject);
        }
        activeShopItems.Clear();

        promptText.text = "";

        isShopOpen = false;
    }

    // if player is close to item, prompt to buy with E
    public bool PromptBuy(ShopItem item, GameObject player)
    {
        float distance = Vector2.Distance(item.transform.position, player.transform.position);
        if (distance < 15f)
        {
            promptText.text = $"Press E to buy {item.itemData.itemName} for {item.itemData.cost} currency";
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyItem(item);
            }
            return true; // Prompt was shown
        }
        return false; // Not in range
    }

    public void BuyItem(ShopItem item)
    {
        ShopItem shopItem = item.GetComponent<ShopItem>();
        if (shopItem != null && currency >= shopItem.itemData.cost)
        {
            currency -= shopItem.itemData.cost;
            Debug.Log($"Bought {item.name} for {shopItem.itemData.cost} currency.");
            item.gameObject.SetActive(false);
            // Add item to player's inventory or apply its effect here

        }
        else
        {
            Debug.Log("Not enough currency to buy this item.");
        }

        GameManager.Instance.StartWave();

        CloseShop();
    }
}