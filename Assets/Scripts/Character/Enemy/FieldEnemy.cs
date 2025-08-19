using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using static GameEnum;

public class FieldEnemy : FieldCharacterBase
{
    [SerializeField]
    private float SearchAngle = 0.0f;
    [SerializeField]
    private float SearchDistance = 0.0f;
    [SerializeField]
    private SphereCollider SearchCollider = null;
    // Start is called before the first frame update
    void Start()
    {
        SearchCollider.radius = SearchDistance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other){
        if (other.CompareTag("Player")) {
            Vector3 posDelta = other.transform.position - transform.position;
            
            float angle = Vector3.Angle(transform.forward, posDelta);
            if(angle < SearchAngle) {
                
                if (Physics.Raycast(transform.position,posDelta,out RaycastHit hit,SearchDistance)) {
                    Debug.DrawRay(transform.position, posDelta, Color.red, SearchDistance);
                    if (hit.collider == other) {
                        Debug.Log("Find!!");

                        ChasePlayer(other.transform.position);
                    }
                }
            }
        }
    }

    private async void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            await MainGamePart.ChangeGamePhase(eGamePhase.Battle);
        }
    }

    private void ChasePlayer(Vector3 _direction) {
        transform.LookAt(_direction);

        rb.AddForce(transform.forward * 10f);
    }
}
