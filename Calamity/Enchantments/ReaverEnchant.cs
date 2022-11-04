using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class ReaverEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Enchantment");
            Tooltip.SetDefault(
@"'A thorny death awaits your enemies...'
Melee projectiles explode on hit
While using a ranged weapon you have a 10% chance to fire a powerful rocket
Your magic projectiles emit a burst of spore gas on enemy hits
Summons a reaver orb that emits spore gas when enemies are near
You emit a cloud of spores when you are hit
Rage activates when you are damaged");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = 400000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(54, 164, 66);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ReaverEffects))
            {
                calamity.Call("SetSetBonus", player, "reaver", true);
                calamity.Call("SetSetBonus", player, "reaver_melee", true);
                calamity.Call("SetSetBonus", player, "reaver_ranged", true);
                calamity.Call("SetSetBonus", player, "reaver_magic", true);
                calamity.Call("SetSetBonus", player, "reaver_rogue", true);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ReaverMinion))
            {
                calamity.Call("SetSetBonus", player, "reaver_summon", true);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("ReaverOrb").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("ReaverOrb").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("ReaverOrb").Type] < 1)
                    {
                        Projectile.NewProjectile(player.GetSource_Misc(""), player.Center.X, player.Center.Y, 0f, -1f, calamity.Find<ModProjectile>("ReaverOrb").Type, (int)(80f * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("ReaverHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ReaverScaleMail").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ReaverCuisses").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("SandSharknadoStaff").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("Triploon").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("MagnaStriker").Type);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
