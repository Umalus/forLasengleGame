/*
 * @brief   �Ώۈ�̂Ɍ����ĉe��������ڂ�
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;

public class ActionRange001_Solo : ActionRangeBase
{
    
    public override void Execute(BattleCharacterBase _targetCharacter) {
        //���X�g��������
        InitializeList(ref targetIDList);
        //�Ώۃ��X�g�ɒǉ�
        targetIDList.Add(_targetCharacter.ID);
    }
}
