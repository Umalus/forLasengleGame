using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FieldCharacterBase : MonoBehaviour
{
    public Rigidbody rb { get; protected set; } = null;
    public float moveSpeed = 10.0f;
    public Vector3 moveDir = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Initialize() {
        //transform.SetParent(SetRoot);
    }
}
