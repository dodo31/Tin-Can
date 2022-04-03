using UnityEngine;

public class LifeBarView : MonoBehaviour
{
    [SerializeField]
    private GameObject bluePart;
    [SerializeField]
    private GameObject background;

    private SpriteRenderer bluePartSprite;
    private SpriteRenderer backgroundSprite;
    private float currentSize;

    private float sizeTo;
    private float sizeSpeed = 5f;

    private float timeSinceLastUpdate;

    protected void Awake()
    {
        bluePartSprite = bluePart.GetComponent<SpriteRenderer>();
        backgroundSprite = background.GetComponent<SpriteRenderer>();
        currentSize = 0;
        sizeTo = 0;
    }

    protected void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate > 0.1)
        {
            UpdateBarSize();
        }
    }

    private void UpdateBarSize()
    {
        bluePartSprite.enabled = currentSize < 1;
        backgroundSprite.enabled = currentSize < 1;
        if (currentSize > sizeTo + 0.05 || currentSize < .3)
        {
            bluePartSprite.color = Color.red;
        }
        else if (currentSize < sizeTo - 0.05 || currentSize > .7)
        {
            bluePartSprite.color = Color.green;
        }
        else
        {
            bluePartSprite.color = Color.blue;
        }

        if (currentSize < sizeTo)
        {
            currentSize += timeSinceLastUpdate * sizeSpeed;
            if (currentSize >= sizeTo)
                currentSize = sizeTo;
        }

        if (currentSize > sizeTo)
        {
            currentSize -= timeSinceLastUpdate * sizeSpeed;
            if (currentSize <= sizeTo)
                currentSize = sizeTo;
        }

        bluePart.transform.localPosition = new Vector3(-.5f + currentSize / 2f, 0, 0);
        bluePart.transform.localScale = new Vector3(currentSize, 1, 1);
        timeSinceLastUpdate = 0;
    }

    public void UpdateSizeTo(float sizeTo)
    {
        this.sizeTo = sizeTo;
        UpdateBarSize();
    }
}
