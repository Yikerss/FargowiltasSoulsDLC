using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class SilvaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public int dragonTimer = 60;

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silva Enchantment");
            Tooltip.SetDefault(
@"'Boundless life energy cascades from you...'
You are immune to almost all debuffs
Reduces all damage taken by 5%, this is calculated separately from damage reduction
All projectiles spawn healing leaf orbs on enemy hits
Max run speed and acceleration boosted by 5%
If you are reduced to 0 HP you will not die from any further damage for 10 seconds
If you get reduced to 0 HP again while this effect is active you will lose 100 max life
This effect only triggers once per life
Your max life will return to normal if you die
True melee strikes have a 25% chance to do five times damage
Melee projectiles have a 25% chance to stun enemies for a very brief moment
Increases your rate of fire with all ranged weapons
Magic projectiles have a 10% chance to cause a massive explosion on enemy hits
Summons an ancient leaf prism to blast your enemies with life energy
Rogue weapons have a faster throwing rate while you are above 90% life
Effects of the The Amalgam, Godly Soul Artifact, and Yharim's Gift");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(176, 112, 70);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 20000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.SilvaEffects))
            {
                calamity.Call("SetSetBonus", player, "silva", true);
                calamity.Call("SetSetBonus", player, "silva_melee", true);
                calamity.Call("SetSetBonus", player, "silva_ranged", true);
                calamity.Call("SetSetBonus", player, "silva_magic", true);
                calamity.Call("SetSetBonus", player, "silva_rogue", true);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.SilvaMinion))
            {
                //summon
                calamity.Call("SetSetBonus", player, "silva_summon", true);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("SilvaCrystal").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("SilvaCrystal").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("SilvaCrystal").Type] < 1)
                    {
                        Projectile.NewProjectile(player.GetSource_Misc(""), player.Center.X, player.Center.Y, 0f, -1f, calamity.Find<ModProjectile>("SilvaCrystal").Type, (int)(1500.0 * (double)player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.FungalMinion))
            {
                calamity.Find<ModItem>("TheAmalgam").UpdateAccessory(player, hideVisual);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.GodlySoulArtifact))
            {
                calamity.Find<ModItem>("GodlySoulArtifact").UpdateAccessory(player, hideVisual);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.YharimGift))
            {
                calamity.Find<ModItem>("YharimsGift").UpdateAccessory(player, hideVisual);
            }

            calamity.Find<ModItem>("DynamoStemCells").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("SilvaHelmet").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SilvaArmor").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("SilvaLeggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("TheAmalgam").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("GodlySoulArtifact").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("YharimsGift").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DynamoStemCells").Type);

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
