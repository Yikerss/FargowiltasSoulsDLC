using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSoulsDLC.Calamity.Essences
{
    public class RogueEssence : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Outlaw's Essence");
            Tooltip.SetDefault(
@"18% increased rogue damage
5% increased rogue velocity
5% increased rogue critical strike chance
'This is only the beginning..'");
        }

        /*public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(255, 30, 247));
                }
            }
        }*/

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            calamity.Call("AddRogueDamage", player, 0.18f);
            calamity.Call("AddRogueCrit", player, 5);
            calamity.Call("AddRogueVelocity", player, 0.05f);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(calamity.Find<ModItem>("RogueEmblem").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("GildedDagger").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("WebBall").Type, 300);
            recipe.AddIngredient(calamity.Find<ModItem>("BouncingEyeball").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Shroomerang").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("MeteorFist").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SludgeSplotch").Type, 300);
            recipe.AddIngredient(calamity.Find<ModItem>("SkyStabber").Type, 4);
            recipe.AddIngredient(calamity.Find<ModItem>("PoisonPack").Type, 3);
            recipe.AddIngredient(calamity.Find<ModItem>("HardenedHoneycomb").Type, 300);
            recipe.AddIngredient(calamity.Find<ModItem>("ShinobiBlade").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("InfernalKris").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AshenStalactite").Type);
            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
