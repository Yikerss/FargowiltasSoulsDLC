using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class GodSlayerEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Slayer Enchantment");
            Tooltip.SetDefault(
@"'The power to slay gods resides within you...'
You will survive fatal damage and will be healed 150 HP if an attack would have killed you
This effect can only occur once every 45 seconds
Taking over 80 damage in one hit will cause you to release a swarm of high-damage god killer darts
An attack that would deal 80 damage or less will have its damage reduced to 1
Your ranged critical hits have a chance to critically hit, causing 4 times the damage
You have a chance to fire a god killer shrapnel round while firing ranged weapons
Enemies will release god slayer flames and healing flames when hit with magic attacks
Taking damage will cause you to release a magical god slayer explosion
Hitting enemies will summon god slayer phantoms
Summons a god-eating mechworm to fight for you
While at full HP all of your rogue stats are boosted by 10%
If you take over 80 damage in one hit you will be given extra immunity frames
Effects of the Nebulous Core and Draedon's Heart");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(100, 108, 156);
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
            Item.value = 10000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.GodSlayerEffects))
            {
                calamity.Call("SetSetBonus", player, "godslayer", true);
                calamity.Call("SetSetBonus", player, "godslayer_melee", true);
                calamity.Call("SetSetBonus", player, "godslayer_ranged", true);
                calamity.Call("SetSetBonus", player, "godslayer_magic", true);
                calamity.Call("SetSetBonus", player, "godslayer_rogue", true);
            }
            
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.MechwormMinion))
            {
                //summon
                calamity.Call("SetSetBonus", player, "godslayer_summon", true);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("Mechworm").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("Mechworm").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("MechwormHead").Type] < 1)
                    {
                        int whoAmI = player.whoAmI;
                        int num = calamity.Find<ModProjectile>("MechwormHead").Type;
                        int num2 = calamity.Find<ModProjectile>("MechwormBody").Type;
                        int num3 = calamity.Find<ModProjectile>("MechwormBody").Type;
                        int num4 = calamity.Find<ModProjectile>("MechwormTail").Type;
                        for (int i = 0; i < 1000; i++)
                        {
                            if (Main.projectile[i].active && Main.projectile[i].owner == whoAmI && (Main.projectile[i].type == num || Main.projectile[i].type == num4 || Main.projectile[i].type == num2 || Main.projectile[i].type == num3))
                            {
                                Main.projectile[i].Kill();
                            }
                        }
                        int num5 = player.maxMinions;
                        if (num5 > 10)
                        {
                            num5 = 10;
                        }
                        int num6 = (int)(35f * (player.GetDamage(DamageClass.Summon).Additive * 5f / 3f + player.GetDamage(DamageClass.Summon).Additive * 0.46f * (num5 - 1)));
                        Vector2 value = player.RotatedRelativePoint(player.MountedCenter, true);
                        Vector2 value2 = Utils.RotatedBy(Vector2.UnitX, player.fullRotation, default(Vector2));
                        Vector2 value3 = Main.MouseWorld - value;
                        float num7 = Main.mouseX + Main.screenPosition.X - value.X;
                        float num8 = Main.mouseY + Main.screenPosition.Y - value.Y;
                        if (player.gravDir == -1f)
                        {
                            num8 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - value.Y;
                        }
                        float num9 = (float)Math.Sqrt((num7 * num7 + num8 * num8));
                        if ((float.IsNaN(num7) && float.IsNaN(num8)) || (num7 == 0f && num8 == 0f))
                        {
                            num7 = player.direction;
                            num8 = 0f;
                            num9 = 10f;
                        }
                        else
                        {
                            num9 = 10f / num9;
                        }
                        num7 *= num9;
                        num8 *= num9;
                        int num10 = -1;
                        int num11 = -1;
                        for (int j = 0; j < 1000; j++)
                        {
                            if (Main.projectile[j].active && Main.projectile[j].owner == whoAmI)
                            {
                                if (num10 == -1 && Main.projectile[j].type == num)
                                {
                                    num10 = j;
                                }
                                else if (num11 == -1 && Main.projectile[j].type == num4)
                                {
                                    num11 = j;
                                }
                                if (num10 != -1 && num11 != -1)
                                {
                                    break;
                                }
                            }
                        }
                        if (num10 == -1 && num11 == -1)
                        {
                            float num12 = Vector2.Dot(value2, value3);
                            if (num12 > 0f)
                            {
                                player.ChangeDir(1);
                            }
                            else
                            {
                                player.ChangeDir(-1);
                            }
                            num7 = 0f;
                            num8 = 0f;
                            value.X = Main.mouseX + Main.screenPosition.X;
                            value.Y = Main.mouseY + Main.screenPosition.Y;
                            int num13 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormHead").Type, num6, 1f, whoAmI, 0f, 0f);
                            int num14 = num13;
                            num13 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody").Type, num6, 1f, whoAmI, num14, 0f);
                            num14 = num13;
                            num13 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody").Type, num6, 1f, whoAmI, num14, 0f);
                            Main.projectile[num14].localAI[1] = num13;
                            Main.projectile[num14].netUpdate = true;
                            num14 = num13;
                            num13 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormTail").Type, num6, 1f, whoAmI, num14, 0f);
                            Main.projectile[num14].localAI[1] = num13;
                            Main.projectile[num14].netUpdate = true;
                        }
                        else if (num10 != -1 && num11 != -1)
                        {
                            int num15 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody").Type, num6, 1f, whoAmI, Main.projectile[num11].ai[0], 0f);
                            int num16 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody").Type, num6, 1f, whoAmI, num15, 0f);
                            Main.projectile[num15].localAI[1] = num16;
                            Main.projectile[num15].ai[1] = 1f;
                            Main.projectile[num15].minionSlots = 0f;
                            Main.projectile[num15].netUpdate = true;
                            Main.projectile[num16].localAI[1] = num11;
                            Main.projectile[num16].netUpdate = true;
                            Main.projectile[num16].minionSlots = 0f;
                            Main.projectile[num16].ai[1] = 1f;
                            Main.projectile[num11].ai[0] = num16;
                            Main.projectile[num11].netUpdate = true;
                            Main.projectile[num11].ai[1] = 1f;
                        }
                    }
                }
            }
            
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.NebulousCore))
            {
                calamity.Find<ModItem>("NebulousCore").UpdateAccessory(player, hideVisual);
            }

            //draedons heart
            calamity.Find<ModItem>("DraedonsHeart").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("GodSlayerHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("GodSlayerChestplate").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("GodSlayerLeggings").Type);

            recipe.AddIngredient(calamity.Find<ModItem>("NebulousCore").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DimensionalSoulArtifact").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("DraedonsHeart").Type);

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
