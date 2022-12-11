using UnityEngine;
using System.Collections;

// UI观测Enemey阵亡事件
public class EnemyKilledObserverUI : IGameEventObserver 
{
	private EnemyKilledSubject m_Subject = null;
	private BattleGame m_BattleGame = null;

	public EnemyKilledObserverUI(BattleGame battleGame  )
	{
        m_BattleGame = battleGame;
	}

	// 設定觀察的主題
	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as EnemyKilledSubject;
	}

	// 通知Subject被更新
	public override void Update()
	{
		//Debug.Log("EnemyKilledObserverUI.Update: Count["+ m_Subject.GetKilledCount() +"]");
		if(m_BattleGame != null)
            m_BattleGame.ShowGameMsg("敌方单位阵亡");

	}

}
