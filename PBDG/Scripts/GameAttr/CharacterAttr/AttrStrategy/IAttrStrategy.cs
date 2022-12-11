using UnityEngine;
using System.Collections;

// 角色数值计算
public abstract class IAttrStrategy
{
	// 初始的數值
	public abstract void InitAttr( ICharacterAttr CharacterAttr );
	
	// 攻击加成
	public abstract int GetAtkPlusValue( ICharacterAttr CharacterAttr );
	
	// 取得减伤
	public abstract int GetDmgDescValue( ICharacterAttr CharacterAttr );
}
