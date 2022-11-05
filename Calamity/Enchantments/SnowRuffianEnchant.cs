using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;
using CalamityMod.CalPlayer;
using CalamityMod;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class SnowRuffianEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        private bool shouldBoost;

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Ruffian Enchantment");
            Tooltip.SetDefault(
@"''
You can glide to negate fall damage
Effects of Scuttler's Jewel");
        }

        /*public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(191, 68, 59);
                }
            }
        }*/

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            //set bonus
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.SnowRuffianWings))
            {
                CalamityPlayer player1 = player.Calamity();

                player1.snowRuffianSet = true;

                if (player.controlJump)
            {
                player.noFallDmg = true;
                player.UpdateJumpHeight();
                if (this.shouldBoost)
                {
                    player.velocity.X = player.velocity.X * 1.3f;
                    this.shouldBoost = false;
                    return;
                }
            }
            else if (!this.shouldBoost && player.velocity.Y == 0f)
            {
                this.shouldBoost = true;
            }
            }

            calamity.Find<ModItem>("ScuttlersJewel").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("SnowRuffianMask").Type); //private REE, fix later
            recipe.AddIngredient(calamity.Find<ModItem>("SnowRuffianChestplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SnowRuffianGreaves").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("ScuttlersJewel").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TundraLeash").Type);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
