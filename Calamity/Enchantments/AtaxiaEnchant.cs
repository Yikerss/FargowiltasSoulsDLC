using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class AtaxiaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydrothermic Enchantment");
            Tooltip.SetDefault(
@"''
You have a 20% chance to emit a blazing explosion on hit
Melee attacks and projectiles cause chaos flames to erupt on enemy hits
You have a 50% chance to fire a homing chaos flare when using ranged weapons
Magic attacks summon damaging and healing flare orbs on hit
Summons a hydrothermic vent to protect you
Rogue weapons have a 10% chance to unleash a volley of chaos flames around the player
Effects of Hallowed Rune and Ethereal Extorter");

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 1000000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(194, 89, 89);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.AtaxiaEffects))
            {
                calamity.Call("SetSetBonus", player, "hydrothermic", true);
                calamity.Call("SetSetBonus", player, "hydrothermic_melee", true);
                calamity.Call("SetSetBonus", player, "hydrothermic_ranged", true);
                calamity.Call("SetSetBonus", player, "hydrothermic_magic", true);
                calamity.Call("SetSetBonus", player, "hydrothermic_rogue", true);
            }


            if (SoulConfig.Instance.calamityToggles.HallowedRune)
                calamity.Find<ModItem>("HallowedRune").UpdateAccessory(player, hideVisual);

            if (SoulConfig.Instance.calamityToggles.EtherealExtorter)
                calamity.Find<ModItem>("EtherealExtorter").UpdateAccessory(player, hideVisual);


            //summon
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ChaosMinion))
            {
                calamity.Call("SetSetBonus", player, "hydrothermic_summon", true);
                calamity.Call("ataxiaBlaze", player, true);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("HydrothermicVentBuff").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("HydrothermicVentBuff").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("HydrothermicVent").Type] < 1)
                    {
                        Vector2 vec = new Vector2(player.Center.X, player.Center.Y);
                        Vector2 floatVec = new Vector2(0f, -1f);

                        Projectile.NewProjectile(player.GetSource_Misc(""), vec, floatVec, calamity.Find<ModProjectile>("HydrothermicVent").Type, (int)(190f * player.GetDamage(DamageClass.Summon).Additive), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("AtaxiaHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AtaxiaArmor").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AtaxiaSubligar").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("HallowedRune").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("EtherealExtorter").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("BarracudaGun").Type);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
