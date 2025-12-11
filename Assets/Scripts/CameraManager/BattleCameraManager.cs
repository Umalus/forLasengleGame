using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraManager : MonoBehaviour
{


    public static BattleCameraManager instance = null;

    [Serializable]
    public class Paramater {
        public Vector3 position;
        public Vector3 angles = new Vector3(10.0f, 0.0f, 0.0f);
        public float distance = 0.0f;
        public Vector3 offset;
    }
    [SerializeField, Header("メインのカメラ")]
    private Camera mainCamera = null;
    [SerializeField, Header("各種パラメーター")]
    private Paramater param;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Transform child;
    [SerializeField]
    private Transform CameraOriginPos;

    private readonly Vector3 CAMERA_ANGLE_OFFSET_PLAYERTURN;
    private readonly Vector3 CAMERA_ANGLE_OFFSET_ENEMYTURN;
    private bool battleStart = false;
    private void Awake() {
        instance = this;
    }
    BattleCameraManager() {
        CAMERA_ANGLE_OFFSET_PLAYERTURN = new Vector3(0.0f,-15.0f,0.0f);
        CAMERA_ANGLE_OFFSET_ENEMYTURN = new Vector3(15.0f, -15.0f, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        //いずれかの要素が入っていなかったら
        if (parent == null || child == null || mainCamera == null) {
            return;
        }

        // パラメータを各種オブジェクトに反映
        parent.position = param.position;
        parent.eulerAngles = param.angles;

        var childPos = child.localPosition;
        childPos.z = -param.distance;
        child.localPosition = childPos;

        mainCamera.transform.localPosition = param.offset;
    }

    public void CameraInit() {
        param.position = CameraOriginPos.position;
        param.angles = CAMERA_ANGLE_OFFSET_ENEMYTURN;
    }

    public async void CameraInit(BattleCharacterBase _actionCharacter) {
        Transform cameraRoot = _actionCharacter.GetComponent<BattlePlayer>().cameraRoot;

        ForcusActionCharacter(cameraRoot);

        if (!battleStart) {
            battleStart = true;
            await FadeManager.instance.FadeIn();
        }
        
    }

    private async UniTask CameraAnimation() {


        await UniTask.CompletedTask;
    }

    private void ForcusActionCharacter(Transform _cameraRoot) {
        //位置を変更
        param.position = _cameraRoot.position;
        param.angles = CAMERA_ANGLE_OFFSET_PLAYERTURN;
    }
}
