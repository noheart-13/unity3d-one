using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Animator Controller 中用于控制行走状态的 Bool 参数名称。
    private const string IS_WALKING = "Is Walking";

    // Player 引用用于读取玩家当前是否正在行走。
    [SerializeField] private Player player;

    // 缓存 Animator 引用，避免在 Update 中每帧重复查找组件。
    private Animator animator;

    private void Awake()
    {
        // 获取与 PlayerAnimator 挂在同一个 GameObject 上的 Animator。
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // 引用都存在时，把 Player 的行走状态同步给 Animator 参数。
        if (player != null && animator != null)
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
        }
    }
}
