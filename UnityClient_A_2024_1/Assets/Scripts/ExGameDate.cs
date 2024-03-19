using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ExGameDate" , fileName = "New ExGame Data" , order = 50)]
public class ExGameData : ScriptableObject //��ũ��Ʈ�� ������Ʈ ���
{
    //���� ���� ������ �߰�
    public string gameName;
    public int gameScore;
    public bool isGameActive;
    
}
