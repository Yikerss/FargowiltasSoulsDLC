using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;


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

            //set bonus
            calamity.Call("SetSetBonus", player, "demonshade", true);

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.RedDevilMinion))
            {
        
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("RedDevil").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("RedDevil").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("RedDevil").Type] < 1)
                    {
                        Projectile.NewProjectile(player.GetSource_Misc(""), player.Center.X, player.Center.Y, 0f, -1f, calamity.Find<ModProjectile>("RedDevil").Type, 10000, 0f, Main.myPlayer, 0f, 0f);
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
