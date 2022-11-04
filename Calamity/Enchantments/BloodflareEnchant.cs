using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class BloodflareEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodflare Enchantment");
            Tooltip.SetDefault(
@"'The souls of the fallen are at your disposal...'
Press Y to trigger a brimflame frenzy effect
While under this effect, your damage is significantly boosted
However, this comes at the cost of rapid life loss and no mana regeneration
Enemies below 50% life have a chance to drop hearts when struck
Enemies above 50% life have a chance to drop mana stars when struck
Enemies killed during a Blood Moon have a much higher chance to drop Blood Orbs
True melee strikes will heal you
After striking an enemy 15 times with true melee you will enter a blood frenzy for 5 seconds
During this you will gain 25% increased melee damage, critical strike chance, and contact damage is halved
This effect has a 30 second cooldown
Press Y to unleash the lost souls of polterghast to destroy your enemies
This effect has a 30 second cooldown
Ranged weapons have a chance to fire bloodsplosion orbs
Magic weapons will sometimes fire ghostly bolts
Magic critical strikes cause flame explosions every 2 seconds
Summons polterghast mines to circle you
Rogue critical strikes have a 50% chance to heal you
Effects of the Core of the Blood God and Eldritch Soul Artifact");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(191, 68, 59);
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
            Item.value = 3000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.BloodflareEffects))
            {
                calamity.Call("SetSetBonus", player, "bloodflare", true);
                calamity.Call("SetSetBonus", player, "bloodflare_melee", true);
                calamity.Call("SetSetBonus", player, "bloodflare_ranged", true);
                calamity.Call("SetSetBonus", player, "bloodflare_magic", true);
                calamity.Call("SetSetBonus", player, "bloodflare_rogue", true);
            }
           
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.PolterMines))
            {
                calamity.Call("SetSetBonus", player, "bloodflare_summon", true);
            }

            calamity.Find<ModItem>("CoreOfTheBloodGod").UpdateAccessory(player, hideVisual);
            calamity.Find<ModItem>("EldritchSoulArtifact").UpdateAccessory(player, hideVisual);
            //brimflame
            Mod.Find<ModItem>("BrimflameEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("BloodflareHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("BloodflareBodyArmor").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("BloodflareCuisses").Type);

            Mod mod = ModLoader.GetMod("FargowiltasSoulsDlc");

            recipe.AddIngredient(mod.Find<ModItem>("BrimflameEnchant").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("CoreOfTheBloodGod").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("EldritchSoulArtifact").Type);


            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
