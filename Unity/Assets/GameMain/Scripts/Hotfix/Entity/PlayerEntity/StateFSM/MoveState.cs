using System;
using UnityEngine;



public enum MoveMode
{
    Null = 0,
    /// <summary>
    /// 往前走
    /// </summary>
    Forward = 1,
    /// <summary>
    /// 往前跑
    /// </summary>
    ForwardRun = 2,
    /// <summary>
    /// 往后退
    /// </summary>
    Back = 3,
    /// <summary>
    /// 往后跑
    /// </summary>
    BackRun = 4,
    /// <summary>
    /// 走路进入跳跃
    /// </summary>
    Jump = 5,
    /// <summary>
    /// 跑步进入跳跃
    /// </summary>
    JumpRun = 6,
}

public enum MoveType
{
    Null = 0,
    /// <summary>
    /// 向点运动
    /// </summary>
    MoveToPos = 1,
    /// <summary>
    /// 向角色运动
    /// </summary>
    MoveToTarget = 2,
    /// <summary>
    /// 向方向运动
    /// </summary>
    MoveToDir = 3,
    /// <summary>
    /// 向点运动(不带寻路，用于网络同步情况)
    /// </summary>
    TransToPos = 4,
}

public enum MoveDirection
{
    Right = 0,
    Left = 1,
    Up = 2,
    Down = 3,
    Back = 4,
    Forward = 5,
}

public enum SceneCharacterFace
{
    Free = 0,
    LeftAndRight = 1,
    Left = 2,
    Right = 3,
}

public class MoveState : State
{
    private MoveMode m_MoveMode = MoveMode.Forward;
    public MoveMode MoveMode
    {
        get { return m_MoveMode; }
        set { m_MoveMode = value; }
    }
    private MoveType m_MoveType = MoveType.Null;
    /// <summary>
    /// 移动速度的倍率 （移动速度 *倍率 = 最终移动速度）
    /// </summary>
    private float m_MoveSpeed = 0;
    /// <summary>
    /// 目标点
    /// </summary>
    protected Vector3 m_MovePos;
    /// <summary>
    /// 运动方向
    /// </summary>
    protected Vector3 m_MoveDir;
    /// <summary>
    /// 移动最终目标点
    /// </summary>
    public Vector3 m_MovePoint;
    /// <summary>
    /// 目标
    /// </summary>
    protected PlayerEntity m_Target;
    /// <summary>
    /// 距离目标停止距离
    /// </summary>
    protected float m_StopDis;
    /// <summary>
    /// 到达目标停止回调
    /// </summary>
    protected Action<PlayerEntity, object> m_ReachCallback;

    /// <summary>
    /// 移动路径点的索引号
    /// </summary>
    protected int m_MovePathIndex;
    /// <summary>
    /// 当前动画名字
    /// </summary>
    protected string m_CurMoveAniName;
    protected string m_MoveName = "Walk";
    protected string m_RunName = "Run";


    public override void SetParam(MessengerInfo messengerInfo)
    {
        if (messengerInfo != null)
        {
            m_StopDis = 0;
            m_ReachCallback = null;
            m_MoveType = (MoveType)messengerInfo.param1;
            if (m_MoveType != MoveType.Null)
            {
                switch (m_MoveType)
                {
                    case MoveType.MoveToPos:
                        m_MovePos = (Vector3)messengerInfo.param2;
                        break;
                    case MoveType.MoveToTarget:
                        m_Target = (PlayerEntity)messengerInfo.param2;
                        break;
                    case MoveType.MoveToDir:
                        m_MoveDir = (Vector3)messengerInfo.param2;
                        break;
                    case MoveType.TransToPos:
                        m_MovePos = (Vector3)messengerInfo.param2;
                        break;
                    default:
                        break;
                }
            }

            Type type = messengerInfo.param3.GetType();
            if (type == typeof(double))
            {
                double speed = (double)messengerInfo.param3;
                m_MoveSpeed = (float)speed;
            }
            else if (type == typeof(float))
            {
                float speed = (float)messengerInfo.param3;
                m_MoveSpeed = speed;
            }
            else if (type == typeof(int))
            {
                int speed = (int)messengerInfo.param3;
                m_MoveSpeed = speed;
            }
            if (messengerInfo.param4 != null)
            {
                Type stopDisType = messengerInfo.param4.GetType();
                if (stopDisType == typeof(double))
                {
                    double StopDis = (double)messengerInfo.param4;
                    m_StopDis = (float)StopDis;
                }
                else if (stopDisType == typeof(float))
                {
                    float StopDis = (float)messengerInfo.param4;
                    m_StopDis = StopDis;
                }
                else if (stopDisType == typeof(int))
                {
                    int StopDis = (int)messengerInfo.param4;
                    m_StopDis = StopDis;
                }
            }
            m_ReachCallback = (Action<PlayerEntity, object>)messengerInfo.param5;
        }
    }

    protected internal override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);
        m_MoveMode = MoveMode.Forward;
        //Log.Info("实体'{0}'  进入到Move状态！", stateController.Owner.Entity.Id);

        if (m_MoveMode == MoveMode.ForwardRun)
        {
            m_CurMoveAniName = m_RunName;
        }
        else
        {
            m_CurMoveAniName = m_MoveName;
        }
        if (m_MoveType == MoveType.MoveToPos || m_MoveType == MoveType.MoveToTarget)
        {
            m_MovePathIndex = 0;
        }
    }

    protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (CanMoveDeActive())
        {
            m_ReachCallback?.Invoke(m_StateController.Owner, null);
            m_ReachCallback = null;
        }
        bool IsSyncPlayerBaseInfo = false;
        if (m_MoveType == MoveType.MoveToDir)
        {
            if (m_StateController.Owner.IsOwner)
            {
                IsSyncPlayerBaseInfo = m_MoveDir != m_StateController.Owner.JoyStickDirection;
                m_MoveDir = m_StateController.Owner.JoyStickDirection;
            }
            MoveToDir();
        }
        else if (m_MoveType == MoveType.TransToPos)
        { }
        else
        {
              
        }
    }
    protected internal override void OnLeave()
    {
        base.OnLeave();
        m_MovePos = Vector3.zero;
        m_MoveDir = Vector3.zero;
        m_Target = null;
        m_MoveType = MoveType.Null;
    }

    protected void MoveToDir()
    {
        Vector3 faceDir = m_MoveDir;
        float angle = Vector3.Angle(Vector3.right, faceDir); //求出两向量之间的夹角
        Vector3 normal = Vector3.Cross(faceDir, Vector3.right);//叉乘求出法线向量
        angle *= Mathf.Sign(Vector3.Dot(normal, Vector3.up));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向
                                                                //Log.ColorInfo(ColorType.black, angle.ToString());

        bool twoDir = m_StateController.Owner.UserTwoDirction;
        float faceDirAngle = m_StateController.Owner.MoveDirection == MoveDirection.Left ? -90 : 90;

        if (!(m_StateController.Owner.UseCustomFace || m_StateController.Owner.UseFaceType == SceneCharacterFace.Free))
        {
            faceDir = m_StateController.Owner.MoveDirection == MoveDirection.Left ? Vector3.left : Vector3.right;
        }

        if (twoDir)//2个方向的时候
        {
            if (angle > -90 && angle < 90)
            {
                if (m_StateController.Owner.UseFaceType == SceneCharacterFace.Left)
                {
                    m_MoveMode = MoveMode.Back;
                    m_StateController.Owner.MoveDirection = MoveDirection.Left;
                    faceDirAngle = -90;
                    faceDir = Vector3.left;
                }
                else
                {
                    m_StateController.Owner.MoveDirection = MoveDirection.Right;
                    faceDirAngle = 90;
                }
            }
            else if (angle < -90 || angle > 90)
            {
                if (m_StateController.Owner.UseFaceType == SceneCharacterFace.Right)
                {
                    m_MoveMode = MoveMode.Back;
                    m_StateController.Owner.MoveDirection = MoveDirection.Right;
                    faceDirAngle = 90;
                    faceDir = Vector3.right;
                }
                else
                {
                    m_StateController.Owner.MoveDirection = MoveDirection.Left;
                    faceDirAngle = -90;
                }
            }
        }
        else//左下 -135  右下 135  左上 -45  右上 45  左-90  右90
        {
            if (angle > 150 && angle < 180)//左
            {
                m_StateController.Owner.MoveDirection = MoveDirection.Left;
                faceDirAngle = -90;
                faceDir = Vector3.left;
            }
            else if (angle > -180 && angle < -150)//左
            {
                m_StateController.Owner.MoveDirection = MoveDirection.Left;
                faceDirAngle = -90;
                faceDir = Vector3.left;
            }
            else if (angle >= -150 && angle <= -105)//左下
            {
                m_StateController.Owner.MoveDirection = MoveDirection.Left;
                faceDirAngle = -135;
                faceDir = new Vector3(-1, 0, -1);
            }
            else if (angle >= 105 && angle <= 150)//左上
            {
                m_StateController.Owner.MoveDirection = MoveDirection.Left;
                faceDirAngle = -45;
                faceDir = new Vector3(-1, 0, 1);
            }
            else if (angle > -30 && angle < 30)//右
            {
                m_StateController.Owner.MoveDirection = MoveDirection.Right;
                faceDirAngle = 90;
                faceDir = Vector3.right;
            }
            else if (angle >= 30 && angle <= 75)//右上
            {
                m_StateController.Owner.MoveDirection = MoveDirection.Right;
                faceDirAngle = 45;
                faceDir = new Vector3(1, 0, 1);
            }
            else if (angle >= -75 && angle <= -30)//右下
            {
                m_StateController.Owner.MoveDirection = MoveDirection.Right;
                faceDirAngle = 135;
                faceDir = new Vector3(1, 0, -1);
            }
            else if (angle > -105 && angle < -75)//下
            {
                m_MoveDir = Vector3.back;
            }
            else if (angle > 75 && angle < 105)//上
            {
                m_MoveDir = Vector3.forward;
            }
        }
        MoveAndSetFaceDir(faceDir);
    }
    protected void MoveAndSetFaceDir(Vector3 faceDir)
    {
        m_StateController.Owner.RoleControllerManager.OnMove(m_MoveDir, GetRealMoveSpeed());
        if (m_CurMoveAniName != m_RunName && m_MoveMode == MoveMode.ForwardRun)
        {
            m_CurMoveAniName = m_RunName;
            m_StateController.Owner.Animator.CrossFade(m_CurMoveAniName, 0.05f);
        }
        else if (m_CurMoveAniName != m_MoveName && m_MoveMode != MoveMode.ForwardRun)
        {
            m_CurMoveAniName = m_MoveName;
            m_StateController.Owner.Animator.CrossFade(m_CurMoveAniName, 0.05f);
        }
        m_StateController.Owner.SetFaceDir(faceDir);
    }

    protected float GetRealMoveSpeed(MoveMode moveMode = MoveMode.Null)
    {
        moveMode = moveMode == MoveMode.Null ? m_MoveMode : moveMode;
        float speed = 0;
        switch (moveMode)
        {
            case MoveMode.Null:
                break;
            case MoveMode.Forward:
                speed = m_StateController.Owner.PlayerEntityData.WalkSpeed;
                break;
            case MoveMode.ForwardRun:
                speed = m_StateController.Owner.PlayerEntityData.RunSpeed;
                break;
            case MoveMode.Back:
                speed = m_StateController.Owner.PlayerEntityData.BackMoveSpeed;
                break;
            default:
                break;
        }
        return speed * m_MoveSpeed;
    }
    /// <summary>
    /// 能否停止移动
    /// </summary>
    /// <returns></returns>
    protected bool CanMoveDeActive()
    {
        Vector3 curPos = m_StateController.Owner.CachedTransform.position;
        switch (m_MoveType)
        {
            case MoveType.MoveToPos:
            case MoveType.MoveToTarget:
                  
                break;
            case MoveType.MoveToDir:
                return (m_StateController.Owner.IsOwner && !m_StateController.Owner.IsJoyStickControl) ||
                    m_MoveDir == Vector3.zero;
            case MoveType.TransToPos:
                break;
            default:
                break;
        }
        return false;
    }

}
