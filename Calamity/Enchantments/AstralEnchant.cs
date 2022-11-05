using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class AstralEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Enchantment");
            Tooltip.SetDefault(
@"'The Astral Infection has consumed you...'
Whenever you crit an enemy fallen, hallowed, and astral stars will rain down
This effect has a 1 second cooldown before it can trigger again
Effects of the Astral Arcanum and Gravistar Sabaton");

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = 1000000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(123, 99, 130);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;
            CalamityPlayer player1 = player.Calamity();
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.AstralStars))
                player1.astralStarRain = true;


            calamity.Find<ModItem>("AstralArcanum").UpdateAccessory(player, hideVisual);

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.GravistarSabaton))
                calamity.Find<ModItem>("GravistarSabaton").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("AstralHelm"));
            recipe.AddIngredient(calamity.Find<ModItem>("AstralBreastplate"));
            recipe.AddIngredient(calamity.Find<ModItem>("AstralLeggings"));
            recipe.AddIngredient(calamity.Find<ModItem>("AstralArcanum"));
            recipe.AddIngredient(calamity.Find<ModItem>("GravistarSabaton"));
            recipe.AddIngredient(calamity.Find<ModItem>("UrsaSergeant"));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
