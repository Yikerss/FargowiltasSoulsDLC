using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Forces
{
    public class DevastationForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Devastation");
            Tooltip.SetDefault(
@"'Rain hell down on those who resist your power'
All armor bonuses from Wulfrum, Reaver, Plague Reaper, and Demonshade
Effects of Trinket of Chi and Plague Hive
Effects of Plagued Fuel Pack, The Camper, and Profaned Soul Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Mod.Find<ModItem>("WulfrumEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("ReaverEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("PlagueReaperEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("DemonShadeEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(null, "WulfrumEnchant");
            recipe.AddIngredient(null, "ReaverEnchant");
            recipe.AddIngredient(null, "PlagueReaperEnchant");
            recipe.AddIngredient(null, "DemonShadeEnchant");

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
