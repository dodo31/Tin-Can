using UnityEngine;

public class LifeBarView : MonoBehaviour
{
    [SerializeField] private GameObject bluepart;
    private SpriteRenderer bluepartSprite;
    private float currentSize;
    [SerializeField] private float sizeTo;
    private float sizeSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        bluepartSprite = bluepart.GetComponent<SpriteRenderer>();
        currentSize = 0;
        sizeTo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBarSize();
    }

    private void UpdateBarSize()
    {
        if (currentSize > sizeTo + 0.05 || currentSize < .3)
        {
            bluepartSprite.color = Color.red;
        }
        else if (currentSize < sizeTo - 0.05 || currentSize > .7)
        {
            bluepartSprite.color = Color.green;
        }
        else
        {
            bluepartSprite.color = Color.blue;
        }
        if (currentSize < sizeTo)
        {
            currentSize += Time.deltaTime * sizeSpeed;
            if (currentSize >= sizeTo)
                currentSize = sizeTo;
        }
        if (currentSize > sizeTo)
        {
            currentSize -= Time.deltaTime * sizeSpeed;
            if (currentSize <= sizeTo)
                currentSize = sizeTo;
        }
        bluepart.transform.position = new Vector3(-.5f + currentSize / 2, 0, 0);
        bluepart.transform.localScale = new Vector3(currentSize, 1, 1);
    }

    public void UpdateSizeTo(float sizeTo)
    {
        this.sizeTo = sizeTo;
    }
}
