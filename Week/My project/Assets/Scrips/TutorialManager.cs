using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [Header("연결 대상")]
    public PlayerMove playerMove;
    public GameObject tutorialPanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public GameObject tunerObject;

    [Header("느낌표 설정")]
    [Tooltip("관리할 모든 느낌표들을 여기에 등록하세요")]
    public List<IndicatorController> indicators;

    private int tutorialStep = 0;

    private void Start()
    {
        tunerObject.SetActive(false);

        foreach (var indicator in indicators) { indicator.Deactivate(); }

        StartTutorial();

    }


    private void Update()
    {   //대화창, "다음"이 켜져있고 스페이스바를 입력 받으면
        if(tutorialPanel.activeSelf && Input.GetButtonDown("Jump") && nextButton.gameObject.activeSelf)
        {
            //현재 버튼에 연결된 기능 실행
            nextButton.onClick.Invoke();
        }
    }

    void StartTutorial()
    {
        playerMove.canMove = false;
        tutorialStep = 1;
        ShowTutorialStep(tutorialStep);

    }

    //다음 대화로 넘어가는 함수
    public void GoToNextDialogue()
    {
        tutorialStep++;
        ShowTutorialStep(tutorialStep);

    }

    //대화창을 닫고 player에게 조작 권한을 주는 함수
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
        //버튼의 기존 연결을 모두 제거해서 중복 연결을 방지
        nextButton.onClick.RemoveAllListeners();
        tutorialPanel.SetActive(true);
        nextButton.gameObject.SetActive(true);

        //UpdateIndicators(step);

        switch(step)
        {
            case 1: //시작하자마자 실행
                dialogueText.text = "아.. 깜빡 잠들었다. 여기 무슨 역이지?";
                nextButton.onClick.AddListener(GoToNextDialogue); //다음 대화 로딩
                break;

            case 2:
                dialogueText.text = "어라? 열차가 멈춘 것 같은데, 왜 문이 안열리지?";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay); //다음 누르면 Player Turn이 올 수 있게
                break;

            case 3: //Player가 Trigger영역에 도착하면 (느낌표 뜬 문 앞)
                playerMove.canMove = false;
                dialogueText.text = "(철컹철컹) 문이 안열리네. 기장실에 가서 물어봐야겠다.";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 4: //Player가 Trigger영역에 도착하면 (다음 스테이지로 가는 문 앞)
                playerMove.canMove = false;
                tunerObject.SetActive(true); //이제부터 tuner 볼 수 있음
                dialogueText.text = "(철컹철컹) 여기도 안열리네? 나갈 다른 방법을 찾아봐야겠는걸.";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 5: //Player가 Trigger영역에 도착하면 (주파수 조율기 영역 내)
                playerMove.canMove = false;
                dialogueText.text = "이게 뭐지? 뭔가 오래된것같은 기계다.";
                nextButton.onClick.AddListener(GoToNextDialogue);
                break;

            case 6:
                dialogueText.text = "버튼이 하나 보인다. 누를까? \n<color=yellow>[Q]를 눌러 주파수 조율기를 활성화할 수 있습니다.</color>";
                tunerObject.SetActive(false);
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 7: //Player가 Q를 눌러 레버를 확인해서 레버를 당기고 난 직후
                playerMove.canMove = false;
                TunerManager.Instance.PauseTimer();
                dialogueText.text = "뭐지 이 기계? 버튼을 누르니 완전 다른 세상으로 온 것 같군.\n근데 레버를 내렸는데 왜 아무 반응이 없지? 다시 버튼을 눌러볼까?\n<color=yellow>[Q]를 다시 누르면 주파수 조율기가 비활성화 됩니다.</color>";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 8:
                playerMove.canMove = false;
                dialogueText.text = "무슨 기계인지는 잘 모르겠지만, 일단 앞으로 갈 수 있겠어.\n이걸 잘 활용하면 기장실까지 갈 수 있겠군. 가보자.";
                nextButton.onClick.AddListener(CloseDialogueAndResumePlay);
                break;

            case 9: //튜토리얼 종료
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
