using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TunerUI : MonoBehaviour
{
    [Header("UI����")]
    [Tooltip("������ ����")]
    public Sprite activeSprite; //��� ����
    [Tooltip("���� �ȵ� ����")] //��� ����
    public Sprite inactiveSprite;
    [Tooltip("�����ܵ��� ��� �ִ� �θ� ������Ʈ�� ����")]
    public GameObject iconsParent; //IconsLayout������Ʈ

    private List<Image> chargeIcons = new List<Image>();

    [Header("Ÿ�̸� UI")]
    [Tooltip("Ÿ�̸� UI�� �����ִ� �θ� ������Ʈ")]
    public GameObject timerUIParent;
    [Tooltip("Ÿ�̸� UI �����̴�")]
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
