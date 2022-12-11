using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraManager : Singleton<VirtualCameraManager>
{

    public Dictionary<string, CinemachineVirtualCamera> virtualCameras = new Dictionary<string, CinemachineVirtualCamera>();
    string activeName= "EntranceVC";

    // Start is called before the first frame update
    public void Start()
    {
        GameObject VC = GameObject.Find("VC");
        for (int i = 0; i < VC.transform.childCount; i++)
        {
            CinemachineVirtualCamera virtualCamera = VC.transform.GetChild(i).GetComponent<CinemachineVirtualCamera>();
            virtualCameras.Add(virtualCamera.gameObject.name, virtualCamera);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    /// <summary>
    /// 选择虚拟相机并设置对象
    /// </summary>
    /// <param name="go"></param>
    /// <param name="cm"></param>
    public void SelectVC(string VCName, Transform go)
    {
        virtualCameras[activeName].gameObject.SetActive(false);
        virtualCameras[VCName].gameObject.SetActive(true);
        activeName = VCName;
        virtualCameras[VCName].LookAt = go;
        virtualCameras[VCName].Follow = go;
    }
    /// <summary>
    /// 选择虚拟相机
    /// </summary>
    /// <param name="VCName"></param>
    public void SelectVC(string VCName)
    {
        virtualCameras[activeName].gameObject.SetActive(false);
        virtualCameras[VCName].gameObject.SetActive(true);
        activeName = VCName;
    }
}
