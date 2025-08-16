using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;

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

    private int allAddExp = -1;
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
        int characterMax = players.Count + enemies.Count;
        List<CharacterBase> inCharacterOrder = new List<CharacterBase>(characterMax);
       
        for (int i = 0;i < characterMax; i++) {
            if(i >= players.Count)
                inCharacterOrder.Add(enemies[i]);

            inCharacterOrder.Add(players[i]);
        }
        //経過ターンを初期化
        int pastTurn = 0;
        //敵か味方が全滅するまでループ
        while (IsPlayerTeamAllDead() || IsEnemiesTeamAllDead(enemies)) {
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
        
        //敵側が全滅なら
        if(IsEnemiesTeamAllDead(enemies)) {
            List<UniTask> tasks = new List<UniTask>();
            for(int i = 0,max = players.Count; i < max; i++) {
                tasks.Add(players[i].AddExp(allAddExp));
            }
            await WaitTask(tasks);
        }
        //フェードアウト
        await FadeManager.instance.FadeOut();

    }

    public override async UniTask Teardown() {
        await base.Teardown();

        enemies.Clear();
    }

    private bool IsRelativeEnemy(CharacterBase _source,CharacterBase _target) {
        return _source.IsPlayer() !=  _target.IsPlayer();
    }

    private bool IsPlayerTeamAllDead() {
        for (int i = 0, max = players.Count; i < max; i++) {
            //もし一人でも死んでいなければfalseを返す
            if (!players[i].isDead) {
                return false;
            }
        }
        return true;
    }
    private bool IsEnemiesTeamAllDead(List<Enemy> _enemyTeam) {
        for (int i = 0, max = _enemyTeam.Count; i < max; i++) {
            if (!_enemyTeam[i].isDead) {
                return false;
            }
        }
        return true;
    }
}

