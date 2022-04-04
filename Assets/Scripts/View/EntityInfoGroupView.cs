using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityInfoGroupView : MonoBehaviour
{
    public EntityType type;
    public EntityPreset preset;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI textMeshProEntityAmount;
    private bool hasBeenEnabled;


    private int entityAmount;

    public void UpdateFromPreset()
    {
        hasBeenEnabled = false;
        image.sprite = preset.Sprite;
        RectTransform rectTransform = (RectTransform)image.transform;
        rectTransform.sizeDelta = new Vector2(preset.Sprite.rect.width * 1.5f, preset.Sprite.rect.height * 1.5f);
        image.gameObject.transform.localPosition = preset.SpriteHeadCenter;
    }

    public void UpdateEntityAmount(int amount)
    {
        entityAmount = amount;
        if (amount > 0) hasBeenEnabled = true;
        textMeshProEntityAmount.text = amount > 0 ? amount.ToString() : "";
        image.enabled = hasBeenEnabled;
        image.color = amount > 0 ? Color.white : Color.black;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
