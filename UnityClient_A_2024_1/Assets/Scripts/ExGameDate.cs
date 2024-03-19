using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ExGameDate" , fileName = "New ExGame Data" , order = 50)]
public class ExGameData : ScriptableObject //스크립트를 오브젝트 상속
{
    //게임 관련 변수들 추가
    public string gameName;
    public int gameScore;
    public bool isGameActive;
    
}
