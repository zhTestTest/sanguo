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
public sealed partial class UIData_Race :  Bright.Config.BeanBase 
{
    public UIData_Race(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Bg = _buf.ReadString();
        Title = _buf.ReadString();
        RaceIndex = _buf.ReadInt();
        PlayerPos = _buf.ReadUnityVector3();
        UnlockStarNum = _buf.ReadInt();
        PostInit();
    }

    public static UIData_Race DeserializeUIData_Race(ByteBuf _buf)
    {
        return new Deer.UIData_Race(_buf);
    }

    /// <summary>
    /// 唯一标识
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 背景图片
    /// </summary>
    public string Bg { get; private set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    /// 场景索引
    /// </summary>
    public int RaceIndex { get; private set; }
    /// <summary>
    /// 角色初始位置
    /// </summary>
    public UnityEngine.Vector3 PlayerPos { get; private set; }
    /// <summary>
    /// 解锁的星星个数
    /// </summary>
    public int UnlockStarNum { get; private set; }

    public const int __ID__ = -1917877934;
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
        + "Bg:" + Bg + ","
        + "Title:" + Title + ","
        + "RaceIndex:" + RaceIndex + ","
        + "PlayerPos:" + PlayerPos + ","
        + "UnlockStarNum:" + UnlockStarNum + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}