﻿using Microsoft.Xna.Framework;
using StarlightRiver.Content.Abilities;
using System;
using static Terraria.ModLoader.ModContent;
using Terraria;
using StarlightRiver.Content.Items;
using StarlightRiver.Codex.Entries;

namespace StarlightRiver.Pickups
{
    class StaminaShardPickup : AbilityPickup
    {
        Tile Parent => Framing.GetTileSafely((int)npc.Center.X / 16, (int)npc.Center.Y / 16);

        public override string Texture => GetStaminaTexture();

        public override Color GlowColor => new Color(255, 100, 30);

        public override bool Fancy => false;

        public override bool CanPickup(Player player)
        {
            AbilityHandler ah = player.GetHandler();
            return !ah.Shards.Has(Parent.frameX);
        }

        public override void Visuals()
        {
            if(Main.rand.Next(2) == 0) Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedByRandom(Math.PI) * Main.rand.NextFloat(16), DustType<Dusts.Stamina>(), Vector2.UnitY * -1);
            Lighting.AddLight(npc.Center, new Vector3(0.5f, 0.25f, 0.05f));
        }

        public override void PickupEffects(Player player)
        {
            AbilityHandler ah = player.GetHandler();
            
            ah.Shards.Add(Parent.frameX);

            if (ah.ShardCount % 3 == 0)
            {
                StarlightRiver.Instance.textcard.Display("Stamina Vessel", "Your maximum stamina has increased by 1", null, 240, 0.8f);
            }
            else
            {
                StarlightRiver.Instance.textcard.Display("Stamina Vessel Shard", "Collect " + (3 - ah.ShardCount % 3) + " more to increase your maximum stamina", null, 240, 0.6f);
            }

            player.GetModPlayer<Core.StarlightPlayer>().MaxPickupTimer = 1;

            Helper.UnlockEntry<StaminaShardEntry>(Main.LocalPlayer);
        }

        private static string GetStaminaTexture()
        {
            if (Main.gameMenu) return "StarlightRiver/Pickups/Stamina1";

            AbilityHandler ah = Main.LocalPlayer.GetHandler();
            return "StarlightRiver/Pickups/Stamina" + (ah.ShardCount + 1);
        }
    }

    class StaminaShardTile : AbilityPickupTile
    {
        public override int PickupType => NPCType<StaminaShardPickup>();

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            fail = true;

            Tile tile = Framing.GetTileSafely(i, j);

            tile.frameX += 1;
            if (tile.frameX > 2) 
                tile.frameX = 0;
            Main.NewText("pickup set to stamina shard number " + tile.frameX, Color.Orange);
        }
    }

    class StaminaShardTileItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Pickups/Stamina1";

        public StaminaShardTileItem() : base("Stamina Shard", "PENIS", TileType<StaminaShardTile>(), 1) { }
    }
}
