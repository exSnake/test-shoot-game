#!/bin/bash
# setup-repo.sh
# Esegui questo script UNA VOLTA dopo aver clonato/creato il repo.
# Inizializza git, imposta il remote e fa il primo commit.

set -e

REPO_URL="https://github.com/YOUR_USERNAME/run-and-shoot.git"

echo "🚀 Inizializzando il repository..."
git init
git add .
git commit -m "chore: initial scaffolding — M0 complete

- Project structure (src/, scenes/, assets/, docs/)
- C# core: EventBus, GameManager, SceneManager
- C# interfaces: IDamageable, IShooter, IEnemy
- C# systems: HealthSystem
- C# entities: PlayerController, PlayerShooter, BaseEnemy, Projectile
- GDScript: Main, HUD, MainMenu, GameOver
- Docs: README, GDD, ARCHITECTURE
- GitHub: PR template, issue templates
"

echo ""
echo "✅ Primo commit fatto!"
echo ""
echo "Ora aggiungi il remote e fai il push:"
echo "  git remote add origin $REPO_URL"
echo "  git branch -M main"
echo "  git push -u origin main"
