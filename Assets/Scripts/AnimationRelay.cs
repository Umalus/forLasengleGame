using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRelay : MonoBehaviour
{

    public async void ExecuteEffect(int _index,Transform _firePoint) {
        await EffectManager.instance.ExecuteEffect(_index,_firePoint);
    }
}
