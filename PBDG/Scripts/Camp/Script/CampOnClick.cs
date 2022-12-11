

using UnityEngine;

/// <summary>
/// 兵营点击成功后通知显示
/// </summary>
public class CampOnClick : MonoBehaviour
{
    public ICamp theCamp = null;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void OnClick()
    {
        //显示兵营信息
        BattleGame.Instance.ShowCampInfo(theCamp);
    }
}

