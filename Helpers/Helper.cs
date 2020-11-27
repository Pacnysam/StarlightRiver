﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Codex;
using StarlightRiver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.UI;
using StarlightRiver.Content.Items;
using StarlightRiver;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Helpers
{
    public static partial class Helper
    {
        private static int tiltTime;
        private static float tiltMax;

        public static Rectangle ToRectangle(this Vector2 vector) => new Rectangle(0,0,(int)vector.X, (int)vector.Y);
        public static Player Owner(this Projectile proj) => Main.player[proj.owner];
        public static Vector2 TileAdj => Lighting.lightMode > 1 ? Vector2.Zero : Vector2.One * 12;
        public static Vector2 ScreenSize => new Vector2(Main.screenWidth, Main.screenHeight);

        /// <summary>
        /// Updates the value used for flipping rotation on the player. Should be reset to 0 when not in use.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="value"></param>
        public static void UpdateRotation(this Player player, float value) => player.GetModPlayer<StarlightPlayer>().rotation = value;

        public static bool IsTargetValid(NPC npc) => npc.active && !npc.friendly && !npc.immortal && !npc.dontTakeDamage;

        public static bool OnScreen(Vector2 pos) => (pos.X > -16 && pos.X < Main.screenWidth + 16 && pos.Y > -16 && pos.Y < Main.screenHeight + 16);

        public static bool OnScreen(Rectangle rect) => rect.Intersects(new Rectangle(0, 0, Main.screenWidth, Main.screenHeight));

        public static bool OnScreen(Vector2 pos, Vector2 size) => OnScreen(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y));

        public static Vector3 Vec3(this Vector2 vector) => new Vector3(vector.X, vector.Y, 0);

        public static Vector3 ScreenCoord(this Vector3 vector) => new Vector3(-1 + vector.X / Main.screenWidth * 2, (-1 + vector.Y / Main.screenHeight * 2f) * -1, 0);

        public static bool IsValidDebuff(Player player,int buffindex)
        {
            int bufftype = player.buffType[buffindex];
            bool vitalbuff = (bufftype == BuffID.PotionSickness || bufftype == BuffID.ManaSickness || bufftype == BuffID.ChaosState);
            return player.buffTime[buffindex] > 2 && Main.debuff[bufftype] && !Main.buffNoTimeDisplay[bufftype] && !Main.vanityPet[bufftype] && !vitalbuff;
        }

        /// <summary>
        /// returns true if every tile in a rectangle is air
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <summary>
        /// returns true if any tile in a rectanlge is air
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        public static void UnlockEntry<type>(Player player)
        {
            CodexHandler mp = player.GetModPlayer<CodexHandler>();
            CodexEntry entry = mp.Entries.FirstOrDefault(n => n is type);

            if (entry.RequiresUpgradedBook && mp.CodexState != 2) return; //dont give the player void entries if they dont have the void book
            entry.Locked = false;
            entry.New = true;
            if (mp.CodexState != 0) StarlightRiver.Instance.codexpopup.TripEntry(entry.Title);
            Main.PlaySound(SoundID.Item30);
        }

        public static void SpawnGem(int ID, Vector2 position)
        {
            int item = Item.NewItem(position, ItemType<StarlightGem>());
            (Main.item[item].modItem as StarlightGem).gemID = ID;
        }

        public static bool CheckCircularCollision(Vector2 center, int radius, Rectangle hitbox)
        {
            if (Vector2.Distance(center, hitbox.TopLeft()) <= radius) return true;
            if (Vector2.Distance(center, hitbox.TopRight()) <= radius) return true;
            if (Vector2.Distance(center, hitbox.BottomLeft()) <= radius) return true;
            return Vector2.Distance(center, hitbox.BottomRight()) <= radius;
        }

        public static bool CheckConicalCollision(Vector2 center, int radius, float angle, float width, Rectangle hitbox)
        {
            if (CheckPoint(center, radius, hitbox.TopLeft(), angle, width)) return true;
            if (CheckPoint(center, radius, hitbox.TopRight(), angle, width)) return true;
            if (CheckPoint(center, radius, hitbox.BottomLeft(), angle, width)) return true;
            return CheckPoint(center, radius, hitbox.BottomRight(), angle, width);
        }

        private static bool CheckPoint(Vector2 center, int radius, Vector2 check, float angle, float width)
        {
            float thisAngle = (center - check).ToRotation() % 6.28f;
            return Vector2.Distance(center, check) <= radius && thisAngle > angle - width && thisAngle < angle + width;
        }

        public static string TicksToTime(int ticks)
        {
            int sec = ticks / 60;
            return (sec / 60) + ":" + (sec % 60 < 10 ? "0" + sec % 60 : "" + sec % 60);
        }

        public static void DoTilt(float intensity)
        {
            tiltMax = intensity; tiltTime = 0;
        }

        public static void UpdateTilt()
        {
            if (Math.Abs(tiltMax) > 0)
            {
                tiltTime++;
                if (tiltTime >= 1 && tiltTime < 40)
                {
                    float tilt = tiltMax - tiltTime * tiltMax / 40f;
                    StarlightRiver.Rotation = tilt * (float)Math.Sin(Math.Pow(tiltTime / 40f * 6.28f, 0.9f));
                }

                if (tiltTime >= 40) { StarlightRiver.Rotation = 0; tiltMax = 0; }
            }
        }

        public static int SamplePerlin2D(int x, int y, int min, int max)
        {
            Texture2D perlin = TextureManager.Load("Images/Misc/Perlin");

            Color[] rawData = new Color[perlin.Width]; //array of colors
            Rectangle row = new Rectangle(0, y, perlin.Width, 1); //one row of the image
            perlin.GetData<Color>(0, row, rawData, 0, perlin.Width); //put the color data from the image into the array
            return (int)(min + rawData[x % 512].R / 255f * max);
        }

        public static float CompareAngle(float baseAngle, float targetAngle)
        {
            return (baseAngle - targetAngle + (float)Math.PI * 3) % MathHelper.TwoPi - (float)Math.PI;
        }

        public static string WrapString(string input, int length, DynamicSpriteFont font, float scale)
        {
            string output = "";
            string[] words = input.Split();

            string line = "";
            foreach (string str in words)
            {
                if(str == "NEWBLOCK")
                {
                    output += ("\n\n");
                    line = ("");
                    continue;
                }

                if (font.MeasureString(line).X * scale < length)
                {
                    output += (" " + str);
                    line += (" " + str);
                }
                else
                {
                    output += ("\n" + str);
                    line = (str);
                }
            }
            return output;
        }

        public static List<T> RandomizeList<T>(List<T> input)
        {
            int n = input.Count();
            while (n > 1)
            {
                n--;
                int k = Main.rand.Next(n + 1);
                T value = input[k];
                input[k] = input[n];
                input[n] = value;
            }
            return input;
        }

        public static Player FindNearestPlayer(Vector2 position)
        {
            Player player = null;

            for (int k = 0; k < Main.maxPlayers; k++)
            {
                if (Main.player[k] != null && Main.player[k].active && (player == null || Vector2.DistanceSquared(position, Main.player[k].Center) < Vector2.DistanceSquared(position, player.Center)))
                    player = Main.player[k];
            }
            return player;
        }
    }
}

