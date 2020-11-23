﻿using System;
using Microsoft.Xna.Framework;
using StarlightRiver.Buffs;
using StarlightRiver.Core;
using StarlightRiver.Content.Projectiles.Ammo;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Content.Items.Accessories.EarlyPreHardmode
{
    public class CoughDrops : SmartAccessory
    {
        public CoughDrops() : base("Cough Drops", "When debuffs expire or are otherwise removed, gain some temporary speed and damage\nBonuses are based on buff duration, additional activations stack the time") { }

        public override bool Autoload(ref string name)
        {
            On.Terraria.Player.DelBuff += PlayerDelBuff;
            return true;
        }

        private void PlayerDelBuff(On.Terraria.Player.orig_DelBuff orig, Player self, int buffid)
        {
            if (Helper.IsValidDebuff(self, buffid))
                ProcEffect(self);

            orig(self, buffid);
        }

        public override void SafeUpdateEquip(Player player)
        {
            for (int i = 0; i < Player.MaxBuffs; i += 1)
            {
                if (player.buffTime[i] < 10)
                {
                    if (Helper.IsValidDebuff(player, i))
                    {
                        player.DelBuff(i);
                    }
                }
            }
            //
        }

        public static void ProcEffect(Player ply)
        {
            if (Helper.HasEquipped(ply, ModContent.ItemType<CoughDrops>()))
            {
                Main.PlaySound(SoundID.DD2_BetsyHurt, ply.Center).Pitch = Main.rand.NextFloat(0.25f,0.6f);
                ply.AddBuff(ModContent.BuffType<CoughDropsBuff>(), 60 * 5);
            }
        }

    }
}