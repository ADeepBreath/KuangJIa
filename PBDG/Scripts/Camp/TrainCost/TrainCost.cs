using UnityEngine;
/// <summary>
/// 训练费用计算
/// </summary>
public class TrainCost : ITrainCost
{
    public TrainCost() { }

    public override int GetTrainCost(ENUM_Soldier emSoldier, int CampLv, ENUM_Weapon emWeapon)
    {
        int Cost = 0;
        // 根据兵种
        switch (emSoldier)
        {
            case ENUM_Soldier.Rookie:
                Cost = 5;
                break;
            case ENUM_Soldier.Sergeant:
                Cost = 7;
                break;
            case ENUM_Soldier.Captain:
                Cost = 10;
                break;
            default:
                Debug.LogWarning("没有定义[" + emSoldier + "]的训练花费");
                break;
        }

        // 根据武器
        switch (emWeapon)
        {
            case ENUM_Weapon.Gun:
                Cost += 5;
                break;
            case ENUM_Weapon.Rifle:
                Cost += 7;
                break;
            case ENUM_Weapon.Rocket:
                Cost += 10;
                break;
        }

        // 加等級
        Cost += (CampLv - 1) * 2;
        return Cost;

    }
}