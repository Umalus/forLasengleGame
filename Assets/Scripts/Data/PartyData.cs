using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PartyData : MonoBehaviour {
    public List<BattleCharacterBase> partyMemberList = new List<BattleCharacterBase>(4);
    public void SelectOnlyCharacter(BattleCharacterBase _character, List<BattleCharacterBase> _party) {
        foreach (var member in _party) {
            member.isSelect = false;
            member.GetComponent<BattleEnemy>().targetUI.SetActive(member.isSelect);
        }
        _character.isSelect = true;
        _character.GetComponent<BattleEnemy>().targetUI.SetActive(_character.isSelect);
        for (int i = 0, max = _party.Count; i < max; i++) {
            Debug.Log(_party[i].name + "のisSelect:" + _party[i].isSelect);
        }
    }

    /// <summary>
    /// 中心からカウント分キャラクターを選択状態にする
    /// </summary>
    /// <param name="_enemyCount"></param>
    /// <param name="_centerIndex"></param>
    /// <param name="_party"></param>
    public void SelectEnemyByCenter(int _enemyCount, int _centerIndex, List<BattleCharacterBase> _party) {
        //まずパーティーメンバー全員のisSelectをfalse
        foreach (var member in _party) {
            member.isSelect = false;
        }
        //左右に広がっていく変数(例:-2,-1,Center,1,2)
        int spreadIndex = 0;
        for (int i = 0; i < _enemyCount; i++) {
            //中心のキャラクターを選択し、そいつから左右に_enemyCount分選ぶ    
            int sign = (i % 2 == 0 ? -1 : 1);
            int selectIndex = _centerIndex + spreadIndex * sign;
            if (selectIndex < 0) continue;
            _party[selectIndex].isSelect = true;
            //もし符号が負(iが2の倍数なら)
            if (sign < 0)
                spreadIndex++;
        }
    }
}
