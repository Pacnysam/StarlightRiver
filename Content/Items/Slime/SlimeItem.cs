using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;

namespace StarlightRiver.Content.Items.Slime
{
    public class SlimeItem : QuickMaterial
    {
        public SlimeItem() : base("Slime glob", "sticks to your hands", 999, Item.sellPrice(0, 0, 0, 2), ItemRarityID.Green) { }
    }
}