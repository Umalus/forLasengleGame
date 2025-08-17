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
    public float moveSpeed = 10.0f;
    public Vector3 moveDir = Vector3.zero;
    void Start()
    {
        Initilized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Force);
    }

    public override void Initilized() {
        //インプットアクションを初期化
        action = InputSystemManager.instance.inputActions;
        //リジッドボディも初期化
        rb = GetComponent<Rigidbody>();
        //プレイヤーのアクション群も初期化
        if(playerAction == null)
            playerAction = new PlayerAction();

        playerAction.operatePlayer = this;

        action.Player.Move.performed += playerAction.OnMovePerformed;
        action.Player.Move.canceled += playerAction.OnMoveCancled;
        action.Player.NormalAttack.performed += playerAction.AttackInField;

        action.Enable();
    }
    
    //バトルーフェーズの
    public async UniTask Battle() {
        await UniTask.CompletedTask;
    }

    public override bool IsPlayer() {
        return true;
    }

    public async UniTask AddExp(int _addExp) {
        int exp = 0;
        exp += _addExp;
        await UniTask.CompletedTask;
    }
}
