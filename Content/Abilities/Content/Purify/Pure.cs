﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Content.Projectiles.Ability;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Abilities.Content.Purify
{
    public class Pure : Ability
    {
        public override string Texture => "StarlightRiver/Pickups/PureCrown";
        public override bool Available => base.Available && !Main.projectile.Any(proj => proj.owner == Player.whoAmI && proj.active && (proj.type == ProjectileType<Purifier>() || proj.type == ProjectileType<PurifierReturn>()));
        public override Color Color => Color.White;
        public override float ActivationCostDefault => 1;

        public override bool HotKeyMatch(TriggersSet triggers, AbilityHotkeys abilityKeys)
        {
            return abilityKeys.Get<Pure>().JustPressed;
        }

        public override void OnActivate()
        {
            Main.PlaySound(SoundID.Item37);
        }

        public override void UpdateActive()
        {
            Projectile.NewProjectile(Player.Center + new Vector2(16, -24), Vector2.Zero, ProjectileType<Purifier>(), 0, 0, Player.whoAmI);
            StarlightWorld.PureTiles.Add((Player.Center + new Vector2(16, -24)) / 16);

            Deactivate();
        }
    }
}