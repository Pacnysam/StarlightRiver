﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using StarlightRiver.Content.Items;
using StarlightRiver.Core;

namespace StarlightRiver.Content.Tiles.Vitric.Temple
{
    class EntranceDoor : ModTile
    {
        public override void SetDefaults()
        {
            minPick = int.MaxValue;
            TileID.Sets.DrawsWalls[Type] = true;
            QuickBlock.QuickSetFurniture(this, 2, 7, DustType<Dusts.Air>(), SoundID.Tink, false, new Color(200, 150, 80), false, true, "Vitric Temple Door");
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (StarlightWorld.HasFlag(WorldFlags.DesertOpen)) tile.inActive(true);
            else tile.inActive(false);
        }
    }

    class EntranceDoorItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";

        public EntranceDoorItem() : base("EntranceDoor", "Titties", TileType<EntranceDoor>(), 1) { }
    }
}
