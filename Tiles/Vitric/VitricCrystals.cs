﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Vitric
{
    internal class VitricLargeCrystal : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, int.MaxValue, DustType<Dusts.Air>(), SoundID.CoinPickup, new Color(115, 182, 158), -1);
            Main.tileBlockLight[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;

            Main.tileMerge[Type][TileType<Blocks.VitricSpike>()] = true;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile t = Main.tile[i, j];
            if (t.frameX > 0)
            {
                Texture2D tex = GetTexture("StarlightRiver/Tiles/Vitric/LargeCrystal");
                spriteBatch.Draw(tex, ((new Vector2(i, j) + Helper.TileAdj) * 16) - Main.screenPosition, tex.Frame(2, 1, t.frameX - 1), Color.White, 0, new Vector2(80, 176), 1, 0, 0);
                //Helper.DrawWithLighting(((new Vector2(i, j) + Helper.TileAdj) * 16) - Main.screenPosition, tex); //Subject to change
            }
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            fail = true;
        }
    }

    internal class VitricSmallCrystal : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, int.MaxValue, DustType<Dusts.Air>(), SoundID.CoinPickup, new Color(115, 182, 158), -1);
            Main.tileBlockLight[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;

            Main.tileMerge[Type][TileType<Blocks.VitricSpike>()] = true;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            fail = true;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile t = Main.tile[i, j];
            if (t.frameX > 0)
            {
                Texture2D tex = GetTexture("StarlightRiver/Tiles/Vitric/SmallCrystal");
                spriteBatch.Draw(tex, ((new Vector2(i, j) + Helper.TileAdj) * 16) - Main.screenPosition, tex.Frame(2, 1, t.frameX - 1), Color.White, 0, new Vector2(32, 48), 1, 0, 0);
                //Helper.DrawWithLighting(((new Vector2(i, j) + Helper.TileAdj) * 16) - Main.screenPosition, tex); //Subject to change
            }
        }
    }
}