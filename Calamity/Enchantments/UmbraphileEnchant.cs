using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class UmbraphileEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Umbraphile Enchantment");
            Tooltip.SetDefault(
@"''
Rogue weapons have a chance to create explosions on hit
Stealth strikes always create an explosion
Penumbra potions always build stealth at max effectiveness
Effects of Thief's Dime, Vampiric Talisman, and Momentum Capacitor");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = 300000;
        }

        /*public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(70, 63, 69);
                }
            }
        }*/

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            calamity.Call("SetSetBonus", player, "umbraphile", true);

            calamity.Find<ModItem>("ThiefsDime").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("VampiricTalisman").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("MomentumCapacitor").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("UmbraphileHood").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("UmbraphileRegalia").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("UmbraphileBoots").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("ThiefsDime").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("VampiricTalisman").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("MomentumCapacitor").Type);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
