using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractEvent;

    // 由 Input Actions 资源自动生成的输入包装类实例。
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        // 创建输入实例，并启用 Player 动作表中的所有输入动作。
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        // 启用 Player 动作表中的所有输入动作。
        inputActions.Player.Enable();
        // 订阅交互动作的触发事件。
        inputActions.Player.Interact.started += OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // 触发交互事件，通知其他系统玩家进行了交互操作。
        OnInteractEvent?.Invoke(this,EventArgs.Empty);
    }

    // 读取玩家当前的移动输入，并返回归一化后的二维方向。
    public Vector2 GetMovementVectorNormalized()
    {
        // Move 动作会把 WASD、方向键或手柄摇杆输入转换成 Vector2。
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
       
        // 归一化后向量长度最大为 1，避免斜向移动比单轴移动更快。
        return inputVector.normalized;
    }

}
