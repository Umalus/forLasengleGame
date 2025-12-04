using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

public class FieldEnemy : FieldCharacterBase {
    [SerializeField]
    private float SearchAngle = 0.0f;
    [SerializeField]
    private float SearchDistance = 0.0f;
    [SerializeField]
    private SphereCollider SearchCollider = null;
    [SerializeField]
    private Animator anim = null;
    public List<BattleEnemy> myParty = null;
    // Start is called before the first frame update
    void Start() {
        Initialize();
        SearchCollider.radius = SearchDistance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

    }

    public override void Initialize() {
        base.Initialize();
    }

    public void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            Vector3 posDelta = other.transform.position - transform.position;

            float angle = Vector3.Angle(transform.forward, posDelta);
            //ŽZo‚µ‚½Šp“x‚ªŽw’è‚ÌŠp“x‚ð’´‚¦‚Ä‚¢‚½‚çreturn
            if (angle >= SearchAngle) return;
            //Ray‚ª“–‚½‚ç‚È‚¯‚ê‚Îreturn
            if (!Physics.Raycast(transform.position, posDelta, out RaycastHit hit, SearchDistance)) return;
            Debug.DrawRay(transform.position, posDelta, Color.red, SearchDistance);
            if (hit.collider == other) {
                Debug.Log("Find!!");
                ChasePlayer(other.transform.position);
            }
        }
    }

    public async void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            List<BattlePlayer> playerParty = collision.gameObject.GetComponent<FieldPlayer>().myParty;

            BattlePhase.SetCharacter(playerParty, myParty);
            BattlePhase.fieldEnemy = gameObject;
            Destroy(this);
            await MainGamePart.ChangeGamePhase(eGamePhase.Battle);
        }
    }

    private void ChasePlayer(Vector3 _direction) {
        transform.LookAt(_direction);
        anim.SetTrigger("Run");

        rb.AddForce(transform.forward * 10f);
    }
}
