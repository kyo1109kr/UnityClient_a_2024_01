using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterManager : MonoBehaviour
{
    public List<Excharacter> characterList = new List<Excharacter>();
    //���� ���� Ŭ������ ����ص�  ExCharacter Fast,ExCharacterUp �ڽ� Ŭ������ List�� �������.


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))  //Ű�� ������ �ı� �ȴ�
        {
            for(int i = 0; i < characterList.Count; i++)
            {
                characterList[i].DestoryCharacter();
            }
        }

    }
}
