using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("연결 대상")]
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
                dialogueText.text = "아.. 깜빡 잠들었다. 여기 무슨 역이지?";
                break;

            case 2:
                dialogueText.text = "어라? 열차가 멈춘 것 같은데, 왜 문이 안열리지?";
                break;

            case 3:
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                break;

            case 4:
                playerMove.canMove = false;
                tutorialPanel.SetActive(true);
                dialogueText.text = "(철컹철컹) 문이 안열리네.. 기장실에 가봐야겠다.";
                break;

            case 5:
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                break;

            case 6:
                playerMove.canMove= false;
                tutorialPanel.SetActive(true);
                dialogueText.text = "여기도 안열리네. 나갈 다른 방법을 찾아봐야겠다..";
                break;

            case 7:
                tutorialPanel.SetActive(false);
                playerMove.canMove = true;
                tunerObject.SetActive(true);
                break;

            case 8:
                playerMove.canMove = false;
                tutorialPanel.SetActive(true);
                dialogueText.text = "이게 뭐지? 뭔가 버튼이 있다. 눌러보자. \n<color=yellow>[Q]를 눌러 주파수 조율기를 활성화 하세요.</color>";
                nextButton.gameObject.SetActive(false);
                break;

            case 9:
                playerMove.canMove = false;
                tutorialPanel.SetActive(true);
                nextButton.gameObject.SetActive(true);
                dialogueText.text = "뭐지 이거? 누르니 없었던 레버가 생겼어..\n뭔진 모르겠지만.. 일단 앞으로 나아갈 수 있겠어.";
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
