using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPlayer : FieldCharacterBase
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject movePlayerObj = null;

    public bool isBattle { get; private set; } = false;
    [SerializeField]
    private Action action = null;
    public FieldPlayerAction playerAction = null;
    [SerializeField]
    private StageObject stgObj = null;
    public List<BattlePlayer> myParty = null;
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            stgObj.SetRespawnPosition(transform.position);
        }
    }

    void Start()
    {
        Initialize();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate() {
        rb.AddForce(moveDir * moveSpeed * 10f,ForceMode.Force);
        
    }

    public override void Initialize() {
        base.Initialize();

        FieldCameraManager.instance.SetTarget(this);

        //�C���v�b�g�A�N�V������������
        action = InputSystemManager.instance.inputActions;
        //���W�b�h�{�f�B��������
        rb = movePlayerObj.GetComponent<Rigidbody>();
        //�v���C���[�̃A�N�V�����Q��������
        if(playerAction == null)
            playerAction = new FieldPlayerAction();

        playerAction.operatePlayer = this;

        action.Player.Move.performed += playerAction.OnMovePerformed;
        action.Player.Move.canceled += playerAction.OnMoveCancled;
        action.Player.NormalAttack.performed += playerAction.AttackInField;

        action.Enable();
    }


}
