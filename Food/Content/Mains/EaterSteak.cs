﻿using StarlightRiver.Core;
using Terraria;
using Terraria.ID;

namespace StarlightRiver.Food.Content.Mains
{
    internal class EaterSteak : Ingredient
    {
        public EaterSteak() : base("+3% damage reduction", 900, IngredientType.Main) { }

        public override bool Autoload(ref string name)
        {
            StarlightNPC.NPCLootEvent += LootEaterSteak;
            return true;
        }

        public override void BuffEffects(Player player, float multiplier)
        {
            player.endurance += 0.03f;
        }

        private void LootEaterSteak(NPC npc)
        {
            if (npc.type == NPCID.EaterofSouls && Main.rand.Next(4) == 0)
                Item.NewItem(npc.Center, item.type);
        }
    }
}
