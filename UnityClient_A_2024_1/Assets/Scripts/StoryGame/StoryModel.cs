using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static STORYGAME.StoryTableObject;
using STORYGAME;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{
    public int storyNumber;    //���丮 ��ȣ
    public Texture2D Mainlmage; //���丮 ������ �̹��� �ؽ���

    public enum STORYTYPE  //���丮 Ÿ�� ����
    {
        MAIN,
        SUB,
        SERIAL

    }

    public STORYTYPE storytype;  //���丮 Ÿ�� ����
    public bool storyDone;      //���丮 ���� ����

    [TextArea(10, 10)]         //�ؽ�Ʈ ���� ǥ�� 
    public string storyText;   //���� ���丮

    public Option[] options; //������ �迭

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

        public ResultType resultType;    //����� Ÿ��
        public int value;                //��ȭ ��ġ �Է�
        public Stats stats;             //�ش� ���� ��ȭ ��ġ
    }

}
