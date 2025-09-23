using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class FieldCameraManager : MonoBehaviour {
    public static FieldCameraManager instance = null;

    [Serializable]
    public class Paramater {
        [SerializeField, Header("�ǂ�������Ώ�")]
        public FieldCharacterBase targetObj = null;
        public Vector3 position;
        public Vector3 angles = new Vector3(10.0f, 0.0f, 0.0f);
        public float distance = 0.0f;
        public Vector3 offset;
    }

    private Action inputActions = null;

    [SerializeField, Header("���C���̃J����")]
    private Camera mainCamera = null;
    [SerializeField, Header("�e��p�����[�^�[")]
    private Paramater param;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Transform child;
    #region �J������]
    //�J������]�p�����o�ϐ�
    [SerializeField]
    private float yMax = 80.0f;
    [SerializeField]
    private float yMin = -20.0f;

    private float inputX;
    private float inputY;


    [SerializeField]
    private float rotateSpeed = 1.0f;
    #endregion
    //�}�E�X���X�}�z���ǂ���(�f�o�b�O�p)
    [SerializeField]
    private bool UseMouse = false;
    private void Awake() {
        instance = this;
    }

    private void Start() {
        Initialized();
    }

    private void Update() {

    }

    private void Initialized() {
        inputActions = InputSystemManager.instance.inputActions;

        inputActions.Player.Camera.performed += RotateCamera;
    }

   

    private void LateUpdate() {
        //�����ꂩ�̗v�f�������Ă��Ȃ�������
        if (parent == null || child == null || mainCamera == null) {
            return;
        }
        //���R�ɒǐ�
        if (param.targetObj != null) {
            param.position = param.targetObj.transform.position;
        }//Vector3.Lerp(
        //    a: param.position,
        //    b: param.targetObj.transform.position,
        //    t: Time.deltaTime * lerpTime);
        //�J��������]
        //param.angles = RotateCamera();


        // �p�����[�^���e��I�u�W�F�N�g�ɔ��f
        parent.position = param.position;
        parent.eulerAngles = param.angles;

        var childPos = child.localPosition;
        childPos.z = -param.distance;
        child.localPosition = childPos;

        mainCamera.transform.localPosition = param.offset;
    }

    /// <summary>
    /// �J������]
    /// </summary>
    /// <returns></returns>
    private void RotateCamera(InputAction.CallbackContext _callback) {
        inputX += _callback.ReadValue<Vector2>().x * rotateSpeed * Time.deltaTime;
        inputY -= _callback.ReadValue<Vector2>().y * rotateSpeed * Time.deltaTime;
        inputY = Mathf.Clamp(inputY, yMin, yMax);

        Quaternion rotation = Quaternion.Euler(inputY, inputX, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, param.offset.z) + param.targetObj.transform.position;

        param.angles = rotation.eulerAngles;
        param.position = position;

    }

    public void SetTarget(FieldCharacterBase _target) {
        param.targetObj = _target;
    }

}