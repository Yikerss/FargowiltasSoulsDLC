using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Forces
{
    public class AnnihilationForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Annihilation");
            Tooltip.SetDefault(
@"'Where once there was life and light, only ruin remains...'
All armor bonuses from Aerospec, Statigel, and Hydrothermic
All armor bonuses from Xeroc and Fearmonger
Effects of Gladiator's Locket and Unstable Prism
Effects of Counter Scarf and Fungal Symbiote
Effects of Hallowed Rune, Ethereal Extorter, and The Community
Effects of Spectral Veil and Statis' Void Sash");
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

            Mod.Find<ModItem>("AerospecEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("StatigelEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("AtaxiaEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("XerocEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("FearmongerEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(null, "AerospecEnchant");
            recipe.AddIngredient(null, "StatigelEnchant");
            recipe.AddIngredient(null, "AtaxiaEnchant");
            recipe.AddIngredient(null, "XerocEnchant");
            recipe.AddIngredient(null, "FearmongerEnchant");

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
