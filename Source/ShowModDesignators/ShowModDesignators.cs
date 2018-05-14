using System.Linq;

namespace ShowModDesignators
{
    using JetBrains.Annotations;
    using RimWorld;
    using Verse;

    [StaticConstructorOnStartup]
    [UsedImplicitly]
    public class ShowModDesignators
    {
        static ShowModDesignators()
        {
            foreach (ModContentPack mcp in LoadedModManager.RunningMods.Where(predicate: mcp => !mcp.IsCoreMod))
                foreach (Def def in mcp.AllDefs)
                {
                    string nameAdd = $"\n({mcp.Name})";

                    if (!def.description.NullOrEmpty())
                        def.description += nameAdd;

                    switch (def)
                    {
                        case TraitDef trd:
                            foreach (TraitDegreeData trdd in trd.degreeDatas)
                                trdd.description += nameAdd;
                            break;
                        case ThingDef td:
                            if (td.race != null)
                            {
                                td.race.meatDef.description += nameAdd;
                                td.race.corpseDef.description += nameAdd;
                                td.race.leatherDef.description += nameAdd;
                            }

                            /*if (td.recipeMaker != null)
                                DefDatabase<RecipeDef>.GetNamed(defName: "Make_" + td.defName).description += nameAdd;
                            if(td.IsDrug)
                                DefDatabase<RecipeDef>.GetNamed(defName: "Administer_" + td.defName).description += nameAdd;*/
                            break;
                    }

                    if (BackstoryDatabase.allBackstories.TryGetValue(key: def.defName, value: out Backstory bs))
                        bs.baseDesc += nameAdd;
                }
        }
    }
}
