using System;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSoulsDLC
{
    public class FargowiltasSoulsDLC : Mod
    {
        internal static FargowiltasSoulsDLC Instance;

        internal bool CalamityLoaded;
        private static readonly Mod mod = ModLoader.GetMod("CalamityMod");
        public static Mod calamity = mod;

        public override void Load()
        {
            Instance = this;

            if (calamity != null)
            {
                AddToggle("CalamityHeader", "Calamity Toggles", "CalamitySoul", "ffffff");
                //AddToggle("CalamityElementalQuiverConfig", "Elemental Quiver", "SnipersSoul", "ffffff");

                AddToggle("AnnihilationForce", "Force of Annihilation", "AnnihilationForce", "ffffff");
                AddToggle("CalamityValkyrieMinionConfig", "Valkyrie Minion", "AerospecEnchant", "ffffff");
                AddToggle("CalamityGladiatorLocketConfig", "Gladiator's Locket", "AerospecEnchant", "ffffff");
                AddToggle("CalamityUnstablePrismConfig", "Unstable Prism", "AerospecEnchant", "ffffff");
                AddToggle("CalamityFungalSymbiote", "Fungal Symbiote", "StatigelEnchant", "ffffff");
                AddToggle("CalamityAtaxiaEffectsConfig", "Ataxia Effects", "AtaxiaEnchant", "ffffff");
                AddToggle("CalamityChaosMinionConfig", "Chaos Spirit Minion", "AtaxiaEnchant", "ffffff");
                AddToggle("CalamityHallowedRuneConfig", "Hallowed Rune", "AtaxiaEnchant", "ffffff");
                AddToggle("CalamityEtherealExtorterConfig", "Ethereal Extorter", "AtaxiaEnchant", "ffffff");
                AddToggle("CalamityXerocEffectsConfig", "Xeroc Effects", "XerocEnchant", "ffffff");

                AddToggle("CalamityStatisBeltOfCursesConfig", "Statis' Void Sash", "FearmongerEnchant", "ffffff");

                AddToggle("DevastationForce", "Force of Devastation", "DevastationForce", "ffffff");
                AddToggle("CalamityReaverEffectsConfig", "Reaver Effects", "ReaverEnchant", "ffffff");
                AddToggle("CalamityReaverMinionConfig", "Reaver Orb Minion", "ReaverEnchant", "ffffff");
                AddToggle("CalamityPlagueHiveConfig", "Plague Hive", "PlagueReaperEnchant", "ffffff");
                AddToggle("CalamityPlaguedFuelPackConfig", "Plague Fuel Pack", "PlagueReaperEnchant", "ffffff");
                AddToggle("CalamityTheCamperConfig", "The Camper", "PlagueReaperEnchant", "ffffff");
                AddToggle("CalamityDevilMinionConfig", "Red Devil Minion", "DemonShadeEnchant", "ffffff");
                AddToggle("CalamityProfanedSoulConfig", "Profaned Soul Crystal", "DemonShadeEnchant", "ffffff");


                AddToggle("DesolationForce", "Force of Desolation", "DesolationForce", "ffffff");
                AddToggle("CalamitySnowRuffianWingsConfig", "Snow Ruffian Wings", "SnowRuffianEnchant", "ffffff");
                AddToggle("CalamityDaedalusEffectsConfig", "Daedalus Effects", "DaedalusEnchant", "ffffff");
                AddToggle("CalamityDaedalusMinionConfig", "Daedalus Crystal Minion", "DaedalusEnchant", "ffffff");
                AddToggle("CalamityPermafrostPotionConfig", "Permafrost's Concoction", "DaedalusEnchant", "ffffff");

                AddToggle("CalamityAstralStarsConfig", "Astral Stars", "AstralEnchant", "ffffff");
                AddToggle("CalamityGravistarSabatonConfig", "GravistarSabaton", "AstralEnchant", "ffffff");

                AddToggle("CalamityOmegaTentaclesConfig", "Omega Blue Tentacles", "OmegaBlueEnchant", "ffffff");
                AddToggle("CalamityDivingSuitConfig", "Abyssal Diving Suit", "OmegaBlueEnchant", "ffffff");
                AddToggle("CalamityReaperToothNecklaceConfig", "Reaper Tooth Necklace", "OmegaBlueEnchant", "ffffff");
                AddToggle("CalamityMutatedTruffleConfig", "Mutated Truffle", "OmegaBlueEnchant", "ffffff");
                AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "VictideEnchant", "ffffff");
                AddToggle("CalamityLuxorGiftConfig", "Luxor's Gift", "VictideEnchant", "ffffff");

                AddToggle("CalamityBloodflareEffectsConfig", "Bloodflare Effects", "BloodflareEnchant", "ffffff");
                AddToggle("CalamityPolterMinesConfig", "Polterghast Mines", "BloodflareEnchant", "ffffff");

                AddToggle("CalamitySilvaEffectsConfig", "Silva Effects", "SilvaEnchant", "ffffff");
                AddToggle("CalamitySilvaMinionConfig", "Silva Crystal Minion", "SilvaEnchant", "ffffff");
                AddToggle("CalamityGodlyArtifactConfig", "Godly Soul Artifact", "SilvaEnchant", "ffffff");
                AddToggle("CalamityYharimGiftConfig", "Yharim's Gift", "SilvaEnchant", "ffffff");
                AddToggle("CalamityFungalMinionConfig", "Fungal Clump Minion", "SilvaEnchant", "ffffff");
                AddToggle("CalamityPoisonSeawaterConfig", "Poisonous Sea Water", "SilvaEnchant", "ffffff");

                AddToggle("CalamityGodSlayerEffectsConfig", "God Slayer Effects", "GodSlayerEnchant", "ffffff");
                AddToggle("CalamityMechwormMinionConfig", "Mechworm Minion", "GodSlayerEnchant", "ffffff");
                AddToggle("CalamityNebulousCoreConfig", "Nebulous Core", "GodSlayerEnchant", "ffffff");
                AddToggle("CalamityAuricEffectsConfig", "Auric Tesla Effects", "AuricEnchant", "ffffff");
                AddToggle("CalamityWaifuMinionsConfig", "Elemental Waifus", "AuricEnchant", "ffffff");

                AddToggle("CalamityShellfishMinionConfig", "Shellfish Minions", "MolluskEnchant", "ffffff");
                AddToggle("CalamityGiantPearlConfig", "Giant Pearl", "MolluskEnchant", "ffffff");

                AddToggle("CalamityTarragonEffectsConfig", "Tarragon Effects", "TarragonEnchant", "ffffff");

            }
        }

        public void AddToggle(String toggle, String name, String item, String color)
        {
            ModTranslation text = LocalizationLoader.CreateTranslation(this, toggle);
            text.SetDefault("[i:" + Instance.Find<ModItem>(item).Type + "][c/" + color + ": " + name + "]");
            LocalizationLoader.AddTranslation(text);
        }

        public override void PostSetupContent()
        {
            try
            {
                CalamityLoaded = ModLoader.GetMod("CalamityMod") != null;
            }
            catch (Exception e)
            {
                Logger.Error("FargowiltasSoulsDLC PostSetupContent Error: " + e.StackTrace + e.Message);
            }
        }


    }
}