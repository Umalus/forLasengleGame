using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerAction{

    //バトル中の行動選択
    public async UniTask Order() {
        while (true) {
            if (await NormalAttack())
                break;
        }
    }

    private async UniTask<bool> NormalAttack() {

        var buttonEvent = BattlePlayer.normalAttackButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        await buttonEvent.OnInvokeAsync();
        return true;
    }
}
