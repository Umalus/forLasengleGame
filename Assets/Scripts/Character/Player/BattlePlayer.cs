using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class BattlePlayer : BattleCharacterBase
{
    public Button normalAttackButton = null;
    public PlayerAction playerAction = null;
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
        playerAction = new PlayerAction();
    }

    public override bool IsPlayer() {
        return true;
    }
    private async UniTask NormalAttack() {

        var buttonEvent = normalAttackButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        await buttonEvent.OnInvokeAsync();
        await playerAction.NormalAttack();
    }
}
