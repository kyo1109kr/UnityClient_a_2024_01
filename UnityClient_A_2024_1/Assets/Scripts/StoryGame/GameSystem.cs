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

        if(GUILayout.Button("Reset Story Models")) //�����Ϳ��� ��ư ����
        {
            gameSystem.ResetStoryModels();
        }



    }

}
#endif
public class GameSystem : MonoBehaviour
{
    public StoryModel[] storyModels;   //����� ���丮 �𵨷� ����

    public static GameSystem instance;   //������ �̱��� ȭ

    private void Awake()                 //Scene�� ���� �ɶ� �ڵ� �����ܿ��� GameSystem�� �̱���ȭ
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

    public int currentStoryIndex = 1;                                   //����ǰ� �ִ� ���丮 ��ȣ


    public void ApplyChoice(StoryModel.Reslut reslut)               //��ư�� ���� ��� ���� �Լ�
    {
        switch (reslut.resultType)
        {
            case StoryModel.Reslut.ResultType.ChangeHp:             //Hp ���� ����
                //stats.currentHpPoint +=result.value;              //���� �����ϰų�
                ChangeState(reslut);                                //�Լ��� ���ؼ� ����
                break;

            case StoryModel.Reslut.ResultType.AddExperience:
                ChangeState(reslut);
                break;

            case StoryModel.Reslut.ResultType.GoToNextStory:  //���� ���丮 ����
                currentStoryIndex = reslut.value;             //���� ���丮 �ε��� �߰�
                StorShow(currentStoryIndex);                  //�Լ��� ���ؼ� ���丮 �ý��� �����Ͽ� ���丮 ���� ����
                break;

            case StoryModel.Reslut.ResultType.GoToRandomStory:   //���� ���丮 ����
                StoryModel temp = RandomStroy();
                StorShow(temp.storyNumber);
                break;
        }

    }

    public void StorShow(int number)
    {
        StoryModel tempstoryModel = FindstoryModel(number);  //���丮�� �����ִ� �Լ�

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
        List<StoryModel> storyModelList = new List<StoryModel>();  //������ ������ ���� List ����
        
        for(int i = 0; i <storyModels.Length; i ++)                //for������ �迭 �ȿ� �ִ� ������ �� �����Ϳ���
        {
            if( storyModels[i].storytype == StoryModel.STORYTYPE.MAIN)  //���丮 Ÿ���� Main �� ��쿡�� �ش� List�� �߰�
            {
                storyModelList.Add(storyModels[i]);
            }
        }
        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)];  //������ ���丮 �� �� List ���� ��ŭ ���� ���� ������ �����´�.
        currentStoryIndex = tempStoryModels.storyNumber;
        Debug.Log("currentStoryIndex" + currentStoryIndex);


        return tempStoryModels;
    }


    StoryModel FindstoryModel(int number)                   //StoryModel�� �ǵ����ִ� �Լ� ��ȣ�� ã�Ƽ� ����
    {
        StoryModel tempStoryModels = null;

        for (int i = 0; i < storyModels.Length; i++)   //for������ �迭 �ȿ� �ִ� ������ �� �����Ϳ���
        {                                              //storyNumber ���� ��ġ�� ��� ���Ƿ� ������ temp �� �־
            if(storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
            }
        }
        return tempStoryModels;             //return ��Ų��.
    }
   

#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryModel>("");//Resources ���� �Ʒ� ��� StoryModel �ҷ�����
    }

}
#endif