﻿using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarlightRiver.Content.Items;
using StarlightRiver.Core;

namespace StarlightRiver.Content.Tiles.Overgrow
{
    class WispAltarL : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = OvergrowTileLoader.OvergrowTileDir + "WispAltarL";
            return true;
        }

        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 6, 11, DustType<Dusts.Gold>(), SoundID.Tink, false, new Color(200, 200, 200));
    }

    class WispAltarLItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";

        public WispAltarLItem() : base("Wisp Altar L Placer", "DEBUG", TileType<WispAltarL>(), -1) { }

    }

    class WispAltarR : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = OvergrowTileLoader.OvergrowTileDir + "WispAltarR";
            return true;
        }

        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 6, 11, DustType<Dusts.Gold>(), SoundID.Tink, false, new Color(200, 200, 200));
    }

    class WispAltarRItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";

        public WispAltarRItem() : base("Wisp Altar R Placer", "DEBUG", TileType<WispAltarR>(), -1) { }
    }
}
