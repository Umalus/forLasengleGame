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
            Debug.Log(_party[i].name + "‚ÌisSelect:" + _party[i].isSelect);
        }
    }
}
