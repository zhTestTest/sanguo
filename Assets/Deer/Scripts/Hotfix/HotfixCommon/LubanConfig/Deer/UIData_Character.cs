//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace cfg.Deer
{
public sealed partial class UIData_Character :  Bright.Config.BeanBase 
{
    public UIData_Character(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Name = _buf.ReadString();
        PlayerPos = _buf.ReadUnityVector3();
        PlayerScale = _buf.ReadUnityVector3();
        Speed = _buf.ReadFloat();
        DamageResistance = _buf.ReadFloat();
        BoosterPower = _buf.ReadFloat();
        BoosterDuration = _buf.ReadFloat();
        BoosterCooldown = _buf.ReadFloat();
        PostInit();
    }

    public static UIData_Character DeserializeUIData_Character(ByteBuf _buf)
    {
        return new Deer.UIData_Character(_buf);
    }

    /// <summary>
    /// 唯一标识
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// UI预制体路径
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 模型位置
    /// </summary>
    public UnityEngine.Vector3 PlayerPos { get; private set; }
    /// <summary>
    /// 模型缩放
    /// </summary>
    public UnityEngine.Vector3 PlayerScale { get; private set; }
    /// <summary>
    /// 移动速度
    /// </summary>
    public float Speed { get; private set; }
    /// <summary>
    /// 伤害抗性
    /// </summary>
    public float DamageResistance { get; private set; }
    /// <summary>
    /// 助推器强度
    /// </summary>
    public float BoosterPower { get; private set; }
    /// <summary>
    /// 助推器持续时间
    /// </summary>
    public float BoosterDuration { get; private set; }
    /// <summary>
    /// 助推器冷却时间
    /// </summary>
    public float BoosterCooldown { get; private set; }

    public const int __ID__ = 768896328;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "PlayerPos:" + PlayerPos + ","
        + "PlayerScale:" + PlayerScale + ","
        + "Speed:" + Speed + ","
        + "DamageResistance:" + DamageResistance + ","
        + "BoosterPower:" + BoosterPower + ","
        + "BoosterDuration:" + BoosterDuration + ","
        + "BoosterCooldown:" + BoosterCooldown + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}