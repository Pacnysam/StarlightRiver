﻿using Microsoft.Xna.Framework;
using StarlightRiver.Content.Items;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Tiles.Void
{
    public class VoidWall : ModWall { public override void SetDefaults() => QuickBlock.QuickSetWall(this, DustType<Dusts.Darkness>(), SoundID.Dig, ItemType<VoidWallItem>(), true, new Color(10, 20, 15)); }
    public class VoidWallItem : QuickWallItem { public VoidWallItem() : base("Void Brick Wall", "", WallType<VoidWall>(), 0) { } }
}