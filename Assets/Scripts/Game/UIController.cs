using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public Text rockCount;

    public Text spearCount;

    public Text swordCount;

    public Slider healthSlider;

    public Slider manaSlider;

    [SerializeField]
    private Image bloodImage;
    private Color originalBloodImageColor;

    private Color equippedColor = new Color(1f, .27f, .79f);
    private Color unequippedColor = Color.white;

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
        UpdateRockCountText(GameManager.instance.player.numRocks);
        UpdateSpearCountText(GameManager.instance.player.numSpears);
        ChangeEquipIndicator(PlayerAttack.SWORD);
    }

    public void UpdateRockCountText(int numRocks)
    {
        rockCount.text = numRocks.ToString();
    }

    public void UpdateSpearCountText(int numSpears)
    {
        spearCount.text = numSpears.ToString();
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

    public void ChangeEquipIndicator(int equipped)
    {
        if(equipped == PlayerAttack.SWORD)
        {
            rockCount.color = unequippedColor;
            swordCount.color = equippedColor;
            spearCount.color = unequippedColor;
        } else if (equipped == PlayerAttack.ROCK)
        {
            rockCount.color = equippedColor;
            swordCount.color = unequippedColor;
            spearCount.color = unequippedColor;
        } else
        {
            rockCount.color = unequippedColor;
            swordCount.color = unequippedColor;
            spearCount.color = equippedColor;
        }
    }
}
