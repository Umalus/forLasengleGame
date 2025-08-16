/*
 * @class   PlayerAction    
 * @brief   �v���C���[�̃A�N�V�����Q
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
        var inputDir = _callback.ReadValue<Vector2>();
        operatePlayer.rb.AddForce(inputDir.normalized * Time.deltaTime);

    }

    public void AttackInField(InputAction.CallbackContext _callback) {
        
    }

    public void UseItem<T>() {
        
    }

    //�o�g�����̍s���I��
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
