using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using STORYGAME;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameSystem gameSystem = (GameSystem)target;

        if(GUILayout.Button("Reset Story Models")) //에디터에서 버튼 생성
        {
            gameSystem.ResetStoryModels();
        }



    }

}
#endif
public class GameSystem : MonoBehaviour
{
    public StoryModel[] storyModels;   //변경된 스토리 모델로 생성

    public static GameSystem instance;   //간단한 싱글톤 화

    private void Awake()                 //Scene이 생성 될때 코드 설정단에서 GameSystem을 싱글톤화
    {
        instance = this;
    }

    public enum GAMESTATE
    {
        STROYSHOW,
        WAITSELECT,
        STORYEND,
        BATTLEMODE,
        BATTLEDONE,
        SHOPMODE,
        ENDMODE
    }
    public Stats stats;
    public GAMESTATE currentState;

    public int currentStoryIndex = 1;                                   //진행되고 있는 스토리 번호


    public void ApplyChoice(StoryModel.Reslut reslut)               //버튼을 누른 결과 연산 함수
    {
        switch (reslut.resultType)
        {
            case StoryModel.Reslut.ResultType.ChangeHp:             //Hp 변동 사항
                //stats.currentHpPoint +=result.value;              //직접 변경하거나
                ChangeState(reslut);                                //함수를 통해서 변경
                break;

            case StoryModel.Reslut.ResultType.AddExperience:
                ChangeState(reslut);
                break;

            case StoryModel.Reslut.ResultType.GoToNextStory:  //다음 스토리 진행
                currentStoryIndex = reslut.value;             //현재 스토리 인덱스 추가
                StorShow(currentStoryIndex);                  //함수를 통해서 스토리 시스템 접근하여 스토리 연출 시작
                break;

            case StoryModel.Reslut.ResultType.GoToRandomStory:   //랜덤 스토리 진행
                StoryModel temp = RandomStroy();
                StorShow(temp.storyNumber);
                break;
        }

    }

    public void StorShow(int number)
    {
        StoryModel tempstoryModel = FindstoryModel(number);  //스토리를 보여주는 함수

        StorySystem.instance.currenStoryModel = tempstoryModel;
        StorySystem.instance.CoShowText();
    }



    public void ChangeState(StoryModel .Reslut reslut)
    {
        if(reslut.stats.hpPoint > 0) stats.hpPoint += reslut.stats.hpPoint;
        if (reslut.stats.spPoint > 0) stats.spPoint += reslut.stats.spPoint;
        if (reslut.stats.currentHpPoint > 0) stats.currentHpPoint += reslut.stats.currentHpPoint;
        if (reslut.stats.currentHpPoint > 0) stats.currentHpPoint += reslut.stats.currentSpPoint;
        if (reslut.stats.currentXpPoint > 0) stats.currentXpPoint += reslut.stats.currentXpPoint;
        if (reslut.stats.strength > 0) stats.strength += reslut.stats.strength;
        if (reslut.stats.dexterity > 0) stats.dexterity += reslut.stats.dexterity;
        if (reslut.stats.consitiution > 0) stats.consitiution += reslut.stats.consitiution;
        if (reslut.stats.wisdom > 0) stats.wisdom += reslut.stats.wisdom;
        if (reslut.stats.Intelligence > 0) stats.Intelligence += reslut.stats.Intelligence;
        if (reslut.stats.charisma > 0) stats.charisma += reslut.stats.charisma;

    }



    StoryModel RandomStroy()
    {
        StoryModel tempStoryModels = null;
        List<StoryModel> storyModelList = new List<StoryModel>();  //램덤을 돌리기 위한 List 생성
        
        for(int i = 0; i <storyModels.Length; i ++)                //for문으로 배열 안에 있는 선언한 모델 데이터에서
        {
            if( storyModels[i].storytype == StoryModel.STORYTYPE.MAIN)  //스토리 타입이 Main 인 경우에만 해당 List에 추가
            {
                storyModelList.Add(storyModels[i]);
            }
        }
        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)];  //선별한 스토리 들 중 List 숫자 만큼 랜덤 값을 돌려서 가져온다.
        currentStoryIndex = tempStoryModels.storyNumber;
        Debug.Log("currentStoryIndex" + currentStoryIndex);


        return tempStoryModels;
    }


    StoryModel FindstoryModel(int number)                   //StoryModel을 되돌려주는 함수 번호로 찾아서 리턴
    {
        StoryModel tempStoryModels = null;

        for (int i = 0; i < storyModels.Length; i++)   //for문으로 배열 안에 있는 선언한 모델 데이터에서
        {                                              //storyNumber 값이 일치할 경우 임의로 선언한 temp 에 넣어서
            if(storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
            }
        }
        return tempStoryModels;             //return 시킨다.
    }
   

#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryModel>("");//Resources 폴더 아래 모든 StoryModel 불러오기
    }

}
#endif