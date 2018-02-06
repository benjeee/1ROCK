using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public Text rockCount;

    public Slider healthSlider;

    public Slider manaSlider;

    [SerializeField]
    private Image bloodImage;
    private Color originalBloodImageColor;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 UIController instantiated");
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        bloodImage.enabled = false;
        originalBloodImageColor = new Color(bloodImage.color.r, bloodImage.color.g, bloodImage.color.b, bloodImage.color.a);
    }

    public void UpdateRockCountText(int numRocks)
    {
        rockCount.text = "YOU HAVE " + numRocks + " ROCKS";
    }

    public void UpdateHealthSlider(int health)
    {
        healthSlider.value = health;
    }

    public void UpdateManaSlider(int mana)
    {
        manaSlider.value = mana;
    }

    public void ShowBlood(float numSeconds)
    {
        bloodImage.enabled = true;
        Color c = bloodImage.color;
        StartCoroutine(FadeBlood(numSeconds, c.a));
    }

    IEnumerator FadeBlood(float numSeconds, float alpha)
    {
        for(int i = 1; i <= 20; i++)
        {
            bloodImage.color = new Color(bloodImage.color.r, bloodImage.color.g, bloodImage.color.b, alpha / i);
            yield return null;
        }
        bloodImage.enabled = false;
        bloodImage.color = originalBloodImageColor;
    }
}
