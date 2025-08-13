using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{

    // Start is called before the first frame update
    public bool isBattle { get; private set; } = false;
    [SerializeField]
    private Action action = null;
    [SerializeField]
    private PlayerAction playerAction = null;
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
        //�C���v�b�g�A�N�V������������
        action = InputSystemManager.instance.inputActions;
        //�v���C���[�̃A�N�V�����Q��������
        if(playerAction == null)
            playerAction = new PlayerAction();

        action.Player.Move.performed += playerAction.OnMovePerformed;
        action.Player.NormalAttack.performed += playerAction.Attack;
    }
}
