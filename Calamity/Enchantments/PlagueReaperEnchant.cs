using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using CalamityMod;
using CalamityMod.Projectiles.Rogue;
using Terraria.DataStructures;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class PlagueReaperEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plague Reaper Enchantment");
            Tooltip.SetDefault(
@"''
Enemies receive 10% more damage from ranged projectiles when afflicted by the Plague
Getting hit causes the plague cinders to rain from above
Effects of Plague Hive, Plagued Fuel Pack, and The Camper");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 300000;
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

            CalamityMod.CalPlayer.CalamityPlayer player1 = player.Calamity();

            player1.plagueReaper = true;
            //meme
            if (player.whoAmI == Main.myPlayer)
            {
                IEntitySource source = player.GetSource_Accessory(base.Item, null);
                if (player.immune && ((player.miscCounter % 10) == 0))
                {
                    int damage = (int)player.GetTotalDamage<RangedDamageClass>().ApplyTo(40f);
                    Projectile projectile = CalamityUtils.ProjectileRain(source, player.Center, 400f, 100f, 500f, 800f, 0x16f, ModContent.ProjectileType<TheSyringeCinder>(), damage, 4f, player.whoAmI);
                    if (projectile.whoAmI.WithinBounds(0x3e8))
                    {
                        projectile.DamageType = DamageClass.Generic;
                    }
                }
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.PlagueHive))
            {
                calamity.Find<ModItem>("PlagueHive").UpdateAccessory(player, hideVisual);
            }
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.PlaguedFuelPack))
            {
                calamity.Find<ModItem>("PlaguedFuelPack").UpdateAccessory(player, hideVisual);
            }
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.TheCamper))
            {
                calamity.Find<ModItem>("TheCamper").UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("PlagueReaperMask").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("PlagueReaperVest").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("PlagueReaperStriders").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("PlagueHive").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("PlaguedFuelPack").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TheCamper").Type);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
