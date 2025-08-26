using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetUI : MonoBehaviour
{
    [SerializeField]
    private GameObject targetImage = null;

    public void Update() {
        targetImage.transform.LookAt(Camera.main.transform);
    }

    public void ShowMark() {
        targetImage.SetActive(true);
    }
    public void HideMark() {
        targetImage.SetActive(false);
    }
    
}
