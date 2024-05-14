using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static STORYGAME.StoryTableObject;
using STORYGAME;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{
    public int storyNumber;    //스토리 번호
    public Texture2D Mainlmage; //스토리 보여줄 이미지 텍스쳐

    public enum STORYTYPE  //스토리 타입 설정
    {
        MAIN,
        SUB,
        SERIAL

    }

    public STORYTYPE storytype;  //스토리 타입 선언
    public bool storyDone;      //스토리 종류 여부

    [TextArea(10, 10)]         //텍스트 영역 표시 
    public string storyText;   //메인 스토리

    public Option[] options; //선택지 배열

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText;
        public EventCheck eventCheck;
    }

    [System.Serializable]
    public class EventCheck
    {
        public int checkValue;

        public enum EventType : int
        { 
             NONE,
             GoToBattle,
             CheckSTR,
             CheeckDEX,
             CheckCON,
             CheckINT,
             CheckWIS,
             checkCHA,
        }

        public EventType eventType;

        public Reslut[] suceessResult;
        public Reslut[] failResult;

    }


    [System.Serializable]
    public class Reslut
    {
        public enum ResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoToRandomStory,
            GoToEnding
        }

        public ResultType resultType;    //결과값 타입
        public int value;                //변화 수치 입력
        public Stats stats;             //해당 스텟 변화 수치
    }

}
