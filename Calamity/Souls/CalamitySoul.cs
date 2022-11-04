using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSoulsDLC.Calamity.Souls
{
    public class CalamitySoul : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public int dragonTimer = 60;
        public const int FireProjectiles = 2;
        public const float FireAngleSpread = 120f;
        public int FireCountdown;

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Tyrant");
            Tooltip.SetDefault(
@"'And the land grew quiet once more...'
All armor bonuses from Aerospec, Statigel, and Hydrothermic
All armor bonuses from Xeroc and Fearmonger
All armor bonuses from Daedalus, Snow Ruffian, Umbraphile, and Astral
All armor bonuses from Omega Blue, Mollusk, Victide, Fathom Swarmer, and Sulphurous
All armor bonuses from Wulfrum, Reaver, Plague Reaper, and Demonshade
All armor bonuses from Tarragon, Bloodflare, and Brimflame
All armor bonuses from God Slayer, Silva, and Auric
Effects of Gladiator's Locket and Unstable Prism
Effects of Counter Scarf and Fungal Symbiote
Effects of Hallowed Rune, Ethereal Extorter, and The Community
Effects of Spectral Veil and Statis' Void Sash
Effects of Scuttler's Jewel and Permafrost's Concoction
Effects of Thief's Dime, Vampiric Talisman, and Momentum Capacitor
Effects of the Astral Arcanum and Gravistar Sabaton
Effects of the Abyssal Diving Suit and Mutated Truffle
Effects of Giant Pearl and Amidias' Pendant
Effects of Aquatic Emblem and Enchanted Pearl
Effects of Ocean's Crest and Luxor's Gift
Effects of Corrosive Spine and Lumenous Amulet
Effects of Sand Cloak and Alluring Bait
Effects of Trinket of Chi and Plague Hive
Effects of Plagued Fuel Pack, The Camper, and Profaned Soul Crystal
Effects of Blazing Core, Dark Sun Ring, and Core of the Blood God
Effects of Nebulous Core and Draedon's Heart
Effects of the The Amalgam and Godly Soul Artifact
Effects of Yharim's Gift, Heart of the Elements, and The Sponge");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;//
            Item.value = 20000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Mod.Find<ModItem>("AnnihilationForce").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("DesolationForce").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("DevastationForce").UpdateAccessory(player, hideVisual);
            Mod.Find<ModItem>("ExaltationForce").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!FargowiltasSoulsDLC.Instance.CalamityLoaded) return;

            Recipe recipe = CreateRecipe();

            Mod fargo = ModLoader.GetMod("FargowiltasSouls");

            recipe.AddIngredient(null, "AnnihilationForce");
            recipe.AddIngredient(null, "DevastationForce");
            recipe.AddIngredient(null, "DesolationForce");
            recipe.AddIngredient(null, "ExaltationForce");
            recipe.AddIngredient(fargo.Find<ModItem>("AbomEnergy"), 10);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
