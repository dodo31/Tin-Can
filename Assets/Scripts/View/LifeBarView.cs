using UnityEngine;

public class LifeBarView : MonoBehaviour
{
	[SerializeField]
	private GameObject bluePart;

	private SpriteRenderer bluePartSprite;
	private float currentSize;

	private float sizeTo;
	private float sizeSpeed = 5f;

	protected void Start()
	{
		bluePartSprite = bluePart.GetComponent<SpriteRenderer>();
		currentSize = 0;
		sizeTo = 0;
	}

	protected void Update()
	{
		UpdateBarSize();
	}

	private void UpdateBarSize()
	{
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
        
		bluePart.transform.localPosition = new Vector3(-.5f + currentSize / 2f, 0, 0);
		bluePart.transform.localScale = new Vector3(currentSize, 1, 1);
	}

	public void UpdateSizeTo(float sizeTo)
	{
		this.sizeTo = sizeTo;
	}
}
