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


public class PlayerAction{
    public Player operatePlayer = null;
    private bool isNormalAttack = false;
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

    //バトル中の行動選択
    public async UniTask Order() {
        while (true) {
            if (await NormalAttack())
                break;
        }
    }

    private async UniTask<bool> NormalAttack() {
        if (!isNormalAttack) return false;

        await UniTask.CompletedTask;
        return true;
    }
}
