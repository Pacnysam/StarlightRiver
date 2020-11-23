﻿using Microsoft.Xna.Framework;
using StarlightRiver.Content.Abilities;
using StarlightRiver.GUI;
using StarlightRiver.Content.Items.Armor;
using StarlightRiver.Content.NPCs.Boss.SquidBoss;
using StarlightRiver.Content.Tiles.Permafrost;
using StarlightRiver.Content.Tiles.Vitric;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Core
{
    //I wanna put any of the accessory stats into a different file for tidiness-IDG
    public partial class StarlightPlayer : ModPlayer
    {
        public short FiftyFiveLeafClover = 0;
        public short DisinfectCooldown = 0;
        public short EngineerTransform = 0;
    }
}
