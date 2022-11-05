using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria.Localization;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Buffs.Summon;
using Terraria.DataStructures;
using CalamityMod.Projectiles.Summon;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class AuricEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auric Tesla Enchantment");
            Tooltip.SetDefault(
@"'Your strength rivals that of the Jungle Tyrant...'
All effects from Tarragon, Bloodflare, Godslayer and Silva armor
All attacks spawn healing auric orbs
Effects of Heart of the Elements and The Sponge");

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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(217, 142, 67);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            CalamityPlayer player1 = player.Calamity();

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.AuricEffects))
            {
                player1.tarraSet = true;
                player1.tarraSummon = true;
                player1.bloodflareSet = true;

                player1.silvaSet = true;
                player1.silvaSummon = true;
                player1.auricSet = true;

                player1.WearingPostMLSummonerSet = true;
                player.thorns += 3f;
                player.lavaMax += 240;
                player.ignoreWater = true;
                player.crimsonRegen = true;
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.PolterMines))
                player1.bloodflareSummon = true;

            if (player.whoAmI == Main.myPlayer)
            {
                IEntitySource source = player.GetSource_ItemUse(base.Item, null);
                if (player.FindBuffIndex(ModContent.BuffType<SilvaCrystalBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<SilvaCrystalBuff>(), 0xe10, true, false);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SilvaCrystal>()] < 1)
                {
                    int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(0x4b0f);
                    int index = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<SilvaCrystal>(), num, 0f, Main.myPlayer, -20f, 0f);
                    if (Utils.IndexInRange<Projectile>(Main.projectile, index))
                    {
                        Main.projectile[index].originalDamage = 0x4b0;
                    }
                }
            }



            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.WaifuMinions))
            {
                calamity.Find<ModItem>("HeartoftheElements").UpdateAccessory(player, hideVisual);
            }

            //the sponge
            calamity.Find<ModItem>("TheSponge").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("AuricTeslaRoyalHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AuricTeslaBodyArmor").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AuricTeslaCuisses").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("HeartoftheElements").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TheSponge").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ArkoftheCosmos").Type);

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
