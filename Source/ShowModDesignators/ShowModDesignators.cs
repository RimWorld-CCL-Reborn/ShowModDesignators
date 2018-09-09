using System.Linq;

namespace ShowModDesignators
{
    using System;
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
                    try
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
                                    if(td.race.meatDef != null)
                                        td.race.meatDef.description += nameAdd;
                                    if(td.race.corpseDef != null)
                                        td.race.corpseDef.description += nameAdd;
                                    if(td.race.leatherDef != null)
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
                    catch (Exception)
                    {
                        Log.Error(text: $"ModDesignator: {def.defName} of {def.GetType()} is evil");
                    }
                }
        }
    }
}
