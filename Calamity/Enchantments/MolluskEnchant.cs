using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
Two shellfishes aid you in combat
When using any weapon you have a 10% chance to throw a returning seashell projectile
Summons a sea urchin to protect you
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
                //set bonus clams
                calamity.Call("SetSetBonus", player, "mollusk", true);
                player.maxMinions += 4;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("Shellfish").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("Shellfish").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("Shellfish").Type] < 2)
                    {
                        Projectile.NewProjectile(player.GetSource_Misc(""), player.Center.X, player.Center.Y, 0f, -1f, calamity.Find<ModProjectile>("Shellfish").Type, (int)(1500.0 * (double)player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
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
