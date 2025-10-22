using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyData : MonoBehaviour
{
    public List<BattleCharacterBase> partyMemberList = new List<BattleCharacterBase>(4);

    public void SelectOnlyCharacter(BattleCharacterBase _character,List<BattleCharacterBase> _party) {
        foreach(var member in _party) {
            member.isSelect = false;
        }
        _character.isSelect = true;

        for(int i = 0 , max = _party.Count; i < max; i++) {
            Debug.Log(_party[i].name + "��isSelect:" + _party[i].isSelect);
        }
    }

    /// <summary>
    /// ���S����J�E���g���L�����N�^�[��I����Ԃɂ���
    /// </summary>
    /// <param name="_enemyCount"></param>
    /// <param name="_centerIndex"></param>
    /// <param name="_party"></param>
    public void SelectEnemyByCenter(int _enemyCount,int _centerIndex, List<BattleCharacterBase> _party) {
        //�܂��p�[�e�B�[�����o�[�S����isSelect��false
        foreach(var member in _party) {
            member.isSelect = false;
        }
        //���E�ɍL�����Ă����ϐ�(��:-2,-1,Center,1,2)
        int spreadIndex = 0;
        for(int i = 0; i < _enemyCount; i++) {
        //���S�̃L�����N�^�[��I�����A�������獶�E��_enemyCount���I��    
            int sign = (i % 2 == 0 ? -1 : 1);
            int selectIndex = _centerIndex + spreadIndex * sign;
            if (selectIndex < 0) continue;
            _party[selectIndex].isSelect = true;
            //������������(i��2�̔{���Ȃ�)
            if (sign < 0)
                spreadIndex++;

        }
    }
}
