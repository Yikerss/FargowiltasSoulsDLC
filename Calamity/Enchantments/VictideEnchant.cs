using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Terraria.DataStructures;

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
Summons a clam to protect you
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
            CalamityPlayer player1 = player.Calamity();

            player1.victideSet = true;
            player1.victideSummoner = true;
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.UrchinMinion))
            {
                player.maxMinions++;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<SeaSnailBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<SeaSnailBuff>(), 0xe10, true, false);
                    }
                    IEntitySource source = player.GetSource_ItemUse(base.Item, null);
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<VictideSeaSnail>()] < 1)
                    {
                        Vector2 pos = new Vector2(player.Center.X, player.Center.Y);
                        Vector2 vel = new Vector2(0, -1);
                        int num = 30;
                        int num2 = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo((float)num);
                        int index = Projectile.NewProjectile(source, pos, vel, ModContent.ProjectileType<VictideSeaSnail>(), num2, 0f, Main.myPlayer, 0f, 0f);
                        if (Utils.IndexInRange<Projectile>(Main.projectile, index))
                        {
                            Main.projectile[index].originalDamage = num;
                        }
                    }
                }
                player.ignoreWater = true;
                if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir, false))
                {

                }

                calamity.Find<ModItem>("OceanCrest").UpdateAccessory(player, hideVisual);

                //calamity.GetItem("DeepDiver").UpdateAccessory(player, hideVisual);
                //calamity.GetItem("TheTransformer").UpdateAccessory(player, hideVisual);

                if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.LuxorGift))
                    calamity.Find<ModItem>("LuxorsGift").UpdateAccessory(player, hideVisual);

            }
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
