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
            {
                foreach (Def def in mcp.AllDefs)
                {
                    if (!def.description.NullOrEmpty())
                        def.description += $"\n({mcp.Name})";

                    if(def is TraitDef trd)
                    {
                        foreach (TraitDegreeData trdd in trd.degreeDatas)
                            trdd.description += $"\n({mcp.Name})";
                    }

                    if (BackstoryDatabase.allBackstories.TryGetValue(key: def.defName, value: out Backstory bs))
                        bs.baseDesc += $"\n({mcp.Name})";
                }
            }
        }
    }
}
