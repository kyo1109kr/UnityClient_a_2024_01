using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    //UI를 컨트롤 할 것이라 추가
using System;           //String 관련 함수 사용 하기 위해 추가

public class StorySystem : MonoBehaviour
{
    public static StorySystem instance;  //간단한 싱글톤 화
    public StoryModel currenStoryModel; //지금 스토리 모델 참조


    public enum TEXTSYSTEM
    {
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;              //각 글자가 나타나는데 걸리는 시간
    public string fullText;                //전체 표시할 텍스트
    private string currentText = "";        //현재까지 표시된 텍스트
    public Text textComponent;              //text 컴포넌트
    public Text storyIndex;                 // 스토리 번호 표시할 UI
    public Image imageComponent;            //이미지UI

    public Button[] buttonWay = new Button[3];   //선택지 버튼 추가
    public Text[] buttonWayText = new Text[3];  //선택지 버튼 Text
    




    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttonWay.Length; i ++)   //버튼 숫자에 따른 함수
        {
            int wayIndex = i;
            buttonWay[i].onClick.AddListener(() => OnWayClick(wayIndex));
        }

        StroyModelinit();
        StartCoroutine(ShowText());
    }

    public void StroyModelinit()
    {
        fullText = currenStoryModel.storyText;
        storyIndex.text = currenStoryModel.storyNumber.ToString();

        for(int i = 0; i < currenStoryModel.options.Length; i++)
        {
            buttonWayText[i].text = currenStoryModel.options[i].buttonText; //버튼 이름을 설정
        }
    }

    public void OnWayClick(int index)
    {

    }


    IEnumerator ShowText()                                      //코루틴 함수 사용
    {
        if(currenStoryModel.Mainlmage !=null)
        {
            //Texture2D를 Sprtie 변환

            Rect rect = new Rect(0, 0, currenStoryModel.Mainlmage.width, currenStoryModel.Mainlmage.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f); //스프라이트 의 축 (중심) 지정
            Sprite sprite = Sprite.Create(currenStoryModel.Mainlmage, rect, pivot);

            imageComponent.sprite = sprite; ;
        }

        else
        {
            Debug.LogError("텍스쳐 로딩이 되지 않습니다. : " + currenStoryModel.Mainlmage.name);
        }

        for(int i = 0; i <fullText.Length; i++)             
        {
            currentText = fullText.Substring(0, i);             //substring 문자열 자르는 함수
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);             //delay 초만큼 For 문을 지연 시킨다.
        }

        for(int i = 0; i <currenStoryModel.options.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);

        }

        yield return new WaitForSeconds(delay);

    }

}
