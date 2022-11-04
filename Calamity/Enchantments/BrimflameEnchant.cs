using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class BrimflameEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brimflame Enchantment");
            Tooltip.SetDefault(
@"''
Press Y to trigger a brimflame frenzy effect
While under this effect, your damage is significantly boosted
However, this comes at the cost of rapid life loss and no mana regeneration");
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
            Item.rare = ItemRarityID.Lime;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            //set bonus
            calamity.Call("SetSetBonus", player, "brimflame", true);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("BrimflameCowl").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("BrimflameRobes").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("BrimflameBoots").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("Butcher").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("IgneousExaltation").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("BlazingStar").Type);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
