using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image healthBarSprite;

    public void UpdateHealth(float maxHealth, float currentHealth)
    {
        Debug.Log($"Updating health: {currentHealth}/{maxHealth}");
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}
