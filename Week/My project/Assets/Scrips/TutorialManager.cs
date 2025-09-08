using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [Header("���� ���")]
    public PlayerMove playerMove;
    public GameObject tutorialPanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public GameObject tunerObject;

    [Header("����ǥ ����")]
    [Tooltip("������ ��� ����ǥ���� ���⿡ ����ϼ���")]
    public List<IndicatorController> indicators;

    private int tutorialStep = 0;

    private void Start()
    {
        tunerObject.SetActive(false);

        foreach (var indicator in indicators) { indicator.Deactivate(); }

        StartTutorial();

    }


    private void Update()
    {   //��ȭâ, "����"�� �����ְ� �����̽��ٸ� �Է� ������
        if(tutorialPanel.activeSelf && Input.GetButtonDown("Jump") && nextButton.gameObject.activeSelf)
        {
            //���� ��ư�� ����� ��� ����
            nextButton.onClick.Invoke();
        }
    }

    void StartTutorial()
    {
        playerMove.canMove = false;
        tutorialStep = 1;
        ShowTutorialStep(tutorialStep);

    }

    //���� ��ȭ�� �Ѿ�� �Լ�
    public void GoToNextDialogue()
    {
        tutorialStep++;
        ShowTutorialStep(tutorialStep);

    }

    //��ȭâ�� �ݰ� player���� ���� ������ �ִ� �Լ�
    public void CloseDialogueAndResumePlay()
    {
        tutorialPanel.SetActive(false);
        //playerMove.canMove = true;

        if (TunerManager.Instance.isTunerActive) TunerManager.Instance.ResumeTimer();

        int nextStep = tutorialStep + 1;
        UpdateIndicators(nextStep);

        StartCoroutine(EnableMovementAfterFrame());

    }

    private IEnumerator EnableMovementAfterFrame()
    {
        yield return null;

        playerMove.canMove = true;
    }

    void ShowTutorialStep(int step)
    {
        //��ư�� ���� ������ ��� �����ؼ� �ߺ� ������ ����
        nextButton.onClick.RemoveAllListeners();
        tutorialPanel.SetActive(true);
        nextButton.gameObject.SetActive(true);

        //UpdateIndicators(step);

        switch(step)
        {
            case 1: //�������ڸ��� ����
                dialogueText.text = "��.. ���� ������. ���� ���� ������?";
                nextButton.onClick.AddListener(GoToNextDialogue); //���� ��ȭ �ε�
                break;

            case 2:
                dialogueText.text = "���? ������ ���� �� ������, �� ���� �ȿ�����?";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay); //���� ������ Player Turn�� �� �� �ְ�
                break;

            case 3: //Player�� Trigger������ �����ϸ� (����ǥ �� �� ��)
                playerMove.canMove = false;
                dialogueText.text = "(ö��ö��) ���� �ȿ�����. ����ǿ� ���� ������߰ڴ�.";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 4: //Player�� Trigger������ �����ϸ� (���� ���������� ���� �� ��)
                playerMove.canMove = false;
                tunerObject.SetActive(true); //�������� tuner �� �� ����
                dialogueText.text = "(ö��ö��) ���⵵ �ȿ�����? ���� �ٸ� ����� ã�ƺ��߰ڴ°�.";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 5: //Player�� Trigger������ �����ϸ� (���ļ� ������ ���� ��)
                playerMove.canMove = false;
                dialogueText.text = "�̰� ����? ���� �����ȰͰ��� ����.";
                nextButton.onClick.AddListener(GoToNextDialogue);
                break;

            case 6:
                dialogueText.text = "��ư�� �ϳ� ���δ�. ������? \n<color=yellow>[Q]�� ���� ���ļ� �����⸦ Ȱ��ȭ�� �� �ֽ��ϴ�.</color>";
                tunerObject.SetActive(false);
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 7: //Player�� Q�� ���� ������ Ȯ���ؼ� ������ ���� �� ����
                playerMove.canMove = false;
                TunerManager.Instance.PauseTimer();
                dialogueText.text = "���� �� ���? ��ư�� ������ ���� �ٸ� �������� �� �� ����.\n�ٵ� ������ ���ȴµ� �� �ƹ� ������ ����? �ٽ� ��ư�� ��������?\n<color=yellow>[Q]�� �ٽ� ������ ���ļ� �����Ⱑ ��Ȱ��ȭ �˴ϴ�.</color>";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 8:
                playerMove.canMove = false;
                dialogueText.text = "���� ��������� �� �𸣰�����, �ϴ� ������ �� �� �ְھ�.\n�̰� �� Ȱ���ϸ� ����Ǳ��� �� �� �ְڱ�. ������.";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 9: //Ʃ�丮�� ����
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                gameObject.SetActive(false);
                break;


        }
    }

    void UpdateIndicators(int currentStep)
    {
        foreach(var indicator in indicators)
        {
            if (indicator.targetStep == currentStep) { indicator.Activate(); }
            else { indicator.Deactivate(); }
        }
    }
    public void TriggerTutorialStep(int step)
    {
        if (step > tutorialStep)
        {
            tutorialStep = step;
            ShowTutorialStep(tutorialStep);
        }
    }


}
