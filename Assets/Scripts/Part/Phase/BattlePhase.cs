using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;
using static GameEnum;

public class BattlePhase : BasePhase {
    //プレイヤーを配置する親オブジェクトの位置
    private static Transform playerRoot;
    //敵を配置する親オブジェクトの位置
    [SerializeField]
    private static Transform enemyRoot;

    [SerializeField]
    UIBattle battleCanvas = null;

    //戦闘に参加しているプレイヤー
    [SerializeField]
    private static List<BattlePlayer> players = new List<BattlePlayer>();
    //戦闘に参加している敵
    [SerializeField]
    private static List<BattleEnemy> enemies = new List<BattleEnemy>();

    private List<BattleCharacterBase> inCharacterOrder = null;

    public static GameObject fieldEnemy = null;

    private int allAddExp = -1;
    public bool isPlayerTurn { get; private set; } = true;
    private void Awake() {
        playerRoot = GameObject.Find("PlayerRoot").transform;
        enemyRoot = GameObject.Find("EnemyRoot").transform;
    }
    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <returns></returns>
    override public async UniTask Initialize() {
        if (players == null || enemies == null) await UniTask.CompletedTask;
        //キャラクター全ての行動順を設定
        int characterMax = players.Count + enemies.Count;
        inCharacterOrder = new List<BattleCharacterBase>(characterMax);

        allAddExp = 0;
        for(int i = 0,max = players.Count;i < max; i++) {
            inCharacterOrder.Add(players[i]);
        }
        for(int i = 0,max = enemies.Count;i < max; i++) {
            inCharacterOrder.Add(enemies[i]);
            allAddExp += inCharacterOrder[i + players.Count].exp;
        }

        await battleCanvas.Open();

        if(Cursor.lockState != CursorLockMode.None)
            Cursor.lockState = CursorLockMode.None;

        await UniTask.CompletedTask;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    /// <returns></returns>
    public override async UniTask Execute() {
        gameObject.SetActive(true);
        await FadeManager.instance.FadeIn();

        

        //inCharacterOrder.Sort((a,b) => b.speed - a.speed);
        //経過ターンを初期化
        int currentTurn = 0;
        bool battleEnd = false;
        //敵か味方が全滅するまでループ
        while (!battleEnd) {
            BattleCharacterBase actionCharacter = inCharacterOrder[currentTurn % inCharacterOrder.Count];
            
            //turnが自分のキャラクターなら
            if (isPlayerTurn) {
                await battleCanvas.Open();
                //プレイヤーの行動を選択
                await actionCharacter.GetComponent<BattlePlayer>().playerAction.Order(enemies,actionCharacter);
                await battleCanvas.Close();
            }
            //turnが敵のキャラなら
            else {
                //エネミーが行動を選択
                await actionCharacter.GetComponent<BattleEnemy>().Order(players[0]);
            }
            
            battleEnd = IsPlayerTeamAllDead();
            if (battleEnd)
                continue;
            battleEnd = IsEnemiesTeamAllDead(enemies);
            if (battleEnd)
                continue;

            //ターンを変更
            if (IsRelativeEnemy(inCharacterOrder[currentTurn % inCharacterOrder.Count], inCharacterOrder[(currentTurn + 1) % inCharacterOrder.Count]))
                isPlayerTurn = !isPlayerTurn;
            //経過ターンを一つ進め、キャラクターの数を超えたらリセット
            currentTurn++;
        }
        //戦闘終了後処理

        //敵側が全滅なら
        if(IsEnemiesTeamAllDead(enemies)) {
            fieldEnemy.GetComponent<StageObject>().isBreak = true;

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
        //for文で確認
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
        //戦闘に参加するキャラクターをそれぞれ追加
        for(int i = 0,max = _playerParty.Count; i < max; i++) {
            players.Add(Instantiate(_playerParty[i],playerRoot));
            players[i].Initilized(i,i);
        }

        
        for(int i = 0,max = _battleEnemies.Count; i < max; i++) {
            //Vector3 instancePos = enemyRoot.position;
            //instancePos *= i;
            //enemyRoot.position = instancePos;
            enemies.Add(Instantiate(_battleEnemies[i], enemyRoot));
            enemies[i].transform.position = new Vector3(enemyRoot.position.x + (2 * i), enemyRoot.position.y, enemyRoot.position.z);
            enemies[i].Initilized(_playerParty.Count + i, _playerParty.Count + 1);
        }

        for(int i = 0 , iMax = enemies.Count; i < iMax; i++) {
            for(int j = 0 , jMax = enemies.Count; j < jMax;j++ ) {
                //各個体にパーティーメンバーを共有
                enemies[i].partyData.partyMemberList.Add(enemies[j]);
            }
        }


    }
}

