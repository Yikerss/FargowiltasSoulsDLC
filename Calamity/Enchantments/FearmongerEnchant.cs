using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class FearmongerEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fearmonger Enchantment");
            Tooltip.SetDefault(
@"''
Minions deal full damage while wielding weaponry
All minion attacks grant colossal life regeneration
15% increased damage reduction during the Pumpkin and Frost Moons
This extra damage reduction ignores the soft cap
Provides cold protection in Death Mode
Effects of Spectral Veil and Statis' Void Sash");
        }
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 750000;
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

            calamity.Call("SetSetBonus", player, "fearmonger", true);

            calamity.Find<ModItem>("SpectralVeil").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("TheEvolution").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("StatisBeltOfCurses").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("FearmongerGreathelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("FearmongerPlatemail").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("FearmongerGreaves").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("TheEvolution").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SpectralVeil").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("StatisBeltOfCurses").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("FaceMelter").Type);


            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
