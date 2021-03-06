﻿using StarlightRiver.Abilities.Content.ForbiddenWinds;
using StarlightRiver.Abilities.Content.Infusions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Items.InfusionMaterial
{
    class DashAstralMaterial : QuickMaterial
    {
        public DashAstralMaterial() : base("Comet essence", "Craft with a basic infusion to create an infusion!", 1, 0, ItemRarityID.Quest){}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(item.type);
            recipe.AddIngredient(ItemType<BasicInfusion>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemType<Astral>());
            recipe.AddRecipe();
        }
    }
}
