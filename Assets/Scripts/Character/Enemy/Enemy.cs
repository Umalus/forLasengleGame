using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    public override void Initilized() {
        
    }

    public override bool IsPlayer() {
        return false;
    }
}
