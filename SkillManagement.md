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
  - 运行时通过 `Assets/Singleton/SkillIcon.cs`
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

## 4. 推荐新增流程

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

## 5. 注意事项

- 同一 `TYPE` 下默认不允许重复 `REAL_NAME`，避免多个技能共用同一动画名导致管理混乱
- 若确实需要复用同一动画名，可在工具中勾选“允许同类型复用 REAL_NAME”
- 运行时真正加载动画使用的是 `REAL_NAME`，而不是显示名
- 运行时真正加载图标使用的是 `RECORD_ID`
