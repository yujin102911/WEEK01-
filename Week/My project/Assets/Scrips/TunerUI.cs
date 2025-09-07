using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TunerUI : MonoBehaviour
{
    [Header("UI연결")]
    [Tooltip("충전된 번개")]
    public Sprite activeSprite; //노란 번개
    [Tooltip("충전 안된 번개")] //흑백 번개
    public Sprite inactiveSprite;
    [Tooltip("아이콘들을 담고 있는 부모 오브젝트를 연결")]
    public GameObject iconsParent; //IconsLayout오브젝트

    private List<Image> chargeIcons = new List<Image>();

    [Header("타이머 UI")]
    [Tooltip("타이머 UI를 갖고있는 부모 오브젝트")]
    public GameObject timerUIParent;
    [Tooltip("타이머 UI 슬라이더")]
    public Slider timeSlider;
    


    private void Start()
    {
        foreach(Image icon in iconsParent.GetComponentsInChildren<Image>())
        {
            chargeIcons.Add(icon);
        }

        if(timerUIParent != null) timerUIParent.SetActive(false);
        if (timeSlider != null) timeSlider.maxValue = TunerManager.Instance.tunerDuration;

    }

    private void Update()
    {
        UpdateChargeUI(TunerManager.Instance.currentCharges);
        HandleTimerUI(TunerManager.Instance.isTunerActive);

    }

    void UpdateChargeUI(int currentCharges)
    {
        for(int i = 0; i < chargeIcons.Count; i++)
        {
            if(i < currentCharges)
            {
                chargeIcons[i].sprite = activeSprite;

            }
            else
            {
                chargeIcons[i].sprite = inactiveSprite;
            }
        }
    }

    void HandleTimerUI(bool isTunerOn)
    {
        if (timerUIParent == null) return;

        if (isTunerOn)
        {
            timerUIParent.SetActive(true);

            float currentTime = TunerManager.Instance.currentTime;
            if(timeSlider != null) timeSlider.value = currentTime;

        }
        else
        {
            timerUIParent.SetActive(false) ;
        }

    }

}
