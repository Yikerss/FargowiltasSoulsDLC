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
    public class StatigelEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Statigel Enchantment");
            Tooltip.SetDefault(
@"'Statis’ mystical power surrounds you…'
When you take over 100 damage in one hit you become immune to damage for an extended period of time
Grants an extra jump and increased jump height
Effects of Counter Scarf, Mana Overloader, and Fungal Symbiote");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = 200000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(181, 0, 156);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;


            CalamityPlayer player1 = player.Calamity();

            player1.statigelSet = true;
            player.hasJumpOption_Sail = true;
            player.jumpBoost = true;

            if (SoulConfig.Instance.calamityToggles.FungalSymbiote)
            {
                calamity.Find<ModItem>("FungalSymbiote").UpdateAccessory(player, hideVisual);
                calamity.Find<ModItem>("FungalClump").UpdateAccessory(player, hideVisual);
            }

            calamity.Find<ModItem>("ManaOverloader").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("CounterScarf").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("StatigelHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("StatigelArmor").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("StatigelGreaves").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("CounterScarf").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ManaOverloader").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("FungalSymbiote").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("FungalClump").Type);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
