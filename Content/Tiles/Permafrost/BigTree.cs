﻿using Microsoft.Xna.Framework;
using StarlightRiver.Content.Items;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Tiles.Permafrost
{
    class BigTree : ModTile
    {
        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 16, 17, DustType<Dusts.Stone>(), SoundID.Tink, false, new Color(100, 200, 200));
    }

    class BigTreeItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";

        public BigTreeItem() : base("Big Tree", "I came", TileType<BigTree>(), 1) { }
    }
}
