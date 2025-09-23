/*
 * @class   PlayerAction    
 * @brief   プレイヤーのアクション群
 *
 */
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class FieldPlayerAction {
    public FieldPlayer operatePlayer = null;
    private bool freeze;
    public AnimatorStateInfo animatorState;
    //private bool isNormalAttack = false;
    public void OnMovePerformed(InputAction.CallbackContext _callback) {
        if (!freeze) {
            operatePlayer.moveDir.x = _callback.ReadValue<Vector2>().x;
            operatePlayer.moveDir.z = _callback.ReadValue<Vector2>().y;
            operatePlayer.animator.SetBool("IsRun", true);
        }

    }

    public void OnMoveCancled(InputAction.CallbackContext _callback) {
        operatePlayer.rb.velocity = Vector3.zero;
        operatePlayer.moveDir = Vector3.zero;
        operatePlayer.animator.SetBool("IsRun", false);
    }
    public void AttackInField(InputAction.CallbackContext _callback) {
        operatePlayer.animator.SetTrigger("Attack");
        operatePlayer.rb.velocity = Vector3.zero;
        operatePlayer.moveDir = Vector3.zero;
    }
   
    public void UseItem<T>() {

    }



}
