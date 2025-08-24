using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class BattlePlayer : BattleCharacterBase
{
    public static Button normalAttackButton = null;
    public BattlePlayerAction playerAction = null;
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
        playerAction = new BattlePlayerAction();
    }

    public override bool IsPlayer() {
        return true;
    }
}
