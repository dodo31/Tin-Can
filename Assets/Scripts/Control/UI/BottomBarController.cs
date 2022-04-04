using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
	public SpawnEggsButton SpawnEggsButton;	
	public EggPreview EggPreview;

	[SerializeField]
	EntityInfoGroupView entityInfoGroupPrefab;
	[SerializeField]
	GameObject entityInfoParent;
	[SerializeField]

	private EntityPresetBase _presetBase;
	private EggsSequence _eggsSequence;

	private Dictionary<EntityType, EntityInfoGroupView> entityInfoGroupViews;
	private List<EntityType> entityTypes;

	public event Action OnNewEggButtonClick;
	public event Action OnPauseButtonClick;
	public event Action OnQuitButtonClick;

	protected void Awake()
	{
		_presetBase = EntityPresetBase.GetInstance();
		_eggsSequence = EggsSequence.GetInstance();

		entityInfoGroupViews = new Dictionary<EntityType, EntityInfoGroupView>();
	}

	protected void Start()
	{
		this.PopulationInfoGroupViews();
		
		SpawnEggsButton.SetInactive();
		EggPreview.SetInactive();
	}

	public void ClickPauseButton()
	{
		OnPauseButtonClick?.Invoke();
	}

	public void ClickNewEggButton()
	{
		OnNewEggButtonClick?.Invoke();
	}

	public void ClickQuitButton()
	{
		OnQuitButtonClick?.Invoke();
	}

	public void SetNewEggEntityPreset(EntityType type)
	{
		EntityPreset preset = _presetBase[type];
		this.SetNewEggEntityPreset(preset);
	}

	public void SetNewEggEntityPreset(EntityPreset preset)
	{
		EggPreview.SetEgg(preset);
		EggPreview.SetActive();
		// newEggEntityInfoGroupView.UpdateFromPreset(preset);
	}

	public void SetNewEggEntityAmount(int amount)
	{
		if (amount > 0)
		{
			SpawnEggsButton.SetActive();
			EggPreview.SetActive();
		}
		else
		{
			SpawnEggsButton.SetInactive();
			EggPreview.SetInactive();
		}

		EggPreview.SetEggAmount(amount);

		// newEggButton.gameObject.SetActive(amount > 0);
		// newEggText.gameObject.SetActive(amount > 0);
		// newEggEntityInfoGroupView.UpdateEntityAmount(amount);
	}

	public void OffsetEntityAmout(EntityType type, int offset)
	{
		if (entityInfoGroupViews.ContainsKey(type))
		{
			EntityInfoGroupView infoGroupView = entityInfoGroupViews[type];
			infoGroupView.UpdateEntityAmount(infoGroupView.EntityAmount + offset);
		}
	}

	private void PopulationInfoGroupViews()
	{
		entityInfoGroupViews.Clear();
		entityTypes = new List<EntityType>();

		foreach (EntityType type in (EntityType[])EntityType.GetValues(typeof(EntityType)))
		{
			if (type != EntityType.HUMAN_1)
			{
				entityInfoGroupViews.Add(type, this.InstantiatePopulationInfoGroup(type));
				entityTypes.Add(type);
			}
		}
	}

	private EntityInfoGroupView InstantiatePopulationInfoGroup(EntityType type)
	{
		EntityPreset entityPreset = _presetBase[type];
		EntityInfoGroupView entityInfoGroupView = Instantiate(entityInfoGroupPrefab);

		entityInfoGroupView.type = type;
		entityInfoGroupView.UpdateFromPreset(entityPreset);
		entityInfoGroupView.transform.SetParent(entityInfoParent.transform);

		return entityInfoGroupView;
	}
}
