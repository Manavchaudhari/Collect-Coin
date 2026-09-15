# CollectCoin

![Unity](https://img.shields.io/badge/Unity-2019.4.34f1_LTS-black?logo=unity)
![Language](https://img.shields.io/badge/language-C%23-blue)
![Platform](https://img.shields.io/badge/platform-Windows-lightgre)
![Status](https://img.shields.io/badge/status-paused-yellow)

A 3D coin-collecting platformer built in Unity: roll a sphere around a level, dodge a water hazard, and collect every coin to win.

## Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [What the Player Can Do](#what-the-player-can-do)
- [Process](#process)
- [How I Built It](#how-i-built-it)
- [How It Could Be Improved](#how-it-could-be-improved)
- [How to Run the Project](#how-to-run-the-project)
- [Assets & Credits](#assets--credits)
- [Demo Video](#demo-video)

## Overview

CollectCoin is a single-player 3D platformer where you control a sphere, collect coins scattered around a level, and reach a win condition once you've collected them all. Falling into water resets the level. It started from a simple rolling-ball base and has since been substantially reworked: physics tuned by hand for movement feel, game state and player control split into separate systems, and a jump-input bug fixed along the way.

## Technologies Used

- **Engine:** Unity 2019.4.34f1 (LTS)
- **Language:** C#
- **UI:** TextMesh Pro
- **Physics:** Unity's built-in `Rigidbody`/`PhysX`, with custom velocity clamping and a gravity multiplier layered on top (see [How I Built It](#how-i-built-it))

## Features

- Coin collection tracked via trigger colliders, with a live on-screen counter
- Win condition once all coins are collected
- Fail/reset state when the player touches water
- Restart available at any time (`R` key), not just on fail

## What the Player Can Do

- Move the sphere (`WASD` / arrow keys)
- Jump (`Space`, or your configured Jump button)
- Collect coins scattered around the level
- Fall into water to trigger an automatic level restart
- Restart manually at any time with `R`
- Win by collecting every coin in the level

## Process

The project began from a simple rolling-ball base - the movement, camera, and basic input pattern follow Unity's own "Roll a Ball" tutorial structure. From there I built out the parts that make it an actual game: a coin-collection system with a win condition, a water hazard with a restart flow, and a UI layer to show progress. Once the core loop worked, I went back and reworked the physics, since the default drag-based movement felt floaty and made landings imprecise - that rework is covered in detail below. The project is still active: a win-screen panel (Restart/Exit buttons) is the next planned addition.

## How I Built It
- **Level and audio.** The environment itself started as AurynSkyGames' Forest Pack demo scene - I modified it, adding extra tree placement to create parkour-style jump paths, and populated it with my own gameplay objects: the player sphere, the coin pickups, and the water/floor triggers. The coin pickup sound runs through the vendor's existing trigger system rather than through my own scripts, positioned to overlap each coin - getting that alignment right for every coin individually was fiddlier than it sounds.
The code is split into two scripts with distinct responsibilities:

- **`PlayerControl.cs`** only handles the sphere itself- reading input, applying movement and jump forces, and reporting what it's currently touching (a coin, water, or the floor). It doesn't know what those events *mean* for the game.
- **`GameManager.cs`** owns all game-flow state- the coin count, win state, and restart/quit - as a singleton (`GameManager.Instance`) that `PlayerControl` calls into when something relevant happens. Keeping these separate meant the physics/input code stayed simple even as game-state logic grew.

## How It Could Be Improved

- Finish and wire up the win-screen panel (Restart/Exit buttons)
- Add audio and particle feedback for jump, and winning
- Multiple levels / level progression instead of a single scene
- A proper main menu instead of starting directly in-level

## How to Run the Project

**Download and run (Windows):**
A packaged Windows build is available on the [Releases](../../releases) page. Download and unzip it, then run `CollectCoin.exe`. Since this isn't a signed build, Windows SmartScreen may show a warning the first time - click "More info" → "Run anyway" to proceed.

**Run it in the Unity Editor:**
1. Clone this repo.
2. Open the project in Unity **2019.4.34f1** (or a compatible 2019.4 LTS patch release).
3. Open the scene at `Assets/Forest Map/Forest Pack/Scenes/Forest_scene.unity` - **not** `Assets/Scene/SampleScene.unity`, which is an unused leftover from early setup.
4. Press Play.

> **Note:** This repo doesn't include the third-party environment art package (see [Assets & Credits](#assets--credits)) due to redistribution restrictions, so opening the project fresh in the Editor will show missing materials/meshes on the environment (ground, scenery). All gameplay code, UI, and logic are unaffected - for the full visual experience, use the WebGL build or the Windows download above.

## Assets & Credits

This project uses third-party art assets that are licensed for use in the finished, built game but aren't cleared for redistributing the raw source files - so they're excluded from this repository (kept locally to build/run the project, listed in `.gitignore`):

- **Forest Pack / CastlePack** environment assets by **AurynSkyGames** ([@AurynSky](https://twitter.com/AurynSky))
- **SkySeries Freebie** skybox by **Avionx**, via the [Unity Asset Store](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633), under the standard Unity Asset Store EULA

All gameplay code, UI logic, and game-state architecture are my own work.

## Demo Video
[![Watch the demo](https://img.youtube.com/vi/fWVJ0kdVvgY/maxresdefault.jpg)](https://youtu.be/fWVJ0kdVvgY)
