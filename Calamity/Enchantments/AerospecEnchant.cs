using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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

            calamity.Call("SetSetBonus", player, "aerospec", true);
            player.noFallDmg = true;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ValkyrieMinion))
            {

                calamity.Call("SetSetBonus", player, "aerospec_summon", true);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("ValkyrieBuff").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("ValkyrieBuff").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("Valkyrie").Type] < 1)
                    {
                        Vector2 vec = new Vector2(player.Center.X, player.Center.Y);
                        Vector2 floatVec = new Vector2(0f, -1f);

                        Projectile.NewProjectile(player.GetSource_Misc(""), vec, floatVec, calamity.Find<ModProjectile>("Valkyrie").Type, 25, 0f, Main.myPlayer, 0f, 0f);
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
