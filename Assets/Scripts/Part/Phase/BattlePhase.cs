using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase : BasePhase
{
    [SerializeField]
    private Transform playersRoot = null;
    [SerializeField]
    private Transform enemiesRoot = null;

    [SerializeField]
    private List<Player> players = null;
    [SerializeField]
    private List<Enemy> enemies = null;

    private bool turn = true;
    override public async UniTask Initialize() {
        for(int i = 0,max = players.Count;i < max; i++) {
            players[i].transform.SetParent(playersRoot);
        }
        for(int i = 0,max = enemies.Count;i < max; i++) {
            enemies[i].transform.SetParent(enemiesRoot);
        }

        await UniTask.CompletedTask;
    }

    public override async UniTask Execute() {
        await Initialize();

        //キャラクター全ての行動順を設定
        List<CharacterBase> inCharacterOrder = new List<CharacterBase>();
        int characterMax = players.Count + enemies.Count;
        for (int i = 0;i < characterMax; i++) {

        }

        int pastTurn = 0;
        //敵か味方が全滅するまでループ
        while (true) {
            //turnが自分のキャラクターなら
            if (turn) {
                //プレイヤーの行動を選択
                await inCharacterOrder[pastTurn].GetComponent<Player>().playerAction.Order();
            }
            //turnが敵のキャラなら
            else {
                //エネミーが行動を選択
                await UniTask.CompletedTask;

            }

            //ターンを変更
            if (IsRelativeEnemy(inCharacterOrder[pastTurn], inCharacterOrder[pastTurn + 1]))
                turn ^= true;
            //経過ターンを一つ進め、キャラクターの数を超えたらリセット
            pastTurn++;
            if(pastTurn > characterMax)
                pastTurn = 0;
        }
        await Teardown();
    }

    public override async UniTask Teardown() {
        await base.Teardown();

        enemies.Clear();
    }

    private bool IsRelativeEnemy(CharacterBase _source,CharacterBase _target) {
        return _source.IsPlayer() !=  _target.IsPlayer();
    }
}

