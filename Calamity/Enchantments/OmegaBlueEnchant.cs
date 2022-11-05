using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Cooldowns;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class OmegaBlueEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Omega Blue Enchantment");
            Tooltip.SetDefault(
@"'The darkness of the Abyss has overwhelmed you...'
Increases armor penetration by 100
Short-ranged tentacles heal you by sucking enemy life
Press Y to activate abyssal madness for 5 seconds
Abyssal madness increases damage, critical strike chance, and tentacle aggression/range
This effect has a 30 second cooldown
Effects of the Abyssal Diving Suit and Mutated Truffle");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 13;
            Item.value = 1000000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(35, 95, 161);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.OmegaTentacles))
            {
                CalamityMod.Cooldowns.CooldownInstance instance;
                 
                float armorPenetration = player.GetArmorPenetration<GenericDamageClass>();
                armorPenetration += 15f;
                player.maxMinions += 2;
                CalamityMod.CalPlayer.CalamityPlayer player1 = player.Calamity();
                player1.wearingRogueArmor = true;
                player1.omegaBlueSet = true;

                player1.WearingPostMLSummonerSet = true;
                if (player1.cooldowns.TryGetValue(OmegaBlue.ID, out instance) && (instance.timeLeft > 0x5dc))
                {
                    int index = Dust.NewDust(player.position, player.width, player.height, DustID.PurificationPowder, 0f, 0f, 100, Color.Transparent, 1.6f);
                    Main.dust[index].noGravity = true;
                    Main.dust[index].noLight = true;
                    Main.dust[index].fadeIn = 1f;
                    Dust dust1 = Main.dust[index];
                    dust1.velocity *= 3f;
                }
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.DivingSuit))
            {
                calamity.Find<ModItem>("AbyssalDivingSuit").UpdateAccessory(player, hideVisual);
            }

            if (SoulConfig.Instance.calamityToggles.ReaperToothNecklace)
            {
                calamity.Find<ModItem>("ReaperToothNecklace").UpdateAccessory(player, hideVisual);
            }

            if (SoulConfig.Instance.calamityToggles.MutatedTruffle)
            {
                calamity.Find<ModItem>("MutatedTruffle").UpdateAccessory(player, hideVisual);
            }

        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("OmegaBlueHelmet").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("OmegaBlueChestplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("OmegaBlueLeggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("AbyssalDivingSuit").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ReaperToothNecklace").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("MutatedTruffle"));
            
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
