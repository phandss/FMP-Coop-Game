using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdateUI : MonoBehaviour
{
    private TextMeshProUGUI contentTxt;
    [SerializeField] private HumanHealth _humanHealth;

    private void Awake()
    {
        contentTxt = GetComponentInChildren<TextMeshProUGUI>();
        _humanHealth.OnHealthChanged += UpdateHealthUI;
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI(float currentHealth = -1)
    {
        if (contentTxt == null)
        {
            contentTxt = GetComponent<TextMeshProUGUI>();
        }
        if (currentHealth < 0)
        {
            currentHealth = _humanHealth.CurrentHealth;
        }
        contentTxt.text = "Health: " + currentHealth.ToString("F0") + "/" + _humanHealth.maxHealth.ToString("F0");
    }


}
