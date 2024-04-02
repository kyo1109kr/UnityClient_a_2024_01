using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //에디터
using System.Text; //텍스트 사용

namespace STORYGAME  //이름 충동 방지
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]

    public class GameSystemEditor : Editor   //유니티 에티더를 상속
    {
        public override void OnInspectorGUI()               //인스펙터 함수를 재정의
        {
            base.OnInspectorGUI();                           //기존 인스펙터를 가져와서 실행

            GameSystem gameSystem = (GameSystem)target;     //게임 시스템 스크립트 타겟을 설정

            if (GUILayout.Button("Reset Stroy Models"))      //버튼을 생성
            {
                gameSystem. ResetStoryModels();
            }
        }

    }
#endif
    public class GameSystem : MonoBehaviour
    {
        public static GameSystem instance;  //간단한 싱글톤 화

        public void Awake()
        {
            instance = this;
        }

        public StoryTableObject[] storyModels;
    
#if UNITY_EDITOR
    [ContextMenu("Reset Stroy Models")]

    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryTableObject>("");  //Resources 폴더 아래 모든 StorModel 불러 오기
    }

    }
#endif
}