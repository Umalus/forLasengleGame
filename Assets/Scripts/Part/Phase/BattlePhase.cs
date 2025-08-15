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

        //�L�����N�^�[�S�Ă̍s������ݒ�
        List<CharacterBase> inCharacterOrder = new List<CharacterBase>();
        int characterMax = players.Count + enemies.Count;
        for (int i = 0;i < characterMax; i++) {

        }

        int pastTurn = 0;
        //�G���������S�ł���܂Ń��[�v
        while (true) {
            //turn�������̃L�����N�^�[�Ȃ�
            if (turn) {
                //�v���C���[�̍s����I��
                await inCharacterOrder[pastTurn].GetComponent<Player>().playerAction.Order();
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
    }

    public override async UniTask Teardown() {
        await base.Teardown();

        enemies.Clear();
    }

    private bool IsRelativeEnemy(CharacterBase _source,CharacterBase _target) {
        return _source.IsPlayer() !=  _target.IsPlayer();
    }
}

