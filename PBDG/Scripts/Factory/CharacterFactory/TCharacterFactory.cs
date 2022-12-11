using UnityEngine;
using System.Collections;

/// <summary>
/// 产生游戏角色的工厂(Generic Method)
/// </summary>
public interface TCharacterFactory_Generic
{
	ISoldier CreateSoldier<T>(ENUM_Weapon emWeapon, int Lv, Vector3 SpawnPosition) where T: ISoldier,new();
	IEnemy 	 CreateEnemy<T>  (ENUM_Weapon emWeapon, Vector3 SpawnPosition, Vector3 AttackPosition) where T: IEnemy,new();
}

