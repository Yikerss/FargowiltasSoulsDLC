using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class XerocEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xeroc Enchantment");
            Tooltip.SetDefault(
@"'The power of an ancient god at your command…'
Rogue projectiles have special effects on enemy hits
Imbued with cosmic wrath and rage when you are damaged
Effects of The Community");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 1000000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(171, 19, 33);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.XerocEffects))
            {
                calamity.Call("SetSetBonus", player, "empyrean", true);
            }

            //the community
            calamity.Find<ModItem>("TheCommunity").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("EmpyreanMask").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("EmpyreanCloak").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("EmpyreanCuisses").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("TheCommunity").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ElephantKiller").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ElementalBlaster").Type);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
