﻿using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace StarlightRiver.Content.NPCs.Miniboss.Glassweaver
{
    class GlassSlash : ModProjectile
    {
        public NPC Parent => Main.npc[(int)projectile.ai[0]];

        public override string Texture => "StarlightRiver/Invisible";

        public override void SetStaticDefaults() => DisplayName.SetDefault("Woven Blade");

        public override void SetDefaults()
        {
            projectile.width = 200;
            projectile.height = 80;
            projectile.hostile = true;
            projectile.timeLeft = 20;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (projectile.timeLeft == 20) Main.PlaySound(SoundID.Item65, projectile.Center);
            if (Parent != null) projectile.Center = Parent.Center + Vector2.UnitX * (projectile.ai[1] == -1 ? 120 : -120);
        }
    }
}
