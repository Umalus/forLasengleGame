using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;
using static GameEnum;

public class BattlePhase : BasePhase {
    //�v���C���[��z�u����e�I�u�W�F�N�g�̈ʒu
    [SerializeField]
    private Transform playersRoot = null;
    //�G��z�u����e�I�u�W�F�N�g�̈ʒu
    [SerializeField]
    private Transform enemiesRoot = null;

    //�퓬�ɎQ�����Ă���v���C���[
    [SerializeField]
    private static List<BattlePlayer> players = new List<BattlePlayer>();
    //�퓬�ɎQ�����Ă���G
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
    /// �X�V����
    /// </summary>
    /// <returns></returns>
    public override async UniTask Execute() {
        gameObject.SetActive(turn);
        await FadeManager.instance.FadeIn();

        //�L�����N�^�[�S�Ă̍s������ݒ�
        int characterMax = players.Count + enemies.Count;
        List<BattleCharacterBase> inCharacterOrder = new List<BattleCharacterBase>(characterMax);
       
        for (int i = 0;i < characterMax; i++) {
            //�퓬�ɎQ������L�����N�^�[��f�������Ƀ\�[�g
            if(i >= players.Count)
                inCharacterOrder.Add(enemies[i % players.Count]);
            else
                inCharacterOrder.Add(players[i]);
        }

        inCharacterOrder.Sort((a,b) => b.speed - a.speed);
        //�o�߃^�[����������
        int pastTurn = 0;
        //�G���������S�ł���܂Ń��[�v
        while (!IsPlayerTeamAllDead() || !IsEnemiesTeamAllDead(enemies)) {
            //turn�������̃L�����N�^�[�Ȃ�
            if (turn) {
                //�v���C���[�̍s����I��
                await inCharacterOrder[pastTurn].GetComponent<BattlePlayer>().playerAction.Order();
            }
            //turn���G�̃L�����Ȃ�
            else {
                //�G�l�~�[���s����I��
                await UniTask.CompletedTask;

            }

            //�^�[����ύX
            if (IsRelativeEnemy(inCharacterOrder[pastTurn], inCharacterOrder[pastTurn + 1]))
                turn ^= true;
            //�o�߃^�[������i�߁A�L�����N�^�[�̐��𒴂����烊�Z�b�g
            pastTurn++;
            if(pastTurn > characterMax)
                pastTurn = 0;
        }

        await Teardown();
        
        //�G�����S�łȂ�
        if(IsEnemiesTeamAllDead(enemies)) {
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
        for(int i = 0,max = _playerParty.Count; i < max; i++) {
            players.Add(_playerParty[i]);
        }
        for(int i = 0,max = _battleEnemies.Count; i < max; i++) {
            enemies.Add(_battleEnemies[i]);
        }


    }
}

