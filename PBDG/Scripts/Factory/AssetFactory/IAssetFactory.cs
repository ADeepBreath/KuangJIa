using UnityEngine;
using System.Collections;

/// <summary>
/// 将Unity Asset实体化成GameObject的工工厂类别
/// </summary>
public abstract class IAssetFactory
{
    // 生成Soldier
    public abstract GameObject LoadSoldier(string AssetName);

    // 生成Enemy
    public abstract GameObject LoadEnemy(string AssetName);

    // 生成Weapon
    public abstract GameObject LoadWeapon(string AssetName);

    // 生成特效
    public abstract GameObject LoadEffect(string AssetName);

    // 生成AudioClip
    public abstract AudioClip LoadAudioClip(string ClipName);

    // 生成Sprite
    public abstract Sprite LoadSprite(string SpriteName);

}

/*
 * 使用Abstract Factory Patterny簡化版,
 * 讓GameObject的產生可以依Uniyt Asset放置的位置來載入Asset
 * 先實作放在Resource目錄下的Asset及Remote(Web Server)上的
 * 當Unity隨著版本的演進，也許會提供不同的載入方式，那麼我們就可以
 * 再先將一個IAssetFactory的子類別來因應變化
 */