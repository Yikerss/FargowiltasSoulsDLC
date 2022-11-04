﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria.Localization;


namespace FargowiltasSoulsDLC.Calamity.Enchantments
{
    public class AuricEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auric Tesla Enchantment");
            Tooltip.SetDefault(
@"'Your strength rivals that of the Jungle Tyrant...'
All effects from Tarragon, Bloodflare, Godslayer and Silva armor
All attacks spawn healing auric orbs
Effects of Heart of the Elements and The Sponge");

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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(217, 142, 67);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;


            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.AuricEffects))
            {
                calamity.Call("SetSetBonus", player, "auric", true);

                //tarragaon
                calamity.Call("SetSetBonus", player, "tarragon", true);
                calamity.Call("SetSetBonus", player, "tarragon_melee", true);
                calamity.Call("SetSetBonus", player, "tarragon_ranged", true);
                calamity.Call("SetSetBonus", player, "tarragon_magic", true);
                calamity.Call("SetSetBonus", player, "tarragon_summon", true);
                calamity.Call("SetSetBonus", player, "tarragon_rogue", true);
                //bloodflare
                calamity.Call("SetSetBonus", player, "bloodflare", true);
                calamity.Call("SetSetBonus", player, "bloodflare_melee", true);
                calamity.Call("SetSetBonus", player, "bloodflare_ranged", true);
                calamity.Call("SetSetBonus", player, "bloodflare_magic", true);
                calamity.Call("SetSetBonus", player, "bloodflare_rogue", true);
                //godslayer
                calamity.Call("SetSetBonus", player, "godslayer", true);
                calamity.Call("SetSetBonus", player, "godslayer_melee", true);
                calamity.Call("SetSetBonus", player, "godslayer_ranged", true);
                calamity.Call("SetSetBonus", player, "godslayer_magic", true);
                calamity.Call("SetSetBonus", player, "godslayer_rogue", true);
                //silva
                calamity.Call("SetSetBonus", player, "silva", true);
                calamity.Call("SetSetBonus", player, "silva_melee", true);
                calamity.Call("SetSetBonus", player, "silva_ranged", true);
                calamity.Call("SetSetBonus", player, "silva_magic", true);
                calamity.Call("SetSetBonus", player, "silva_rogue", true);
            }

            //summon head
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.PolterMines))
                calamity.Call("SetSetBonus", player, "bloodflare_summon", true);
            

            if (player.whoAmI == Main.myPlayer)
            {
                if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.SilvaMinion))
                {
                    calamity.Call("SetSetBonus", player, "silva_summon", true);
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("SilvaCrystal").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("SilvaCrystal").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("SilvaCrystal").Type] < 1)
                    {
                        Projectile.NewProjectile(player.GetSource_Misc(""),player.Center.X, player.Center.Y, 0f, -1f, calamity.Find<ModProjectile>("SilvaCrystal").Type, (int)(3000.0 * (double)player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                    }
                }

                if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.MechwormMinion))
                {
                    calamity.Call("SetSetBonus", player, "godslayer_summon", true);
                    if (player.FindBuffIndex(calamity.Find<ModBuff>("Mechworm").Type) == -1)
                    {
                        player.AddBuff(calamity.Find<ModBuff>("Mechworm").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.Find<ModProjectile>("MechwormHead").Type] < 1)
                    {
                        int whoAmI = player.whoAmI;
                        int num = calamity.Find<ModProjectile>("MechwormHead").Type;
                        int num2 = calamity.Find<ModProjectile>("MechwormBody").Type;
                        int num3 = calamity.Find<ModProjectile>("MechwormBody2").Type;
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
                        int num6 = (int)(35f * player.GetDamage(DamageClass.Summon).Multiplicative * 5f / 3f + player.GetDamage(DamageClass.Summon).Multiplicative * 0.46f * (num5 - 1));
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
                            int num13 = Projectile.NewProjectile(player.GetSource_Misc(""),value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormHead").Type, num6, 1f, whoAmI, 0f, 0f);
                            int num14 = num13;
                            num13 = Projectile.NewProjectile(player.GetSource_Misc(""),value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody").Type, num6, 1f, whoAmI, num14, 0f);
                            num14 = num13;
                            num13 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody2").Type, num6, 1f, whoAmI, num14, 0f);
                            Main.projectile[num14].localAI[1] = num13;
                            Main.projectile[num14].netUpdate = true;
                            num14 = num13;
                            num13 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormTail").Type, num6, 1f, whoAmI, num14, 0f);
                            Main.projectile[num14].localAI[1] = num13;
                            Main.projectile[num14].netUpdate = true;
                            return;
                        }
                        if (num10 != -1 && num11 != -1)
                        {
                            int num15 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody").Type, num6, 1f, whoAmI, Main.projectile[num11].ai[0], 0f);
                            int num16 = Projectile.NewProjectile(player.GetSource_Misc(""), value.X, value.Y, num7, num8, calamity.Find<ModProjectile>("MechwormBody2").Type, num6, 1f, whoAmI, (float)num15, 0f);
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

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.WaifuMinions))
            {
                calamity.Find<ModItem>("HeartoftheElements").UpdateAccessory(player, hideVisual);
            }

            //the sponge
            calamity.Find<ModItem>("TheSponge").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(calamity.Find<ModItem>("AuricTeslaRoyalHelm").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AuricTeslaBodyArmor").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("AuricTeslaCuisses").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("HeartoftheElements").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("TheSponge").Type);
            recipe.AddIngredient(calamity.Find<ModItem>("ArkoftheCosmos").Type);

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
