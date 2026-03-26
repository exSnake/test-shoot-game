# 📋 Game Design Document — RunAndShoot

> Versione: 0.1 (draft)
> Ultimo aggiornamento: 2025

---

## 🎯 Concept

**Genere:** 2D Run & Shoot — progressione orizzontale
**Riferimento:** Kirby Superstar (SNES)
**Piattaforma target:** PC (Windows/Linux) + Browser (WebGL via itch.io)
**Numero giocatori:** 1 (single player)

---

## 🔄 Core Loop

```
Spawn → Muoviti → Spara nemici → Supera ostacoli → Batti il boss → Level complete
```

Ogni ciclo deve durare ~3-5 minuti per livello.

---

## 🕹️ Controls

| Azione | Tasto |
|---|---|
| Muoviti | A / D  o  ← → |
| Salta | Spazio |
| Spara | Click sinistro  o  Z |
| Pausa | Esc |

---

## 🧑 Il Personaggio

- Movimento orizzontale + salto
- Sparo orizzontale nella direzione in cui è rivolto
- HP: 5 (visualizzati come cuori o barra)
- Invulnerabilità breve dopo un colpo (1 secondo)

---

## 👾 Nemici (placeholder)

| Tipo | Comportamento | HP | Danno | Score |
|---|---|---|---|---|
| Walker | Cammina verso il player | 2 | 1 | 100 |
| Shooter | Si ferma e spara | 3 | 1 | 150 |
| Jumper | Salta in modo imprevedibile | 2 | 1 | 120 |

---

## 👹 Boss (Livello 1)

- **Fase 1** (100% → 50% HP): carica orizzontale + proiettili
- **Fase 2** (50% → 0% HP): pattern più aggressivo, più veloce
- HP: 20
- Score: 1000

---

## 📊 Progressione

- Il gioco è strutturato in livelli lineari
- Ogni livello termina con una boss fight
- Nessun sistema di upgrade in v1 (potenziale espansione futura)

---

## 🎨 Stile visivo

- Pixel art 2D — palette colorata e leggibile
- Asset pack da definire (Kenney / itch.io)
- Risoluzione interna: 1280×720

---

## 🔊 Audio

- Musica chiptune per i livelli
- SFX: sparo, salto, danno, morte nemico, boss, game over
- Sorgente: Freesound / OpenGameArt

---

## 🚧 Out of scope (v1)

- Multiplayer
- Sistema di upgrade/potenziamenti
- Mobile/gamepad support
- Livelli non lineari
