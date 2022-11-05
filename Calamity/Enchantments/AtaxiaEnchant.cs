using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.CalPlayer;
using CalamityMod;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class AtaxiaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydrothermic Enchantment");
            Tooltip.SetDefault(
@"''
You have a 20% chance to emit a blazing explosion on hit
Melee attacks and projectiles cause chaos flames to erupt on enemy hits
You have a 50% chance to fire a homing chaos flare when using ranged weapons
Magic attacks summon damaging and healing flare orbs on hit
Summons a hydrothermic vent to protect you
Rogue weapons have a 10% chance to unleash a volley of chaos flames around the player
Effects of Hallowed Rune and Ethereal Extorter");

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 1000000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(194, 89, 89);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.calamityToggles.HallowedRune)
                calamity.Find<ModItem>("HallowedRune").UpdateAccessory(player, hideVisual);

            if (SoulConfig.Instance.calamityToggles.EtherealExtorter)
                calamity.Find<ModItem>("EtherealExtorter").UpdateAccessory(player, hideVisual);


            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.AtaxiaEffects))
            {
                CalamityPlayer player1 = player.Calamity();
                player1.ataxiaBlaze = true;
                player1.chaosSpirit = true;
            }

                //summon
                if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ChaosMinion))
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source = player.GetSource_ItemUse(base.Item, null);
                    if (player.FindBuffIndex(ModContent.BuffType<HydrothermicVentBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<HydrothermicVentBuff>(), 0xe10, true, false);
                    }
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<HydrothermicVent>()] < 1)
                    {
                        int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(250f);
                        int index = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<HydrothermicVent>(), num, 0f, Main.myPlayer, 0x26f, 0f);
                        if (Utils.IndexInRange<Projectile>(Main.projectile, index))
                        {
                            Main.projectile[index].originalDamage = 190;
                        }
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("AtaxiaHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AtaxiaArmor").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AtaxiaSubligar").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("HallowedRune").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("EtherealExtorter").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("BarracudaGun").Type);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
