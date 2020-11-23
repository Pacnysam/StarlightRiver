﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Projectiles.WeaponProjectiles
{
    internal class FlareExplosion : ModProjectile
    {
		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.alpha = 255;
			projectile.ranged = true;
			projectile.friendly = true;
			projectile.timeLeft = 14;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.extraUpdates = 1;
		}

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare Explosion");
        }

        public override void AI()
		{
			
		}
    }
}