# Player.cs 代码注释

此文件用于解释 `Player.cs`。原脚本采用 GBK 编码，为了保留其中的所有代码、原注释和乱码，未直接修改该文件。

## 字段

- `speed`：玩家每秒移动的世界单位数，可在 Inspector 中调整。
- `gameInput`：输入管理脚本的引用，用于取得键盘或手柄的移动方向。
- `isWalking`：保存玩家当前是否有移动方向，供 `PlayerAnimator` 查询。
- `lastInteractDir`：保存最近一次非零移动方向。玩家松开方向键后，当前输入会变为零，但交互射线仍可沿最后面对的方向检测。

## Update

`Update()` 每帧依次调用：

1. `HandleMovement()`：读取输入、预测碰撞、移动并旋转玩家。
2. `HandleOnInteractions()`：沿最近一次移动方向发射交互射线。

## HandleOnInteractions

- `interactDistance` 是交互射线的最大检测距离。
- `inputVector` 是归一化后的二维输入。
- `moveDir` 把二维输入的 X、Y 映射到三维世界的 X、Z 平面。
- 只有 `moveDir` 不是零向量时才更新 `lastInteractDir`，避免玩家停止后丢失朝向。
- `Physics.Raycast(...)` 返回是否命中 Collider。
- `out RaycastHit raycastHit` 保存命中对象、命中点、距离和表面法线等信息。

## IsWalking

`IsWalking()` 对外提供玩家的行走状态。其他脚本可以读取结果，但不能直接修改私有字段 `isWalking`。

## HandleMovement

- `moveDistance = speed * Time.deltaTime` 把每秒速度换算成本帧移动距离，使移动不受帧率影响。
- `playerRadius` 和 `playerHeight` 描述用于碰撞预测的胶囊体尺寸。
- `Physics.CapsuleCast(...)` 不会真正移动玩家，只预测胶囊体沿目标方向移动时是否会撞到 Collider。
- `!Physics.CapsuleCast(...)` 中的 `!` 是逻辑取反：没有碰撞时 `canMove` 才为 `true`。

当完整移动方向被阻挡时，代码会：

1. 只保留 X 分量，尝试左右移动。
2. X 方向也被阻挡时，只保留 Z 分量，尝试前后移动。
3. 找到可行方向后更新 `moveDir`，从而产生沿墙移动的效果。
4. 所有方向都被阻挡时，`canMove` 保持 `false`，本帧不修改位置。

最后：

- `transform.position += moveDir * moveDistance` 执行本帧位移。
- `isWalking = moveDir != Vector3.zero` 根据方向是否为零更新行走状态。
- `Vector3.Slerp(...)` 在当前朝向和移动方向之间做球面插值，让玩家平滑转向。
