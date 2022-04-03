using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    EntityPresetBase preset;
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
    public event Action OnNewEggButtonClick;
    public event Action OnPauseButtonClick;
    public event Action OnQuitButtonClick;



    List<EntityInfoGroupView> entityInfoGroupViews;
    List<EntityType> entityTypes;

    // Start is called before the first frame update
    void Start()
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
        SetNewEggEntityPreset(EntityPresetBase.GetInstance()[type]);
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

    public void SetEntityAmount(EntityType type, int amount)
    {
        int index = entityTypes.IndexOf(type);
        if(index != -1)
            entityInfoGroupViews[index].UpdateEntityAmount(amount);
    }

    private void SpawnEntityInfoGroupViews()
    {
        preset = EntityPresetBase.GetInstance();
        entityInfoGroupViews = new List<EntityInfoGroupView>();
        entityTypes = new List<EntityType>();
        foreach (EntityType type in (EntityType[])EntityType.GetValues(typeof(EntityType)))
        {
            // TODO if entitytype != humain
            entityInfoGroupViews.Add(InstantiateEntityInfoGroup(type));
            entityTypes.Add(type);
        }
    }

    private EntityInfoGroupView InstantiateEntityInfoGroup(EntityType type)
    {
        EntityPreset entityPreset = preset[type];
        EntityInfoGroupView entityInfoGroupView = Instantiate(entityInfoGroupPrefab);

        entityInfoGroupView.type = type;
        entityInfoGroupView.preset = entityPreset;
        entityInfoGroupView.UpdateFromPreset();
        entityInfoGroupView.transform.SetParent(entityInfoParent.transform);
        return entityInfoGroupView;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
