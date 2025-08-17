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
    public abstract void Initilized();

    public abstract bool IsPlayer();
}
