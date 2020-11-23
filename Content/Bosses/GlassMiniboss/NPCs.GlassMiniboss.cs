﻿using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StarlightRiver.Content.NPCs.Miniboss.Glassweaver
{
    internal partial class GlassMiniboss : ModNPC
    {
        bool attackVariant = false;

        internal ref float GlobalTimer => ref npc.ai[0];
        internal ref float Phase => ref npc.ai[1];
        internal ref float AttackPhase => ref npc.ai[2];
        internal ref float AttackTimer => ref npc.ai[3];

        private float spinAngle = 0;

        public static Vector2 spawnPos => StarlightWorld.VitricBiome.TopLeft() * 16 + new Vector2(1 * 16, 76 * 16);
        private static Vector2 LeftForge => spawnPos + new Vector2(-350, 300);
        private static Vector2 RightForge => spawnPos + new Vector2(350, 300);

        //Tracking of the forges, if this ends up bloated i'll move it out to a seperate manager NPC but for now this is simpler
        public int leftForgeCharge;
        public int rightForgeCharge;

        public enum PhaseEnum
        {
            SpawnEffects = 0,
            SpawnAnimation = 1,
            FirstPhase = 2,
            DeathAnimation = 3
        }

        public override void SetStaticDefaults() => DisplayName.SetDefault("Glassweaver");

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false; //no contact damage! this is strictly a GOOD GAME DESIGN ONLY ZONE!!!

        public override void SetDefaults()
        {
            npc.width = 56;
            npc.height = 60;
            npc.lifeMax = 1500;
            npc.damage = 20;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.knockBackResist = 0;
            npc.boss = true;
            npc.defense = 14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Miniboss");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(2000 * bossLifeScale);
        }

        public override bool CheckDead()
        {
            NPC.NewNPC((StarlightWorld.VitricBiome.X - 10) * 16, (StarlightWorld.VitricBiome.Center.Y + 12) * 16, NPCType<GlassweaverTown>());

            StarlightWorld.Flag(WorldFlags.DesertOpen);
            Main.NewText("The temple doors slide open...", new Color(200, 170, 80));

            return true;
        }

        private void SetPhase(PhaseEnum phase) => Phase = (float)phase;

        public override void AI()
        {
            AttackTimer++;

            switch (Phase)
            {
                case (int)PhaseEnum.SpawnEffects:

                    ResetAttack();

                    StarlightRiver.Instance.textcard.Display("Glassweaver", "the", null, 240, 1, true);

                    SetPhase(PhaseEnum.SpawnAnimation);

                    break;

                case (int)PhaseEnum.SpawnAnimation:

                    if (AttackTimer < 300) SpawnAnimation();
                    else
                    {
                        SetPhase(PhaseEnum.FirstPhase);
                        ResetAttack();
                        npc.noGravity = false;
                    }

                    break;

                case (int)PhaseEnum.FirstPhase:

                    npc.spriteDirection = npc.Center.X > Target.Center.X ? 1 : -1;

                    if (AttackTimer == 1)
                    {
                        AttackPhase++;
                        if (AttackPhase > 8) AttackPhase = 0;

                        attackVariant = Main.rand.NextBool();
                        npc.netUpdate = true;
                    }

                    switch (AttackPhase)
                    {
                        case 0: Spears(); break;
                        case 1: Knives(); break;
                        case 2: UppercutGlide(); break;
                        case 3: Idle(60); break;
                        case 4: Hammer(); break;
                        case 5: Knives(); break;
                        case 6: SlashUpSlash(); break;
                        case 7: Idle(90); break;
                        case 8: if (attackVariant) SlashUpSlash(); else UppercutGlide(); break;
                    }

                    break;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackVariant);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackVariant = reader.ReadBoolean();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            var glowTex = GetTexture("StarlightRiver/NPCs/Miniboss/Glassweaver/ForgeGlow");

            for(int k = 0; k < leftForgeCharge; k++)
            {
                var pos = LeftForge - Main.screenPosition + new Vector2(-glowTex.Width / 2 + 2, -168 + k * -(glowTex.Height + 2));
                spriteBatch.Draw(glowTex, pos, Color.Red * 0.3f);
            }

            for (int k = 0; k < rightForgeCharge; k++)
            {
                var pos = RightForge - Main.screenPosition + new Vector2(-glowTex.Width / 2 + 2, -168 + k * -(glowTex.Height + 2));
                spriteBatch.Draw(glowTex, pos, Color.Red * 0.3f);
            }

            if (spinAngle != 0)
            {
                float sin = (float)Math.Sin(spinAngle + 1.57f * npc.direction);
                int off = Math.Abs((int)(npc.frame.Width * sin));

                SpriteEffects effect = sin > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                spriteBatch.Draw(GetTexture(Texture), new Rectangle((int)(npc.position.X - Main.screenPosition.X - off / 2 + npc.width / 2), (int)(npc.position.Y - Main.screenPosition.Y), off, npc.frame.Height), npc.frame, drawColor, 0, Vector2.Zero, effect, 0);
            }

            else
            {
                spriteBatch.Draw(GetTexture(Texture), npc.Center - Main.screenPosition, npc.frame, drawColor, 0, npc.frame.Size() / 2, 1, npc.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
            
            return false;
        }
    }
}
