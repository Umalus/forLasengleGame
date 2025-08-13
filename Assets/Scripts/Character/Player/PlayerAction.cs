/*
 * @class   PlayerAction    
 * @brief   プレイヤーのアクション群
 *
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static CommonModule;


public class PlayerAction{
    public Player operatePlayer = null;

    public void OnMovePerformed(InputAction.CallbackContext _callback) {
        var inputDir = _callback.ReadValue<Vector2>();
        operatePlayer.rb.AddForce(inputDir.normalized * Time.deltaTime);

    }

    public void Attack(InputAction.CallbackContext _callback) {
        if (operatePlayer.isBattle) {

        }
        else {

        }
    }

    public void UseItem<T>() {
        
    }


}
