using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;
using static GameEnum;

public class BattlePhase : BasePhase {
    //�v���C���[��z�u����e�I�u�W�F�N�g�̈ʒu
    private static Transform playerRoot;
    //�G��z�u����e�I�u�W�F�N�g�̈ʒu
    [SerializeField]
    private static Transform enemyRoot;

    [SerializeField]
    UIBattle battleCanvas = null;

    //�퓬�ɎQ�����Ă���v���C���[
    [SerializeField]
    private static List<BattlePlayer> players = new List<BattlePlayer>();
    //�퓬�ɎQ�����Ă���G
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
    /// ����������
    /// </summary>
    /// <returns></returns>
    override public async UniTask Initialize() {
        if (players == null || enemies == null) await UniTask.CompletedTask;
        //�L�����N�^�[�S�Ă̍s������ݒ�
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
    /// �X�V����
    /// </summary>
    /// <returns></returns>
    public override async UniTask Execute() {
        gameObject.SetActive(true);
        await FadeManager.instance.FadeIn();

        

        //inCharacterOrder.Sort((a,b) => b.speed - a.speed);
        //�o�߃^�[����������
        int currentTurn = 0;
        bool battleEnd = false;
        //�G���������S�ł���܂Ń��[�v
        while (!battleEnd) {
            BattleCharacterBase actionCharacter = inCharacterOrder[currentTurn % inCharacterOrder.Count];
            
            //turn�������̃L�����N�^�[�Ȃ�
            if (isPlayerTurn) {
                await battleCanvas.Open();
                //�v���C���[�̍s����I��
                await actionCharacter.GetComponent<BattlePlayer>().playerAction.Order(enemies,actionCharacter);
                await battleCanvas.Close();
            }
            //turn���G�̃L�����Ȃ�
            else {
                //�G�l�~�[���s����I��
                await actionCharacter.GetComponent<BattleEnemy>().Order(players[0]);
            }
            
            battleEnd = IsPlayerTeamAllDead();
            if (battleEnd)
                continue;
            battleEnd = IsEnemiesTeamAllDead(enemies);
            if (battleEnd)
                continue;

            //�^�[����ύX
            if (IsRelativeEnemy(inCharacterOrder[currentTurn % inCharacterOrder.Count], inCharacterOrder[(currentTurn + 1) % inCharacterOrder.Count]))
                isPlayerTurn = !isPlayerTurn;
            //�o�߃^�[������i�߁A�L�����N�^�[�̐��𒴂����烊�Z�b�g
            currentTurn++;
        }
        //�퓬�I���㏈��

        //�G�����S�łȂ�
        if(IsEnemiesTeamAllDead(enemies)) {
            fieldEnemy.GetComponent<StageObject>().isBreak = true;

            List<UniTask> tasks = new List<UniTask>();
            for(int i = 0,max = players.Count; i < max; i++) {
                tasks.Add(players[i].AddExp(allAddExp));
            }
            await WaitTask(tasks);
        }
        //�t�F�[�h�A�E�g
        await FadeManager.instance.FadeOut();
        await MainGamePart.ChangeGamePhase(eGamePhase.Field);
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <returns></returns>
    public override async UniTask Teardown() {
        await base.Teardown();
    }
    /// <summary>
    /// ���ΓI�𔻒�
    /// </summary>
    /// <param name="_source"></param>
    /// <param name="_target"></param>
    /// <returns></returns>
    private bool IsRelativeEnemy(BattleCharacterBase _source,BattleCharacterBase _target) {
        return _source.IsPlayer() !=  _target.IsPlayer();
    }
    /// <summary>
    /// �v���C���[���|���ꂽ������
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerTeamAllDead() {
        for (int i = 0, max = players.Count; i < max; i++) {
            //������l�ł�����ł��Ȃ����false��Ԃ�
            if (!players[i].isDead) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// �G��S�ē|����������
    /// </summary>
    /// <param name="_enemyTeam"></param>
    /// <returns></returns>
    private bool IsEnemiesTeamAllDead(List<BattleEnemy> _enemyTeam) {
        //for���Ŋm�F
        for (int i = 0, max = _enemyTeam.Count; i < max; i++) {
            if (!_enemyTeam[i].isDead) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// �퓬�ɎQ������L�����N�^�[��ݒ�
    /// </summary>
    public static void SetCharacter(List<BattlePlayer> _playerParty,List<BattleEnemy> _battleEnemies) {
        //��x�퓬�ɎQ�������L�����N�^�[�����Z�b�g
        if(!IsEmpty(players))
        players.Clear();
        if(!IsEmpty(enemies))
        enemies.Clear();
        //�퓬�ɎQ������L�����N�^�[�����ꂼ��ǉ�
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
                //�e�̂Ƀp�[�e�B�[�����o�[�����L
                enemies[i].partyData.partyMemberList.Add(enemies[j]);
            }
        }


    }
}

