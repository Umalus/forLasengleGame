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
        InitializeList(ref targetList);
        //�Ώۃ��X�g�ɒǉ�
        if(_targetCharacter.isSelect)
        targetList.Add(_targetCharacter);
    }
}
