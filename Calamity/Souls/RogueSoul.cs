using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSoulsDLC.Calamity.Essences;

namespace FargowiltasSoulsDLC.Calamity.Souls
{
    public class RogueSoul : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vagabond's Soul");
            Tooltip.SetDefault(
@"'They’ll never see it coming'
30% increased rogue damage
15% increased rogue velocity
15% increased rogue critical strike chance
Effects of Eclipse Mirror, Nanotech, Venerated Locket, and Dragon Scales");
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
            Item.value = 1000000;
            Item.rare = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            calamity.Call("AddRogueDamage", player, 0.3f);
            calamity.Call("AddRogueCrit", player, 15);
            calamity.Call("AddRogueVelocity", player, 0.15f);

            calamity.Find<ModItem>("EclipseMirror").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("Nanotech").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("VeneratedLocket").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("DragonScales").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RogueEssence>());

            recipe.AddIngredient(calamity.Find<ModItem>("EclipseMirror").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Nanotech").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("VeneratedLocket").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DragonScales").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("HellsSun").Type, 10);
            recipe.AddIngredient(calamity.Find<ModItem>("JawsOfOblivion").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DeepSeaDumbbell").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TimeBolt").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Eradicator").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("EclipsesFall").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Celestus").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ScarletDevil").Type);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
