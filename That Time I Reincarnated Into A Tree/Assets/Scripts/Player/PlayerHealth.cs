using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Color lowHealthColor;
    [SerializeField] private Color HighHealthColor;

    //Flash
    SpriteRenderer playerSprite;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;

    int health;
    bool flashing;

    private void Start()
    {
        health = maxHealth;

        healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        healthBarImage = healthBar.fillRect.GetComponentInChildren<Image>();
        healthText = healthBar.GetComponentInChildren<TextMeshProUGUI>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthBarImage.color = HighHealthColor;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();

        playerSprite = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = playerSprite.material;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        StartCoroutine(HealthBarChanged());

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    IEnumerator HealthBarChanged()
    {
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
        healthBarImage.color = Color.white;
        playerSprite.material = flashMaterial;
        yield return new WaitForSeconds(0.1f);
        healthBarImage.color = Color.Lerp(lowHealthColor, HighHealthColor, healthBar.normalizedValue);
        healthBar.value = health;
        playerSprite.material = originalMaterial;
    }

}
