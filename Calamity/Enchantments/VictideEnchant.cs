using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class VictideEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Victide Enchantment");
            Tooltip.SetDefault(
@"'The former seas have energized you…'
When using any weapon you have a 10% chance to throw a returning seashell projectile
This seashell does true damage and does not benefit from any damage class
Summons a sea urchin to protect you
Effects of Ocean's Crest and Luxor's Gift");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 80000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(67, 92, 191);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            //all
            calamity.Call("SetSetBonus", player, "victide", true);

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.UrchinMinion))
            {
                //summon
                calamity.Call("SetSetBonus", player, "victide_summon", true);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("Urchin").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("Urchin").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("Urchin").Type] < 1)
                    {
                        Projectile.NewProjectile(player.GetSource_Misc(""), player.Center.X, player.Center.Y, 0f, -1f, calamity.Find<ModProjectile>("Urchin").Type, (int)(7f * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            calamity.Find<ModItem>("OceanCrest").UpdateAccessory(player, hideVisual);

            //calamity.GetItem("DeepDiver").UpdateAccessory(player, hideVisual);
            //calamity.GetItem("TheTransformer").UpdateAccessory(player, hideVisual);

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.LuxorGift))
                calamity.Find<ModItem>("LuxorsGift").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("VictideHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("VictideBreastplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("VictideLeggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("OceanCrest").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("LuxorsGift").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TeardropCleaver").Type);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
