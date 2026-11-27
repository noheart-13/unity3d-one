using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractEvent;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause
    }

    // 由 Input Actions 资源自动生成的输入包装类实例。
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        Instance = this;
        // 创建输入实例，并启用 Player 动作表中的所有输入动作。
        inputActions = new InputSystem_Actions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)){
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        Debug.Log(GetBindingText(Binding.Move_Up));
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new InputSystem_Actions();
        }

        // 启用 Player 动作表中的所有输入动作。
        inputActions.Player.Interact.performed += OnInteract;
        inputActions.Player.InteractAlternates.performed += InteractAlternate_performed;
        // 订阅完成后再启用 Player 动作表中的所有输入动作。
        inputActions.Player.Enable();
        inputActions.Player.Pause.performed += Pause_performed; 
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        if (inputActions == null)
        {
            return;
        }

        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.InteractAlternates.performed -= InteractAlternate_performed;
        inputActions.Player.Pause.performed -= Pause_performed;
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        inputActions?.Dispose();
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
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
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.Move_Up:
                return inputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Down:
                return inputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Move_Left:
                return inputActions.Player.Move.bindings[6].ToDisplayString();
            case Binding.Move_Right:
                return inputActions.Player.Move.bindings[8].ToDisplayString();
            case Binding.Interact:
                return inputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return inputActions.Player.InteractAlternates.bindings[1].ToDisplayString();
            case Binding.Pause:
                return inputActions.Player.Pause.bindings[0].ToDisplayString();
            default:
                return null;
        }
    }
    public void RebindBinding(Binding binding, Action onComplete)
    {

        // Implementation for rebinding a specific binding
        inputActions.Player.Disable();
        InputAction inputAction;
        int bindingIndex ;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
               inputAction = inputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Down:
                inputAction = inputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Move_Left:
                inputAction = inputActions.Player.Move;
                bindingIndex = 6;
                break;
            case Binding.Move_Right:
                inputAction = inputActions.Player.Move;
                bindingIndex = 8;
                break;
            case Binding.Interact:
                inputAction = inputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = inputActions.Player.InteractAlternates;
                bindingIndex = 1;
                break;
            case Binding.Pause:
                inputAction = inputActions.Player.Pause;
                bindingIndex = 0;
                break;

        }
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose(); 
                inputActions.Player.Enable();
                onComplete();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, inputAction.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}
