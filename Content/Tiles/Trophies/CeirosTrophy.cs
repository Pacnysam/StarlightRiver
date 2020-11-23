﻿using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarlightRiver.Content.Items;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria;
using StarlightRiver.Core;

namespace StarlightRiver.Content.Tiles.Trophies
{
    class CeirosTrophy : ModTile
    {
        public override void SetDefaults()
        {
            TileObjectData.newTile.AnchorWall = TileObjectData.Style3x3Wall.AnchorWall;
            QuickBlock.QuickSetFurniture(this, 3, 3, 7, SoundID.Dig, false, new Color(120, 85, 60), false, false, "Trophy");
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16, ItemType<CeirosTrophyItem>());
    }

    class CeirosTrophyItem : QuickTileItem
    {
        public CeirosTrophyItem() : base("Ceiros Trophy", "", TileType<CeirosTrophy>(), 1) { }
    }
}
