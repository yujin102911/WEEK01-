using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("���� ���")]
    public PlayerMove playerMove;
    public GameObject tutorialPanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public GameObject tunerObject;
    public GameObject tunerLever;

    private int tutorialStep = 0;

    private void Start()
    {
        tunerObject.SetActive(false);
        tunerLever.SetActive(false);
        nextButton.onClick.AddListener(OnNextButtonClicked);

        StartTutorial();

    }
    void StartTutorial()
    {
        playerMove.canMove = false;
        tutorialStep = 1;
        ShowTutorialStep(tutorialStep);

    }

    public void OnNextButtonClicked()
    {
        tutorialStep++;
        ShowTutorialStep(tutorialStep);

    }

    void ShowTutorialStep(int step)
    {
        switch(step)
        {
            case 1:
                tutorialPanel.SetActive(true);
                dialogueText.text = "��.. ���� ������. ���� ���� ������?";
                break;

            case 2:
                dialogueText.text = "���? ������ ���� �� ������, �� ���� �ȿ�����?";
                break;

            case 3:
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                break;

            case 4:
                playerMove.canMove = false;
                tutorialPanel.SetActive(true);
                dialogueText.text = "(ö��ö��) ���� �ȿ�����.. ����ǿ� �����߰ڴ�.";
                break;

            case 5:
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                break;

            case 6:
                playerMove.canMove= false;
                tutorialPanel.SetActive(true);
                dialogueText.text = "���⵵ �ȿ�����. ���� �ٸ� ����� ã�ƺ��߰ڴ�..";
                break;

            case 7:
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                tunerObject.SetActive(true);
                break;

            case 8:
                playerMove.canMove = false;
                tutorialPanel.SetActive(true);
                dialogueText.text = "�̰� ����? ���� ��ư�� �ִ�. ��������. \n<color=yellow>[Q]�� ���� ���ļ� �����⸦ Ȱ��ȭ �ϼ���.</color>";
                nextButton.gameObject.SetActive(false);
                break;

            case 9:
                playerMove.canMove = false;
                tutorialPanel.SetActive(true);
                nextButton.gameObject.SetActive(true);
                dialogueText.text = "���� �̰�? ������ ������ ������ �����..\n���� �𸣰�����.. �ϴ� ������ ���ư� �� �ְھ�.";
                break;

            case 10:
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                gameObject.SetActive(false);
                break;

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
