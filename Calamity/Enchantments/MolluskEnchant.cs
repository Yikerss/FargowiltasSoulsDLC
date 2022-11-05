using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod;
using Terraria.DataStructures;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class MolluskEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mollusk Enchantment");
            Tooltip.SetDefault(
@"'The world is your oyster'
When using any weapon you have a 10% chance to throw a returning seashell projectile
Summons multiple clams to protect you
Effects of Giant Pearl and Aquatic Emblem 
Effects of Ocean's Crest and Luxor's Gift");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = 150000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(74, 97, 96);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;


            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ShellfishMinion))
            {

                player.Calamity().molluskSet = true;
                player.maxMinions += 4;
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source = player.GetSource_ItemUse(base.Item, null);
                    if (player.FindBuffIndex(ModContent.BuffType<ShellfishBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<ShellfishBuff>(), 0xe10, true, false);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<Shellfish>()] < 2)
                    {
                        Vector2 pos = new Vector2(player.Center.X, player.Center.Y);
                        Vector2 vel = new Vector2(0, -1);
                        int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(200f);
                        Projectile.NewProjectileDirect(source, pos, vel, ModContent.ProjectileType<Shellfish>(), num, 0f, player.whoAmI, 0f, 0f).originalDamage = num;
                    }
                }
                player.Calamity().wearingRogueArmor = true;
            }




            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.GiantPearl))
            {
                calamity.Find<ModItem>("GiantPearl").UpdateAccessory(player, hideVisual);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.AmidiasPendant))
            {
                calamity.Find<ModItem>("AmidiasPendant").UpdateAccessory(player, hideVisual);
            }

            calamity.Find<ModItem>("AquaticEmblem").UpdateAccessory(player, hideVisual);

            Mod.Find<ModItem>("VictideEnchant").UpdateAccessory(player, hideVisual);

        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("MolluskShellmet").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("MolluskShellplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("MolluskShelleggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("AmidiasPendant").Type);
            recipe.AddIngredient(ModContent.ItemType<VictideEnchant>());
            recipe.AddIngredient(calamity.Find<ModItem>("GiantPearl").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AquaticEmblem").Type);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
