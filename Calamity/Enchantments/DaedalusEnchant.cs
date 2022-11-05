using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Buffs.Summon;
using Terraria.DataStructures;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class DaedalusEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daedalus Enchantment");
            Tooltip.SetDefault(
@"'Icy magic envelopes you...'
You have a 33% chance to reflect projectiles back at enemies
If you reflect a projectile you are also healed for 1/5 of that projectile's damage
Getting hit causes you to emit a blast of crystal shards
You have a 10% chance to absorb physical attacks and projectiles when hit
If you absorb an attack you are healed for 1/2 of that attack's damage
A daedalus crystal floats above you to protect you
Rogue projectiles throw out crystal shards as they travel
You can glide to negate fall damage
Effects of Scuttler's Jewel and Permafrost's Concoction");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = 500000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(64, 115, 164);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            CalamityPlayer player1 = player.Calamity();

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.DaedalusEffects))
            {
                player1.daedalusAbsorb = true;
                player1.daedalusReflect = true;
                player1.daedalusSplit = true;
                player1.daedalusShard = true;
            }
            
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.PermafrostPotion))
            {
                //permafrost concoction
                calamity.Find<ModItem>("PermafrostsConcoction").UpdateAccessory(player, hideVisual);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.DaedalusMinion) && player.whoAmI == Main.myPlayer)
            {
                player.Calamity().daedalusCrystal = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source = player.GetSource_ItemUse(base.Item, null);
                    if (player.FindBuffIndex(ModContent.BuffType<DaedalusCrystalBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<DaedalusCrystalBuff>(), 0xe10, true, false);
                    }
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<DaedalusCrystal>()] < 1)
                    {
                        Vector2 pos = new Vector2(player.Center.X, player.Center.Y);
                        Vector2 vel = new Vector2(0, -1);
                        int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(0x5ff);
                        Projectile.NewProjectileDirect(source, pos, vel, ModContent.ProjectileType<DaedalusCrystal>(), num, 0f, Main.myPlayer, 50f, 0f).originalDamage = 0x5f;
                    }
                }

            }

            Mod.Find<ModItem>("SnowRuffianEnchant").UpdateAccessory(player, hideVisual);            
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("DaedalusHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DaedalusBreastplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DaedalusLeggings").Type);

            recipe.AddIngredient(Mod.Find<ModItem>("SnowRuffianEnchant").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("PermafrostsConcoction").Type);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
