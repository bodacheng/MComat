# Asset Organization

This repository contains both project-owned resources and large third-party asset packs. To reduce breakage risk, cleanup should prefer incremental moves inside project-owned roots rather than top-level wholesale relocation of vendor packages.

## Resource Roots

Addressable or project-runtime-managed content stays under `Assets/ExternalAssets`.

Top-level structure:

- `Animations`
- `Audio`
- `Data`
- `Materials`
- `Prefabs`
- `Textures`
- `_Temp`

Non-addressable resource packs are collected separately under `Assets/NonAddressableAssets`.

Top-level structure:

- `Backgrounds`
- `Environments`
- `Icons`
- `VFX`

### Type-Based Layout

Use type-first folders under `Assets/ExternalAssets`:

- `Animations` for `.anim`, controllers, and related animation assets.
- `Audio` for BGM and other runtime audio assets.
- `Data` for `.asset`, `.csv`, `.json`, and story or stage content.
- `Materials` for shared `.mat` assets.
- `Prefabs` for runtime prefab content such as units, battlegrounds, VFX, and UI prefabs.
- `Textures` for icons, backgrounds, and other image assets.

Inside each type folder, add one more layer by domain when useful, for example:

- `Assets/ExternalAssets/Textures/Icons/Skill`
- `Assets/ExternalAssets/Textures/Icons/Unit`
- `Assets/ExternalAssets/Prefabs/UI`
- `Assets/ExternalAssets/Data/Config`

### Exceptions

- Keep `_Temp` for temporary imports and staging files that should not sit with production assets.
- Keep `_Legacy` and `_Source` only inside a specific domain folder when old revisions or source PSD/JPG files must stay nearby.

## Split Rule

- If a resource is used directly by Addressable Groups, keep it under `Assets/ExternalAssets` or its existing addressable-owned folder.
- If a resource pack is not used by Addressable Groups, move it under `Assets/NonAddressableAssets`.
- Avoid moving only part of a third-party pack unless necessary, especially when importer settings, materials, shaders, or editor scripts may expect the package layout.

## Safety Rules

- Before moving project assets, search for hard-coded `Assets/...` paths in editor scripts.
- Before moving anything under `Resources`, check `Resources.Load(...)` usage first.
- Before moving Addressables content, preserve GUIDs and do not change runtime addresses unless the call sites are updated too.
- Avoid bulk-moving third-party package roots unless there is a very strong reason and the package-specific editor scripts have been reviewed.

## Cleanup Status

- Addressable project resources were further consolidated into `Assets/ExternalAssets` with type-first subfolders.
- `BGMS` was folded into `Assets/ExternalAssets/Audio/BGM`.
- Several non-addressable resource packs were moved under `Assets/NonAddressableAssets`.
- Editor scripts with hard-coded asset paths were updated to the new structure.
- Representative mixed-content folders outside the main root were normalized into `Materials`, `Textures`, `Models`, `Prefabs`, and `Scenes`.
