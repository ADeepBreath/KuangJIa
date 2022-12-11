using UnityEngine;
using System.Collections;

// 从远程,將Unity Asset实例化成GameObject的工厂
public class RemoteAssetFactory : IAssetFactory
{

    // 生成Soldier
    public override GameObject LoadSoldier(string AssetName)
    {
        // 载入放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Characters/Soldier/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0); 
        return null;
    }

    // 生成Enemy
    public override GameObject LoadEnemy(string AssetName)
    {
        // 载入放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Characters/Enemy/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 生成Weapon
    public override GameObject LoadWeapon(string AssetName)
    {
        // 载入放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Weapons/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 生成特效
    public override GameObject LoadEffect(string AssetName)
    {
        // 载入放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Effects/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 生成AudioClip
    public override AudioClip LoadAudioClip(string ClipName)
    {
        // 载入放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Audios/"+ClipName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 生成Sprite
    public override Sprite LoadSprite(string SpriteName)
    {
        // 载入放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Sprites/"+SpriteName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }
}
