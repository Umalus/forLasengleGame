using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData001_Slash : SkillDataBase
{
    public override void UseSkill(BattleCharacterBase _useCharacter) {
        //�����A�g�p�҂̃��x�����K�v�ȃ��x���𖞂����Ă��Ȃ���΁A�������Ȃ�
        //��UI�ŕ\�����Ȃ��悤�ɂ͂��邪�O�̂���
        if (_useCharacter.lv < this.lv) return;
        //MP������Ȃ����̓��O��\�����������Ȃ�
        if(_useCharacter.MP < needMP) {
            Debug.Log("MP������܂���!");
            return;
        }
        for(int i = 0 ,max = rangeType; i < max; i++) {

        }

    }
}
