# Black Quartz 可复用 Prompt

## 1. 在 Codex 中完整重建一个同方向的 v2 Companion

将游戏原版参考图与这段 Prompt 一起交给 Codex，并调用 `$hatch-pet`：

```text
请使用 $hatch-pet 创建一个 Codex v2 Companion，名字叫 Black Quartz。

核心身份：她是一种安静运行的几何协议实体。主体由上下两枚细长、近乎实心的烟黑石英晶体棱柱构成：上方尖端朝上，下方尖端朝下，共用稳定的竖直轴，中间只有一道狭窄暗缝。整体轮廓是一根悬浮的黑色双锥，不是两个空心三角形，也不是银色机械框架。

材质：低反射烟黑石英与抛光黑曜石玻璃；深蓝黑、炭黑和内部灰色渐层；只有柔和铅灰晶面反射、极轻微冷蓝或紫色折射。中央狭缝内藏一枚很小的冷蓝档案核心，安静状态下不可出现暴露的大光球。

气质：克制、安静、秩序、档案、权限、科幻、成熟、非卖萌。禁止动物解剖、脸、眼睛、肢体、翅膀、羽毛、尾巴、服装、文字、UI、散落碎片、阴影、光环、霓虹描边和亮银金属框。

状态语言：
- idle：双锥近乎闭合，中缝只有极小呼吸式蓝光。
- running-right / running-left：不做动物奔跑；只让上下晶体产生克制的方向性相位差和回弹。
- waving：中缝附着的小晶面短暂展开，作为正式确认信号；不得出现手臂或动作线。
- jumping：完整双锥保持同一尺寸，仅以低→中→高→中→低的 y 位置表达小幅悬浮跃迁；无阴影、落地痕迹或缩放。
- failed：蓝核短暂变暗，晶体收紧后复位。
- waiting：两枚晶体略微分开，像权限门等待批准。
- running（processing）：晶体适度展开，显露内嵌蓝核、附着式细环和黑色权限刺；所有结构必须连接主体。
- review：中缝内的窄小扫描孔径平稳扫过并归中。

观察方向：她没有眼睛。唯一的瞄准特征是中缝内很小的蓝色菱形孔径。外部双锥轴线、尖端、尺度和基线必须固定；孔径在中缝中移动来表达 000 上、090 屏幕右、180 下、270 屏幕左，并以 22.5° 均匀插值完成 16 个顺时针方向。不得旋转或倾斜整个物体来伪造视线。

请完成 8×11、192×208 单元格、1536×2288 的 spriteVersionNumber 2 图集；生成九种标准状态、四向锚点、两条八帧方向行；完成透明边缘处理、v2 验证、三人独立方向盲测和最终视觉 QA 后再打包。任何跳跃缩放、方向基准模糊、银框化或动物化都必须修复后才能交付。
```

## 2. 单张主视觉 Prompt

```text
Create a single centered full-body reference sprite of Black Quartz, a quiet geometric protocol entity. Two elongated, nearly solid faceted smoky-black quartz prisms share one vertical axis: upper prism points up, lower prism points down, separated by one narrow dark seam. The silhouette reads as one floating black double-point monolith, never hollow triangles or a silver mechanical frame. Low-reflectance obsidian-glass material, blue-black charcoal mass, subtle internal grey gradients, restrained lead-grey facet reflections, faint cold-blue and violet refraction. A tiny sapphire archive core is only barely visible inside the seam. Mature science fiction, archival, ordered, silent, elegant, non-cute. No animal or humanoid anatomy, no face, eyes, limbs, wings, clothing, text, symbols, UI, loose fragments, particles, shadow, halo, neon outline, scenery, or props. Crisp pet-safe silhouette, readable at 192×208. Flat pure #00FF00 chroma background.
```

## 3. 保持身份、调整材质

```text
Edit the supplied Black Quartz reference without changing its silhouette, proportions, seam width, core size, or vertical registration. Preserve the two solid elongated crystal prisms exactly. Reduce bright metallic edge highlights; deepen the body toward smoky blue-black quartz with soft lead-grey facet reflections, subtle internal translucency, and restrained cold-blue/violet refraction. The result must feel like dense polished stone and obsidian glass—not chrome, metal armor, a hollow emblem, or neon UI. Keep the background flat #00FF00 and add no effects, shadows, fragments, text, or scenery.
```

## 4. 修复跃迁动画

```text
Edit this five-frame Black Quartz jumping strip. Use one identical double-prism sprite size in every frame: same pixel width, height, crystal proportions, seam, and blue-core size. Change only the full sprite's y position in a symmetric low→middle→high→middle→low arc. Do not scale, squash, stretch, rotate, change perspective, or make the apex frame appear closer to camera. Keep safe top and bottom margins. No floor, shadow, dust, impact mark, trail, glow, or detached effect. Flat #00FF00 background.
```

## 5. 延展世界观或宣传文案

```text
以“Black Quartz 是一种档案与权限协议的几何化身”为核心，为她写一段克制、冷静、略带科幻感的世界观介绍。强调烟黑晶体、隐藏的蓝色档案核心、静止时闭合、处理任务时展开，以及她是一个长期陪伴用户工作的协议实体。不要写成卖萌角色，不要使用夸张战斗口吻，控制在 150–250 字。
```
