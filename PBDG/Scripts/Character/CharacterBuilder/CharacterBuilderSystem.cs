using UnityEngine;
using System.Collections.Generic;

/// <summary>
///  利用Builder界面來建造物件
///  角色建造系统
/// </summary>
public class CharacterBuilderSystem : IGameSystem
{
    private int m_GameObjectID = 0;

    public CharacterBuilderSystem(BattleGame battleGame) : base(battleGame)
    { }

    public override void Initialize()
    { }

    public override void Update()
    { }


    // 建立 
    public void Construct(ICharacterBuilder theBuilder)
    {
        // 利用Builder产生各部份加入Product中
        theBuilder.LoadAsset(++m_GameObjectID);
        theBuilder.AddOnClickScript();
        theBuilder.AddWeapon();
        theBuilder.SetCharacterAttr();
        theBuilder.AddAI();

        // 加入管理器内
        theBuilder.AddCharacterSystem(m_battleGame);
    }
}
