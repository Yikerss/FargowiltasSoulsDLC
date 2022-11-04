using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Forces
{
    public class ExaltationForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Exaltation");
            Tooltip.SetDefault(
@"''
All armor bonuses from Tarragon, Bloodflare, and Brimflame
All armor bonuses from God Slayer, Silva, and Auric
Effects of Blazing Core, Dark Sun Ring, and Core of the Blood God
Effects of Nebulous Core and Draedon's Heart
Effects of the The Amalgam and Godly Soul Artifact
Effects of Yharim's Gift, Heart of the Elements, and The Sponge");
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

            Mod.Find<ModItem>("TarragonEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("BloodflareEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("GodSlayerEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("SilvaEnchant").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("AuricEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(null, "TarragonEnchant");
            recipe.AddIngredient(null, "BloodflareEnchant");
            recipe.AddIngredient(null, "GodSlayerEnchant");
            recipe.AddIngredient(null, "SilvaEnchant");
            recipe.AddIngredient(null, "AuricEnchant");

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
