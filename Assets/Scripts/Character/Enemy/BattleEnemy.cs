using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BattleEnemy : BattleCharacterBase {
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
    }

    public override bool IsPlayer() {
        return false;
    }

    public async UniTask Order(BattlePlayer _target) {
        int actionIndex = Random.Range(0, 3);
        UniTask action;
        switch (actionIndex) {
            case 0:
                action = Attack(_target);
                break;
            case 1:
                action = Hopping();
                break;
            default:
                action = UniTask.CompletedTask;
                break;
        }
        await action;
    }

    private async UniTask Attack(BattlePlayer _target) {
        int damage = Random.Range(rawAttack - 5, rawAttack);

        _target.RemoveHP(damage);
        Debug.Log("PlayerのHP:" + _target.HP);
        await UniTask.DelayFrame(500);
    }

    private async UniTask Hopping() {
        Debug.Log("pop-pop");
        //エネミーが行動を選択
        await UniTask.DelayFrame(500);
    }
}
