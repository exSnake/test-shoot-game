# 🏗️ Architecture — RunAndShoot

## Strategia linguistica

| Layer | Linguaggio | Perché |
|---|---|---|
| Core systems, Entities, Interfaces | **C#** | Type safety, OOP solido, SOLID più facile da applicare |
| Scene scripts, UI, Level logic | **GDScript** | Più veloce da scrivere, perfetto con l'AI per chi è alle prime armi |

---

## Autoloads (Singleton di Godot)

Registrati in `Project → Project Settings → Autoload`:

| Nome | File | Responsabilità |
|---|---|---|
| `EventBus` | `src/Core/EventBus.cs` | Segnali globali decoupled |
| `GameManager` | `src/Core/GameManager.cs` | Stato di gioco, score, lives |
| `SceneManager` | `src/Core/SceneManager.cs` | Tutte le transizioni di scena |

> Questi tre sono gli **unici** global state. Tutto il resto comunica tramite EventBus.

---

## Flusso degli eventi (EventBus pattern)

```
PlayerController ──TakeDamage()──► HealthSystem
                                        │
                                   OnHealthChanged
                                        │
                                   EventBus.PlayerHealthChanged
                                        │
                          ┌─────────────┴──────────────┐
                        HUD.gd                    GameManager.cs
                   (aggiorna barra)          (controlla game over)
```

Nessuno tiene una reference diretta a nessun altro. Tutto passa per EventBus.

---

## Gerarchia delle entità

```
CharacterBody2D
├── PlayerController (IDamageable)
│   └── PlayerShooter (IShooter)
└── BaseEnemy (IEnemy → IDamageable)
    ├── EnemyWalker
    ├── EnemyShooter (IShooter)
    └── Boss
        └── (fasi gestite internamente)

Area2D
└── Projectile
    ├── PlayerBullet
    └── EnemyBullet
```

---

## Regole architetturali

1. **Nessun `GetNode` su nodi fuori dalla propria scena** — si usa EventBus
2. **I sistemi puri (HealthSystem) sono classi C# normali**, non Node — niente dipendenza da Godot
3. **Le scene GDScript non contengono logica di business** — solo reazione a eventi e chiamate a C#
4. **Ogni nemico nuovo estende `BaseEnemy`** — non copia/incolla
5. **Le scene `.tscn` non vivono nella cartella `src/`** — solo in `scenes/`

---

## Convenzioni di naming

| Tipo | Convenzione | Esempio |
|---|---|---|
| Classi C# | PascalCase | `PlayerController` |
| File GDScript | PascalCase | `HUD.gd` |
| Segnali EventBus | snake_case (Godot convention) | `player_health_changed` |
| Costanti | SCREAMING_SNAKE | `MAX_LIFETIME` |
| Export params | PascalCase | `[Export] float MoveSpeed` |
