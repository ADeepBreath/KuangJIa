using UnityEngine;
using System.Collections;

/// <summary>
/// ��������
/// </summary>
public abstract class IWeaponFactory
{
    // ��������
    public abstract IWeapon CreateWeapon(ENUM_Weapon emWeapon);
}

