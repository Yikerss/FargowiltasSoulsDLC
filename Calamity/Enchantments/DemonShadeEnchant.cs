using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;
using CalamityMod.CalPlayer;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Typeless;
using Terraria.DataStructures;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class DemonShadeEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonshade Enchantment");
            Tooltip.SetDefault(
@"'Demonic power emanates from you…'
All attacks inflict Demon Flames
Shadowbeams and Demon Scythes fall from the sky on hit
A friendly red devil follows you around
Press Y to enrage nearby enemies with a dark magic spell for 10 seconds
This makes them do 1.5 times more damage but they also take five times as much damage
Effects of Profaned Soul Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(173, 52, 70);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            CalamityPlayer player1 = player.Calamity();
            //set bonus
            player1.dsSetBonus = true;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.RedDevilMinion))
            {

                if (player.whoAmI == Main.myPlayer)
                {
                    player1.redDevil = true;
                    IEntitySource source = player.GetSource_ItemUse(base.Item, null);
                    if (player.FindBuffIndex(ModContent.BuffType<DemonshadeSetDevilBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<DemonshadeSetDevilBuff>(), 0xe10, true, false);
                    }
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonshadeRedDevil>()] < 1)
                    {
                        Vector2 pos = new Vector2(player.Center.X, player.Center.Y);
                        Vector2 vel = new Vector2(0, -1);
                        int num = 0x2710;
                        int num2 = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(0x2710f);
                        Projectile.NewProjectileDirect(source, pos, vel, ModContent.ProjectileType<DemonshadeRedDevil>(), num2, 0f, Main.myPlayer, 0f, 0f).originalDamage = num;
                    }
                }

            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ProfanedSoulCrystal))
            {
                calamity.Find<ModItem>("ProfanedSoulCrystal").UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(calamity.Find<ModItem>("DemonshadeHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DemonshadeBreastplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DemonshadeGreaves").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("ProfanedSoulCrystal").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Apotheosis").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Eternity").Type);

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
