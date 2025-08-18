/*
 * @brief   対象一体に向けて影響をおよぼす
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;

public class ActionRange001_Solo : ActionRangeBase
{
    
    public override void Execute(BattleCharacterBase _targetCharacter) {
        //リストを初期化
        InitializeList(ref targetIDList);
        //対象リストに追加
        targetIDList.Add(_targetCharacter.ID);
    }
}
