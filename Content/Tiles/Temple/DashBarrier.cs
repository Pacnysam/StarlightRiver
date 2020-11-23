﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Content.Abilities;
using StarlightRiver.Content.Items;
using StarlightRiver.Content.Projectiles.Dummies;
using System;
using Terraria;
using Terraria.ID;
using StarlightRiver.Core;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Tiles.Temple
{
    class DashBarrier : DummyTile
    {
        public override int DummyType => ProjectileType<DashBarrierDummy>();

        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 2, 3, DustType<Dusts.Stamina>(), SoundID.Shatter, false, new Color(204, 91, 50), false, false, "Stamina Jar");
    }

    internal class DashBarrierDummy : Dummy
    {
        public DashBarrierDummy() : base(TileType<DashBarrier>(), 32, 48) { }
        public override void Collision(Player player)
        {
            if (AbilityHelper.CheckDash(player, projectile.Hitbox))
            {
                WorldGen.KillTile(ParentX, ParentY);
                Main.PlaySound(SoundID.Tink, projectile.Center);
            }
        }
    }

    public class DashBarrierItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";

        public DashBarrierItem() : base("Dash Barrier", "Cum in my pussy.", TileType<DashBarrier>(), -12) { }
    }
}
