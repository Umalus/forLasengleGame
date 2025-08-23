using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;
using static GameEnum;

public class BattlePhase : BasePhase {
    //プレイヤーを配置する親オブジェクトの位置
    [SerializeField]
    private Transform playersRoot = null;
    //敵を配置する親オブジェクトの位置
    [SerializeField]
    private Transform enemiesRoot = null;

    //戦闘に参加しているプレイヤー
    [SerializeField]
    private static List<BattlePlayer> players = new List<BattlePlayer>();
    //戦闘に参加している敵
    [SerializeField]
    private static List<BattleEnemy> enemies = new List<BattleEnemy>();

    private int allAddExp = -1;
    private bool turn = true;
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    override public async UniTask Initialize() {
        if (players == null || enemies == null) await UniTask.CompletedTask;

        allAddExp = 0;
        for(int i = 0,max = players.Count;i < max; i++) {
            BattlePlayer createObject = Instantiate(players[i], playersRoot);
            createObject.Initilized(i,0);
        }
        for(int i = 0,max = enemies.Count;i < max; i++) {
            BattleEnemy createObject = Instantiate(enemies[i], enemiesRoot);
            createObject.Initilized(i + players.Count,0);
            allAddExp += enemies[i].exp;
        }
        
        await UniTask.CompletedTask;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    /// <returns></returns>
    public override async UniTask Execute() {
        gameObject.SetActive(turn);
        await FadeManager.instance.FadeIn();

        //キャラクター全ての行動順を設定
        int characterMax = players.Count + enemies.Count;
        List<BattleCharacterBase> inCharacterOrder = new List<BattleCharacterBase>(characterMax);
       
        for (int i = 0;i < characterMax; i++) {
            //戦闘に参加するキャラクターを素早さ順にソート
            if(i >= players.Count)
                inCharacterOrder.Add(enemies[i % players.Count]);
            else
                inCharacterOrder.Add(players[i]);
        }

        inCharacterOrder.Sort((a,b) => b.speed - a.speed);
        //経過ターンを初期化
        int pastTurn = 0;
        //敵か味方が全滅するまでループ
        while (!IsPlayerTeamAllDead() || !IsEnemiesTeamAllDead(enemies)) {
            //turnが自分のキャラクターなら
            if (turn) {
                //プレイヤーの行動を選択
                await inCharacterOrder[pastTurn].GetComponent<BattlePlayer>().playerAction.Order();
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
        await MainGamePart.ChangeGamePhase(eGamePhase.Field);
    }
    /// <summary>
    /// 解放処理
    /// </summary>
    /// <returns></returns>
    public override async UniTask Teardown() {
        await base.Teardown();
    }
    /// <summary>
    /// 相対的を判定
    /// </summary>
    /// <param name="_source"></param>
    /// <param name="_target"></param>
    /// <returns></returns>
    private bool IsRelativeEnemy(BattleCharacterBase _source,BattleCharacterBase _target) {
        return _source.IsPlayer() !=  _target.IsPlayer();
    }
    /// <summary>
    /// プレイヤーが倒されたか判定
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerTeamAllDead() {
        for (int i = 0, max = players.Count; i < max; i++) {
            //もし一人でも死んでいなければfalseを返す
            if (!players[i].isDead) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 敵を全て倒したか判定
    /// </summary>
    /// <param name="_enemyTeam"></param>
    /// <returns></returns>
    private bool IsEnemiesTeamAllDead(List<BattleEnemy> _enemyTeam) {
        for (int i = 0, max = _enemyTeam.Count; i < max; i++) {
            if (!_enemyTeam[i].isDead) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 戦闘に参加するキャラクターを設定
    /// </summary>
    public static void SetCharacter(List<BattlePlayer> _playerParty,List<BattleEnemy> _battleEnemies) {
        //一度戦闘に参加したキャラクターをリセット
        if(!IsEmpty(players))
        players.Clear();
        if(!IsEmpty(enemies))
        enemies.Clear();
        for(int i = 0,max = _playerParty.Count; i < max; i++) {
            players.Add(_playerParty[i]);
        }
        for(int i = 0,max = _battleEnemies.Count; i < max; i++) {
            enemies.Add(_battleEnemies[i]);
        }


    }
}

