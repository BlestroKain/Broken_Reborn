# Guild Upgrade System Implementation Plan

This document outlines tasks required to integrate the guild upgrade system based on the reference commits from Joak1ngglb/Intersect-Engine.

## 1. Guild Creation Interface
- Add a new window `GuildCreationInterface` allowing players to design a guild logo with customizable background and symbol ([ad681d8](https://github.com/Joak1ngglb/Intersect-Engine/commit/ad681d827b5fbe9037ec5f6e2300fcad4ee49c4f)).
- Provide color sliders for background and symbol as well as preview components.
- Extend `GameInterface` with methods to open/close this window and update the game loop accordingly.
- Extend content loading to include a new `Guild` texture group and implement pixel editing utilities (`SetColor`, `GraphicsHelper.Compose`, `GraphicsHelper.Recolor`).

## 2. Guild Logo Networking and Storage
- Introduce packets for guild creation and guild data updates (`CreateGuildPacket`, `GuildUpdate`, etc.) ([dfb4d27](https://github.com/Joak1ngglb/Intersect-Engine/commit/dfb4d273825c2a214cd7746cf1fef3a10cf60826)).
- Update `PacketHandler` and `PacketSender` on both client and server to handle logo information.
- Modify player and guild database models to store logo files and RGB values.
- Add migrations to create the new columns for logo data.
- Display the composed guild logo in the `GuildWindow` and associate it with player name labels.

## 3. GUI Improvements
- Replace logo selection panels with scrollable containers and adjust control positions (`b485d6a1`).
- Show guild logos next to player labels (`2b70aa45`) and tweak sizes (`b04b6d06`).
- Allow the guild creation window to be opened via in‑game events by sending a `GuildCreationWindowPacket` (`4865d4f6`).

## 4. Guild Levels and Experience
- Track guild experience and level server‑side, including initial configuration values for base XP and growth factor.
- Update guild members when experience changes and show level/EXP in the guild window (`ca878d3`, `9ff28732`).
- Implement player commands to donate experience and adjust personal contribution percentage.

## 5. Guild Upgrades
- Add support for guild points and upgrade types (`GuildUpgradeType`).
- Implement client UI to view and purchase upgrades and server logic to apply them (`9085b269`).
- Store spent points and upgrade levels in the database with a migration.

## 6. Maintenance and Fixes
- Reset contribution variables for new or removed members (`169101c`).
- Remove unused symbol scaling and Y‑offset properties from packets and UI (`981719fe`).
- Ensure guild members are attached correctly when loaded from the database (`01369e523`).

Each of these steps corresponds to features introduced in the reference commits and should be ported while respecting the existing code style and architecture of this repository.

Referenced commits: `ad681d8`, `dfb4d27`, `9ff2873`, `b485d6a1`, `ca878d3`, `2b70aa45`, `b04b6d06`, `9085b269`, `4865d4f6`, `169101c`, `981719fe`, `01369e523`.
