using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria.Localization;
using CalamityMod.CalPlayer;
using CalamityMod;
using CalamityMod.CalPlayer.Dashes;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class GodSlayerEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Slayer Enchantment");
            Tooltip.SetDefault(
@"'The power to slay gods resides within you...'
You will survive fatal damage and will be healed 150 HP if an attack would have killed you
This effect can only occur once every 45 seconds
Taking over 80 damage in one hit will cause you to release a swarm of high-damage god killer darts
An attack that would deal 80 damage or less will have its damage reduced to 1
Your ranged critical hits have a chance to critically hit, causing 4 times the damage
You have a chance to fire a god killer shrapnel round while firing ranged weapons
Enemies will release god slayer flames and healing flames when hit with magic attacks
You can dash in any direction
Taking damage will cause you to release a magical god slayer explosion
Hitting enemies will summon god slayer phantoms
While at full HP all of your rogue stats are boosted by 10%
If you take over 80 damage in one hit you will be given extra immunity frames
Effects of the Nebulous Core and Draedon's Heart");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(100, 108, 156);
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
            Item.value = 10000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            CalamityPlayer player1 = player.Calamity();

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.GodSlayerEffects))
            {
                player1.godSlayerThrowing = true;
                player1.godSlayer = true;
                player1.godSlayerRanged = true;
            }
            
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.MechwormMinion))
            {
                if (player1.godSlayerDashHotKeyPressed || ((player.dashDelay != 0) && (player1.LastUsedDashID == GodslayerArmorDash.ID)))
                {
                    player1.DeferredDashID = GodslayerArmorDash.ID;
                    player.dash = 0;
                }
            }
            
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.NebulousCore))
            {
                calamity.Find<ModItem>("NebulousCore").UpdateAccessory(player, hideVisual);
            }

            //draedons heart
            calamity.Find<ModItem>("DraedonsHeart").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("GodSlayerHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("GodSlayerChestplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("GodSlayerLeggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("NebulousCore").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DimensionalSoulArtifact").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DraedonsHeart").Type);

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
