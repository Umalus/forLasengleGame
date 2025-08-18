using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemy : BattleCharacterBase
{
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
    }

    public override bool IsPlayer() {
        return false;
    }
}
