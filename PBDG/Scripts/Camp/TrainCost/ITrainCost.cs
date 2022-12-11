/// <summary>
/// 训练费用计算
/// </summary>
public abstract class ITrainCost
{
    public abstract int GetTrainCost(ENUM_Soldier emSoldier, int CampLv, ENUM_Weapon emWeapon);
}
