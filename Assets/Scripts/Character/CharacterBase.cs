using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //[SerializeField]
    //private CharacterStatus status = null;
    public bool isDead { get; protected set; } = false;

    protected enum CharacterState {
        Invalid = -1,
        Field,
        Battle,

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

    }
    public abstract void Initilized();

    public abstract bool IsPlayer();
}
