using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 加载状态
/// </summary>
public class LoadState : FSMState
{
    public string loadSceneName;

    Slider loadSlider;
    AsyncOperation thisAsync,loadAsync;

    public LoadState(FSMSystem fsm) : base(fsm)
    {
        this.fsm = fsm;
        stateName = "LoadScene";
    }

    public override void DoBeforeEntering()
    {
        thisAsync = SceneManager.LoadSceneAsync("LoadScene");
        thisAsync.completed += (ao) =>
        {
            // 获取进度条，设置初始值
            loadSlider = GameObject.Find("LoadSlider").GetComponent<Slider>();
            loadSlider.value = 0;

            //异步加载目标场景
            loadAsync = SceneManager.LoadSceneAsync(loadSceneName);
            loadAsync.allowSceneActivation = false;
        };
       
    }
    public override void Act()
    {
        if(!thisAsync.isDone)
        {
            return;
        }

        //异步加载进程
        if(loadAsync.progress<0.9f)
        {
            loadSlider.value= loadAsync.progress;
        }
        else
        {
            loadSlider.value=1;
            loadAsync.allowSceneActivation = true;
        }
    }

    public override void Reason()
    {
        if (!thisAsync.isDone)
        {
            return;
        }

        //加载完成切换状态
        if (loadAsync.isDone)
        {
            fsm.PerformTransition(loadSceneName);
        }
    }
}
