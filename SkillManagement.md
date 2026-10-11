# 技能管理与新增流程

## 1. 运行时技能管理结构

### 技能数据源
- `Assets/ExternalAssets/Data/Config/mst_skill.csv`
  - 技能主表，定义 `RECORD_ID / REAL_NAME / TYPE / SP_LEVEL / ATTACK_TYPE / EVENT_CODE`
- `Assets/ExternalAssets/Data/Config/skill_ai_attrs.csv`
  - AI 触发距离与高度
- `Assets/ExternalAssets/Data/Config/skill_name.csv`
  - 多语言名字与简介
- `Assets/ExternalAssets/Data/Config/SkillStaticAnalysis.csv`
  - 展示层的伤害、攻击次数、HP 估算

### 运行时装载
- `Assets/ImportantDefinition/Skill/SkillConfig/SkillConfigTable/SkillConfigTable.cs`
  - 统一加载技能 CSV，并构建 `SkillConfigRefDic`
- `Assets/ImportantDefinition/Skill/SkillConfig/SkillConfigTable/SkillConfigTable_Util.cs`
  - 将 CSV 行转换成 `SkillConfig`
- `Assets/ImportantDefinition/Skill/SkillConfig/SkillNameTable.cs`
  - 负责多语言技能名与简介

### 战斗中的技能组装
- `Assets/Behaviour/Soul/SkillSet/SkillSet.cs`
  - 九宫格技能组结构（`a1 ~ c3`）
- `Assets/Behaviour/Soul/SkillSet/SkillSet_INI.cs`
  - 将技能 ID 转为 `SkillEntity`，生成连段可取消关系
- `Assets/DATA_CENTER/Data_Center.cs`
  - `Step2Initialize` 中根据 `SkillSet` 预加载动画

### 资源查找规则
- 图标：
  - 运行时通过 `Packages/com.mcombat.shared/Runtime/ModelView/SkillIcon.cs`
  - 使用 Addressables 地址 = `RECORD_ID`
- 动画：
  - 运行时通过 `Assets/ResourceLoading/AnimationResourceLoader.cs`
  - 使用 Addressables 地址 = `{TYPE}/skill/{REAL_NAME}.anim`
  - 同时依赖 `skill_anim` 标签做预扫描

## 2. 新增技能时必须同时补齐的内容

1. 四张 CSV 都要有对应记录  
2. 技能图标要能通过 `RECORD_ID` 被 Addressables 加载  
3. 技能动画要能通过 `{TYPE}/skill/{REAL_NAME}.anim` 被 Addressables 加载  
4. 技能组编辑、技能石、战斗预加载都依赖同一套 `RECORD_ID -> SkillConfig -> REAL_NAME`

## 3. 已新增的“技能添加系统”

入口：
- Unity 菜单 `Tools/Skill Creation Tool`

功能：
- 自动分配新的技能 ID
- 一次性向四张技能 CSV 追加新条目
- 自动创建或复制：
  - 技能图标
  - 技能动画
- 自动注册 Addressables：
  - 图标进 `SkillIcon`
  - 动画进 `SkillAnim`
  - 动画自动挂 `skill_anim` 标签
- 失败时回滚：
  - CSV 变更
  - 新建资源
  - Addressables 条目

## 4. 根据动作素材新增技能（新版）

入口：Unity 菜单 `Tools/Skill Motion Authoring`，也可从旧工作台的“技能制作与添加”页打开。

当前自动流程支持 **human 类型的 Humanoid / GR 近战技能**。法术、武器专用判定、非人形、位移技能仍使用原工作台编排；不要把其他类型强行声明为 GR。

1. 选择 `.anim` 或 FBX 内的具体 AnimationClip，以及 `Assets/ExternalAssets/Prefabs/Units/human/` 下的角色 Prefab。
2. 点击“分析动作并导出方案”。工具在独立预览场景采样，导出 JSON：来源 GUID/子资源 ID、导入依赖哈希、骨骼轨迹、候选攻击窗口、评分理由和最多 12 帧骨架证据。
3. 由作者或 Codex 审阅 JSON。编辑 `hits` 的 `enabled/functionName/startTime/endTime/damageType`、`cancelTime`、名称、AI 距离、权重，并填写 `reviewNotes`。时间单位是秒；候选可能处于子帧，正式窗口可按素材帧率对齐。
4. 点击“校验方案”。此步骤不写入项目。低分候选默认禁用；自动启用的建议也需要结合姿势确认。评分表示运动证据强弱，不代表攻击意图或命中概率。
5. 点击“按方案创建技能”。只生成新 `.anim`，原 FBX/动画不变；写入四张 CSV，复制或生成图标，注册 Addressables。
6. 在技能工作台预览新动作，再进战斗确认碰撞、取消时机和 AI 距离。自动校验不能替代实战调优。

### Codex 可直接使用的命令

不需要在项目中配置 OpenAI API Key。Codex 通过项目命令调用 Unity，与窗口使用同一套分析/校验/创建代码。批处理前关闭本项目的 Unity 编辑器；如果编辑器正在使用，则在窗口里操作。默认自动使用 `ProjectVersion.txt` 对应的 macOS Unity，可用 `--unity` 指定其他平台路径。

```bash
python3 Tools/skill_authoring.py analyze \
  --clip 'Assets/Malbers Animations/Monsters/Baruk the Werewolf/Animations/Attacks/Baruk_Attack_Left_Claw.fbx' \
  --clip-name Baruk_Attack_Left_Claw \
  --rig Assets/ExternalAssets/Prefabs/Units/human/baruk.prefab \
  --plan SkillAuthoring/draft.json

# 审阅并编辑 JSON 后：
python3 Tools/skill_authoring.py validate --plan SkillAuthoring/draft.json
python3 Tools/skill_authoring.py create --plan SkillAuthoring/draft.json
python3 Tools/skill_authoring.py selftest
```

`recordId` 留空时扫描四表自动分配；也可明确指定尚未使用的正整数。创建结果和 Unity 日志位于 `Logs/skill-authoring-<action>.*`。成功后将返回的 ID 记入方案，再用 `verify --plan ...` 复查已创建内容。`create` 会自动检查刚生成的四表、动画事件、源素材未变、Addressables 和 Sprite；重复 `create` 会拒绝覆盖既有技能。

可视化骨架证据（需要 Pillow，可使用 Codex 提供的 Python 运行时）：

```bash
python3 Tools/render_skill_poses.py SkillAuthoring/draft.json SkillAuthoring/draft_poses.png
```

### 自动分析与运行时边界

- 在固定朝向中测量相对肢体根部的位移，保留肩/髋驱动的摆动；按肢长归一化，平滑采样，区分伸展与回收。
- 脚的离地判断使用固定根平面高度，避免下蹲时把支撑脚误判为踢击。
- 优先保留较强且互不重叠的候选，单段建议不超过 0.4 秒；JSON 保留所有候选供复核。
- 生成的事件为部位开启、同部位关闭、末段全局清理、取消。支持六个实际存在的身体部位事件；不接受旧名单中已不存在的手持武器事件名。
- 不允许攻击窗口重叠。项目多个身体部位共享命中去重集合，交错开启会重置命中状态；相邻窗口允许同刻衔接，先关闭后开启。重新开启部位本身会清理命中记录，不额外生成 `ClearTargets`。
- `damageType` 支持 `-1` 或 `1–15`，`0` 仅用于关闭。默认 `1` 为轻攻击。
- 取消必须晚于最后关闭，且早于片段结束；不在末帧放关键事件。生成的技能动画关闭循环。
- 验证来源、角色绑定、帧率、有限数值、路径、表结构、ID/名字/地址冲突。素材或角色依赖改变后必须重新分析，避免把旧时序应用到新动作。
- 新技能仅写入方案生成的近战事件，不自动复制源素材的声音、特效或移动事件。四表静态伤害按 `attackWeight × 攻击段数` 估算，距离仍需实战标定。
- 失败时恢复四表原始字节、Addressables 条目和标签，并删除本次新建资源；不主动保存无关的脏资源。

### 已落地的验证技能

`SkillAuthoring/baruk_left_claw.json` 保存了技能 **200：“裂爪 / Rending Claw / 裂き爪”** 的完整证据与审阅结论。来源是此前尚未注册为技能的 `Baruk_Attack_Left_Claw.fbx`；使用左手第 17–24 帧攻击、第 30 帧开放取消。新动画为 `baruk_rending_claw.anim`，骨架图见 `SkillAuthoring/baruk_left_claw_poses.png`。图标临时复用既有 156 的爪形美术。已完成 Unity 编译、生成和资源一致性校验，尚未完成真实战斗中的距离/手感调优。

## 5. 直接创建表项或手动编排

1. 打开 `Tools/Skill Creation Tool`
2. 填写：
   - `REAL_NAME`
   - `TYPE`
   - `SP_LEVEL`
   - AI 参数
   - 多语言名称与简介
3. 选择是否复制现有图标/动画模板
4. 点击 `生成技能条目`
5. 在 Unity 中补完新动画事件和图标美术

## 6. 注意事项

- 同一 `TYPE` 下默认不允许重复 `REAL_NAME`，避免多个技能共用同一动画名导致管理混乱
- 若确实需要复用同一动画名，可在工具中勾选“允许同类型复用 REAL_NAME”
- 运行时真正加载动画使用的是 `REAL_NAME`，而不是显示名
- 运行时真正加载图标使用的是 `RECORD_ID`
