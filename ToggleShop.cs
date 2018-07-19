using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ToggleShop : MonoBehaviour {

    [SerializeField]
    private GameObject shopPanel;

    private Text buttonText;

    void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
    }

    public void ToggleShopPanel()
    {
        if (shopPanel.activeInHierarchy)
        {
            shopPanel.SetActive(false);
            buttonText.text = "Open Shop";
        }
        else
        {
            shopPanel.SetActive(true);
            buttonText.text = "Close Shop";
        }
    }

}
