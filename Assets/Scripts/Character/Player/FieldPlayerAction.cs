/*
 * @class   PlayerAction    
 * @brief   プレイヤーのアクション群
 *
 */
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static CommonModule;


public class FieldPlayerAction{
    public FieldPlayer operatePlayer = null;
    //private bool isNormalAttack = false;
    public void OnMovePerformed(InputAction.CallbackContext _callback) {
        
        operatePlayer.moveDir.x = _callback.ReadValue<Vector2>().x;
        operatePlayer.moveDir.z = _callback.ReadValue<Vector2>().y;

    }

    public void OnMoveCancled(InputAction.CallbackContext _callback) {
        operatePlayer.moveDir = Vector3.zero;
    }
    public void AttackInField(InputAction.CallbackContext _callback) {
        
    }

    public void UseItem<T>() {
        
    }

    
}
