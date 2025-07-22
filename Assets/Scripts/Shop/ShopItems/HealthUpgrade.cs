using UnityEngine;

public class HealthUpgrade : ShopItem
{
    public override void ApplyEffect()
    {
        // Apply the fire speed upgrade effect here
        Debug.Log("Health upgraded!");
    }
}
