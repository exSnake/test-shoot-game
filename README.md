# 🔫 RunAndShoot

> 2D horizontal run & shoot game — inspired by Kirby Superstar.
> Built with **Godot 4** · **C#** (core systems) + **GDScript** (scenes/UI)

---

## 🗂️ Project Structure

```
run-and-shoot/
├── src/                         # C# source — core systems & entities
│   ├── Core/
│   │   ├── EventBus.cs          # Global decoupled signal bus (Autoload)
│   │   ├── GameManager.cs       # Game state, score, lives (Autoload)
│   │   └── SceneManager.cs      # All scene transitions (Autoload)
│   ├── Interfaces/
│   │   ├── IDamageable.cs
│   │   ├── IShooter.cs
│   │   └── IEnemy.cs
│   ├── Systems/
│   │   └── HealthSystem.cs      # Reusable health logic (not a Node)
│   └── Entities/
│       ├── Player/
│       │   ├── PlayerController.cs
│       │   └── PlayerShooter.cs
│       ├── Enemies/
│       │   └── BaseEnemy.cs
│       └── Projectiles/
│           └── Projectile.cs
│
├── scenes/                      # Godot scenes (.tscn) + GDScript (.gd)
│   ├── main/        Main.tscn / Main.gd
│   ├── ui/          HUD, MainMenu, GameOver
│   ├── levels/      Level01.tscn, Level02.tscn …
│   ├── player/      Player.tscn
│   ├── enemies/     EnemyWalker.tscn, EnemyShooter.tscn …
│   ├── boss/        Boss.tscn
│   └── projectiles/ Bullet.tscn
│
├── assets/
│   ├── sprites/     player / enemies / boss / projectiles / tiles / ui
│   ├── audio/       sfx / music
│   └── fonts/
│
├── docs/
│   ├── GDD.md       Game Design Document
│   └── ARCHITECTURE.md
│
├── project.godot
└── RunAndShoot.csproj
```

---

## 🚀 Getting Started

### Prerequisites
- [Godot 4.2+](https://godotengine.org/download) — with **.NET / Mono** support
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

### Setup
```bash
git clone https://github.com/YOUR_USERNAME/run-and-shoot.git
cd run-and-shoot
# Open Godot 4, click "Import" and select project.godot
```

> ⚠️ First open: Godot will build the C# solution automatically. Wait for it to finish before hitting Play.

---

## 🏗️ Architecture Principles

| Principle | How we apply it |
|---|---|
| **Single Responsibility** | `PlayerController` moves, `PlayerShooter` shoots, `HealthSystem` tracks HP |
| **Open/Closed** | New enemies extend `BaseEnemy` without touching existing code |
| **Dependency Inversion** | Systems depend on `IDamageable`, `IShooter`, `IEnemy` — not concrete classes |
| **Event-driven** | `EventBus` decouples UI, GameManager and entities from each other |

---

## 🗺️ Milestones

| # | Milestone | Status |
|---|---|---|
| M0 | Setup & scaffolding | ✅ Done |
| M1 | Player movement & jump | 🔲 Todo |
| M2 | Shooting & combat | 🔲 Todo |
| M3 | First playable level | 🔲 Todo |
| M4 | Boss fight | 🔲 Todo |
| M5 | Game feel & polish | 🔲 Todo |
| M6 | itch.io release | 🔲 Todo |

---

## 👥 Team

| Role | Who |
|---|---|
| Architecture, core systems, review | Daniele |
| Level design, assets, balancing, GDScript | [Amico] |
