using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class WulfrumEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wulfrum Enchantment");
            Tooltip.SetDefault(
@"'Not to be confused with Tungsten Enchantment…'
+5 defense when below 50% life
Effects of Trinket of Chi");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 40000;
            Item.defense = 3;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(129, 168, 109);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (player.statLife <= (int)(player.statLifeMax2 * 0.5))
            {
                player.statDefense += 5;
            }
            //trinket of chi
            calamity.Find<ModItem>("TrinketofChi").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("WulfrumHat").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("WulfrumJacket").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("WulfrumOveralls").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("TrinketofChi").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SparkSpreader").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Pumpler").Type);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
