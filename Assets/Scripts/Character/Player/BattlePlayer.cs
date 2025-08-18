using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BattleCharacterBase
{
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
    }

    public override bool IsPlayer() {
        return true;
    }
    public async UniTask Battle() {
        await UniTask.CompletedTask;
    }
}
