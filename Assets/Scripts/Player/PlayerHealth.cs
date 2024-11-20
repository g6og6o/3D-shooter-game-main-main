using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public int maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthbar;
    public TextMeshProUGUI healthText;
    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;

    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, 0);
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if(health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b,tempAlpha);
            }
        }
    }
    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthbar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthbar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthbar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthbar.color = Color.green;
            backHealthbar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillB, backHealthbar.fillAmount, percentComplete);
        }
            healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;

    }


}
