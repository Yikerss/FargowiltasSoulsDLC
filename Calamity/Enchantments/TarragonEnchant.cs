using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class TarragonEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tarragon Enchantment");
            Tooltip.SetDefault(
@"'Braelor's undying might flows through you...'
Increased heart pickup range
Enemies have a chance to drop extra hearts on death
You have a 25% chance to regen health quickly when you take damage
Press Y to cloak yourself in life energy that heavily reduces enemy contact damage for 10 seconds
Ranged critical strikes will cause an explosion of leaves
Ranged projectiles have a chance to split into life energy on death
On every 5th critical strike you will fire a leaf storm
Magic projectiles have a 50% chance to heal you on enemy hits
At full health you gain +2 max minions and 10% increased minion damage
Summons a life aura around you that damages nearby enemies
After every 25 rogue critical hits you will gain 5 seconds of damage immunity
While under the effects of a debuff you gain 10% increased rogue damage
Effects of Blazing Core and Dark Sun Ring");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(169, 106, 52);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 3000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.TarragonEffects))
            {
                calamity.Call("SetSetBonus", player, "tarragon", true);
                calamity.Call("SetSetBonus", player, "tarragon_melee", true);
                calamity.Call("SetSetBonus", player, "tarragon_ranged", true);
                calamity.Call("SetSetBonus", player, "tarragon_magic", true);
                calamity.Call("SetSetBonus", player, "tarragon_summon", true);
                calamity.Call("SetSetBonus", player, "tarragon_rogue", true);
            }
            
            calamity.Find<ModItem>("BlazingCore").UpdateAccessory(player, hideVisual);
            //dark sun ring
            calamity.Find<ModItem>("DarkSunRing").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("TarragonHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TarragonBreastplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TarragonLeggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("BlazingCore").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DarkSunRing").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TrueTyrantYharimsUltisword").Type);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
