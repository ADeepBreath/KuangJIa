using UnityEngine;
using System.Collections;

/// <summary>
///  从本地將Unity Asset实体化化成GameObject的工厂
/// </summary>
public class LocalAssetFactory : IAssetFactory
{
    // 生成Soldier
    public override GameObject LoadSoldier(string AssetName)
    {
        // 载入放在本地裝置的Asset示意
        //string FilePath = "D:/xxx/Characters/Soldier/"+AssetName+".assetbundle";
        // 执行载入
        return null;
    }

    // 生成Enemy
    public override GameObject LoadEnemy(string AssetName)
    {
        //载入放在本地装置上的Asset示意
        //string RemotePath = "D:/xxx/Characters/Enemy/"+AssetName+".assetbundle";
        //执行载入
        return null;
    }

    //生成Weapon
    public override GameObject LoadWeapon(string AssetName)
    {
        // 载入放在本地装置上的Asset示意
        //string RemotePath = "D:/xxx/Weapons/"+AssetName+".assetbundle";
        // 执行载入
        return null;
    }

    // 生成特效
    public override GameObject LoadEffect(string AssetName)
    {
        // 载入放在本地裝置上的Asset示意
        //string RemotePath = "D:/xxx/Effects/"+AssetName+".assetbundle";
        //执行载入
        return null;
    }

    // 生成AudioClip
    public override AudioClip LoadAudioClip(string ClipName)
    {
        // 载入放在本地裝置上的Asset示意
        //string RemotePath = "D:/xxx/Audios/"+AssetName+".assetbundle";
        //执行载入
        return null;
    }

    // 生成Sprite
    public override Sprite LoadSprite(string SpriteName)
    {
        // 载入放在本地裝置上的Asset示意
        //string RemotePath = "D:/xxx/Sprites/"+SpriteName+".assetbundle";
        //执行载入
        return null;
    }
}

