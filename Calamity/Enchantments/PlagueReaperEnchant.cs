using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

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

            calamity.Call("SetSetBonus", player, "plaguereaper", true);
            //meme
            if (player.whoAmI == Main.myPlayer && player.immune && Utils.NextBool(Main.rand, 10))
            {
                for (int j = 0; j < 1; j++)
                {
                    float num2 = player.position.X + (float)Main.rand.Next(-400, 400);
                    float num3 = player.position.Y - (float)Main.rand.Next(500, 800);
                    Vector2 vector = new Vector2(num2, num3);
                    float num4 = player.position.X + (float)(player.width / 2) - vector.X;
                    float num5 = player.position.Y + (float)(player.height / 2) - vector.Y;
                    num4 += (float)Main.rand.Next(-100, 101);
                    float num6 = (float)22;
                    float num7 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                    num7 = num6 / num7;
                    num4 *= num7;
                    num5 *= num7;
                    int num8 = Projectile.NewProjectile(player.GetSource_Misc(""),num2, num3, num4, num5, calamity.Find<ModProjectile>("TheSyringeCinder").Type, 40, 4f, player.whoAmI, 0f, 0f);
                    Main.projectile[num8].ai[1] = player.position.Y;
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
