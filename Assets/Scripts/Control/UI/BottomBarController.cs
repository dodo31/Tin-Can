using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
	[SerializeField]
	EntityInfoGroupView entityInfoGroupPrefab;
	[SerializeField]
	GameObject entityInfoParent;
	[SerializeField]
	Button playPauseButton;
	[SerializeField]
	TextMeshProUGUI playPauseButtonText;
	[SerializeField]
	GameObject QuitButton;
	[SerializeField]
	TextMeshProUGUI newEggText;
	[SerializeField]
	EntityInfoGroupView newEggEntityInfoGroupView;
	[SerializeField]
	Button newEggButton;

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
		SpawnEntityInfoGroupViews();
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
		newEggEntityInfoGroupView.preset = preset;
		newEggEntityInfoGroupView.UpdateFromPreset();
	}

	public void SetNewEggEntityAmount(int amount)
	{
		newEggButton.gameObject.SetActive(amount > 0);
		newEggText.gameObject.SetActive(amount > 0);
		newEggEntityInfoGroupView.UpdateEntityAmount(amount);
	}
	
	public void SetPlayPauseButtonTo(bool isPaused)
	{
		playPauseButtonText.text = isPaused ? "Play" : "Pause";
	}

	// Not used (yet)
	public void SetEntityAmount(EntityType type, int amount)
	{
		if (entityInfoGroupViews.ContainsKey(type))
		{
			EntityInfoGroupView infoGroupView = entityInfoGroupViews[type];
			infoGroupView.UpdateEntityAmount(amount);
		}
	}

	public void OffsetEntityAmout(EntityType type, int offset)
	{
		if (entityInfoGroupViews.ContainsKey(type))
		{
			EntityInfoGroupView infoGroupView = entityInfoGroupViews[type];
			infoGroupView.UpdateEntityAmount(infoGroupView.EntityAmount + offset);
		}
	}

	private void SpawnEntityInfoGroupViews()
	{
		entityInfoGroupViews.Clear();
		entityTypes = new List<EntityType>();

		foreach (EntityType type in (EntityType[])EntityType.GetValues(typeof(EntityType)))
		{
			if (type != EntityType.HUMAN_1)
			{
				entityInfoGroupViews.Add(type, this.InstantiateEntityInfoGroup(type));
				entityTypes.Add(type);
			}
		}
	}

	private EntityInfoGroupView InstantiateEntityInfoGroup(EntityType type)
	{
		EntityPreset entityPreset = _presetBase[type];
		EntityInfoGroupView entityInfoGroupView = Instantiate(entityInfoGroupPrefab);

		entityInfoGroupView.type = type;
		entityInfoGroupView.preset = entityPreset;
		entityInfoGroupView.UpdateFromPreset();
		entityInfoGroupView.transform.SetParent(entityInfoParent.transform);

		return entityInfoGroupView;
	}
}
