using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    [HideInInspector] public float money;

    [SerializeField] private TextMeshProUGUI moneyText, shopMoneyText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        money = 50f;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = $"${money:0.00}";
        shopMoneyText.text = $"${money:0.00}";
    }
}
