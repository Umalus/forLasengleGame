using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPlayer : FieldCharacterBase
{
    // Start is called before the first frame update
    public Animator animator = null;
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
        //XZ���ʂ̒P�ʃx�N�g���擾
        Vector3 cameraForward = Vector3.Scale(FieldCameraManager.instance.GetMainCamera().transform.forward,new Vector3(1.0f,0.0f,1.0f));

        Vector3 moveForward = cameraForward * moveDir.z + FieldCameraManager.instance.GetMainCamera().transform.right * moveDir.x;
        if (moveDir.sqrMagnitude >= Mathf.Epsilon)
            transform.position += moveForward * moveSpeed * Time.deltaTime;

        if(moveForward != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveForward);

    }

    public override void Initialize() {
        base.Initialize();

        FieldCameraManager.instance.SetTarget(this);

        //�C���v�b�g�A�N�V������������
        action = InputSystemManager.instance.inputActions;
        //���W�b�h�{�f�B��������
        rb = GetComponent<Rigidbody>();
        //�v���C���[�̃A�N�V�����Q��������
        if(playerAction == null)
            playerAction = new FieldPlayerAction();

        playerAction.operatePlayer = this;
        playerAction.animatorState = animator.GetCurrentAnimatorStateInfo(0);

        action.Player.Move.performed += playerAction.OnMovePerformed;
        action.Player.Move.canceled += playerAction.OnMoveCancled;
        action.Player.NormalAttack.performed += playerAction.AttackInField;

        action.Enable();
    }


}
