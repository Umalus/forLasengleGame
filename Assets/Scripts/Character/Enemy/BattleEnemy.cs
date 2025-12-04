using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattleEnemy : BattleCharacterBase {
    [System.NonSerialized]
    public PartyData partyData = null;
    public GameObject targetUI = null;
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
        partyData = new PartyData();
        targetUI.transform.LookAt(Vector3.back);
        targetUI.SetActive(false);
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
                action = UniTask.DelayFrame(1);
                break;
        }
        await action;
    }

    private async UniTask Attack(BattlePlayer _target) {
        int damage = Random.Range(rawAttack - 5, rawAttack);
        _target.RemoveHP(damage);
        anim.SetTrigger("Attack");
        _target.TakeDamageAnimation();
        Debug.Log("PlayerのHP:" + _target.HP);
        await UniTask.DelayFrame(500);
    }

    private async UniTask Hopping() {
        Debug.Log("pop-pop");
        //エネミーが行動を選択
        await UniTask.DelayFrame(500);
    }
    public void SelectEnemy() {


        if (!BattlePhase.isPlayerTurn || BattlePhase.commandEnd) return;
        partyData.SelectOnlyCharacter(this,partyData.partyMemberList);

    }

    
}
