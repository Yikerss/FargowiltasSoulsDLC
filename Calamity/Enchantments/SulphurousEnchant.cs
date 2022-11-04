using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class SulphurousEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulphurous Enchantment");
            Tooltip.SetDefault(
@"''
Attacking and being attacked by enemies inflicts poison
Grants a sulphurous bubble jump that applies venom on hit
Slightly reduces breath loss in the abyss
Effects of Sand Cloak and Alluring Bait");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 50000;
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

            player.hasJumpOption_Sandstorm = true;

            calamity.Call("SetSetBonus", player, "sulphur", true);

            calamity.Find<ModItem>("SandCloak").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("AlluringBait").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("SulphurousHelmet").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SulphurousBreastplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SulphurousLeggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("SandCloak").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AlluringBait").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("CausticCroakerStaff").Type);


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
