using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{

    // Start is called before the first frame update
    public bool isBattle { get; private set; } = false;
    [SerializeField]
    private Action action = null;
    public PlayerAction playerAction = null;
    public Rigidbody rb { get; private set; } = null;
    void Start()
    {
        Initilized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Initilized() {
        //インプットアクションを初期化
        action = InputSystemManager.instance.inputActions;
        //プレイヤーのアクション群も初期化
        if(playerAction == null)
            playerAction = new PlayerAction();

        action.Player.Move.performed += playerAction.OnMovePerformed;
        action.Player.NormalAttack.performed += playerAction.AttackInField;
    }
    
    //バトルーフェーズの
    public async UniTask Battle() {
        await UniTask.CompletedTask;
    }

    public override bool IsPlayer() {
        return true;
    }
}
