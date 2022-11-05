using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Summon;
using CalamityMod;
using Terraria.DataStructures;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class AerospecEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aerospec Enchantment");
            Tooltip.SetDefault(
@"'The sky comes to your aid…'
You fall quicker and are immune to fall damage
Taking over 25 damage in one hit causes several homing feathers to fall
Summons a Valkyrie minion to protect you
Effects of Gladiator's Locket and Unstable Prism");
            
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 3;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.value = 200000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(159, 112, 112);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ValkyrieMinion))
            {

                CalamityPlayer player1 = player.Calamity();
                player1.valkyrie = true;
                player1.aeroSet = true;
                player.noFallDmg = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source = player.GetSource_ItemUse(base.Item, null);
                    if (player.FindBuffIndex(ModContent.BuffType<ValkyrieBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<ValkyrieBuff>(), 0xe10, true, false);
                    }
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<Valkyrie>()] < 1)
                    {
                        int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(40f);

                        int index = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Valkyrie>(), num, 0f, Main.myPlayer, 0f, 0f);
                        if (Utils.IndexInRange<Projectile>(Main.projectile, index))
                        {
                            Main.projectile[index].originalDamage = 20;
                        }
                    }
                }
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.GladiatorLocket))
                calamity.Find<ModItem>("GladiatorsLocket").UpdateAccessory(player, hideVisual);
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.UnstablePrism))
                calamity.Find<ModItem>("UnstablePrism").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            CreateRecipe()
                .AddIngredient(calamity.Find<ModItem>("AerospecHelm").Type)
                .AddIngredient(calamity.Find<ModItem>("AerospecBreastplate").Type)
                .AddIngredient(calamity.Find<ModItem>("AerospecLeggings").Type)
                .AddIngredient(calamity.Find<ModItem>("GladiatorsLocket").Type)
                .AddIngredient(calamity.Find<ModItem>("UnstablePrism").Type)
                .AddIngredient(calamity.Find<ModItem>("StormSurge").Type)
                .AddTile(TileID.DemonAltar)
                .Register();

        }
    }
}
