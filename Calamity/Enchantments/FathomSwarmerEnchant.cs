using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class FathomSwarmerEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fathom Swarmer Enchantment");
            Tooltip.SetDefault(
@"''
10% increased minion damage while submerged in liquid
Provides a moderate amount of light and moderately reduces breath loss in the abyss
Attacking and being attacked by enemies inflicts poison
Grants a sulphurous bubble jump that applies venom on hit
Effects of Corrosive Spine and Lumenous Amulet
Effects of Sand Cloak and Alluring Bait");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Lime;
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

            calamity.Call("SetSetBonus", player, "fathomswarmer", true);
            if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
            {
                player.GetDamage(DamageClass.Summon) += 0.1f;
            }

            calamity.Find<ModItem>("CorrosiveSpine").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("LumenousAmulet").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("SulphurousEnchant").UpdateAccessory(player, hideVisual);

        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("FathomSwarmerVisage").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("FathomSwarmerBreastplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("FathomSwarmerBoots").Type);

            recipe.AddIngredient(Mod.Find<ModItem>("SulphurousEnchant").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("CorrosiveSpine").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("LumenousAmulet").Type);


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
