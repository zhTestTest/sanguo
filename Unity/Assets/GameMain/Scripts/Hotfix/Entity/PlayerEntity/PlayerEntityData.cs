using System.Collections.Generic;

public class PlayerEntityData : EntityData
{
    public float ConstantSpeed { get; set; } = 0;
    public bool UnUseGravity { get; set; } = true;
    public float WalkSpeed { get; set; } = 2;
    public float RunSpeed { get; set; } = 4;
    public float BackMoveSpeed { get; set; } = 3;
    public float TurningSpeed { get; set; } = 5;
    public float Gravity { get; set; } = 10;
    public float JumpPower { get; set; } = 4;
    public bool JumpCanMove { get; set; } = true;

    public List<string> AIStates = new List<string> { };

    public int InitAIStateId = 0;

    public float Radius = 0.5f;


    public long serverPlayerId = 0;
    public string username = string.Empty;
    public string password = string.Empty;

    public PlayerEntityData(int entityId, int typeId, string groupName, string assetName) : base(entityId, typeId, groupName, assetName)
    {

    }
}
