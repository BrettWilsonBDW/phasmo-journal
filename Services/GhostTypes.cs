using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Phasmo
{
    public enum GhostEvidence
    {
        EMF_LEVEL_5 = 0,
        ULTRAVIOLET,
        GHOST_WRITING,
        FREEZING_TEMPERATURES,
        DOTS_PROJECTOR,
        GHOST_ORB,
        SPIRIT_BOX
    }

    public enum Ghosts
    {
        SPIRIT = 0,
        WRAITH,
        PHANTOM,
        POLTERGEIST,
        BANSHEE,
        JINN,
        MARE,
        REVENANT,
        SHADE,
        DEMON,
        YUREI,
        ONI,
        YOKAI,
        HANTU,
        GORYO,
        MYLING,
        ONRYO,
        THE_TWINS,
        RAIJU,
        OBAKE,
        THE_MIMIC,
        MOROI,
        DEOGEN,
        THAYE,
        INVALID = -1
    }

    internal class GhostTypes
    {
        private List<GhostEvidence> evidenceList = new List<GhostEvidence>();
        private List<GhostEvidence> struckOutEvidenceList = new List<GhostEvidence>();
        private static Dictionary<Ghosts, List<GhostEvidence>> evidenceMap = new Dictionary<Ghosts, List<GhostEvidence>>
        {
            { 0, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.SPIRIT_BOX, GhostEvidence.GHOST_WRITING} },
            { (Ghosts)1, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.SPIRIT_BOX, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)2, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.ULTRAVIOLET, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)3, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.ULTRAVIOLET, GhostEvidence.GHOST_WRITING} },
            { (Ghosts)4, new List<GhostEvidence> { GhostEvidence.ULTRAVIOLET, GhostEvidence.GHOST_ORB, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)5, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.ULTRAVIOLET, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)6, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.GHOST_ORB, GhostEvidence.GHOST_WRITING} },
            { (Ghosts)7, new List<GhostEvidence> { GhostEvidence.GHOST_ORB, GhostEvidence.GHOST_WRITING, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)8, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.GHOST_WRITING, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)9, new List<GhostEvidence> { GhostEvidence.ULTRAVIOLET, GhostEvidence.GHOST_WRITING, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)10, new List<GhostEvidence> { GhostEvidence.GHOST_ORB, GhostEvidence.FREEZING_TEMPERATURES, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)11, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.FREEZING_TEMPERATURES, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)12, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.GHOST_ORB, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)13, new List<GhostEvidence> { GhostEvidence.ULTRAVIOLET, GhostEvidence.GHOST_ORB, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)14, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.ULTRAVIOLET, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)15, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.ULTRAVIOLET, GhostEvidence.GHOST_WRITING} },
            { (Ghosts)16, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.GHOST_ORB, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)17, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.SPIRIT_BOX, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)18, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.GHOST_ORB, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)19, new List<GhostEvidence> { GhostEvidence.EMF_LEVEL_5, GhostEvidence.ULTRAVIOLET, GhostEvidence.GHOST_ORB} },
            { (Ghosts)20, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.ULTRAVIOLET, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)21, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.GHOST_WRITING, GhostEvidence.FREEZING_TEMPERATURES} },
            { (Ghosts)22, new List<GhostEvidence> { GhostEvidence.SPIRIT_BOX, GhostEvidence.GHOST_WRITING, GhostEvidence.DOTS_PROJECTOR} },
            { (Ghosts)23, new List<GhostEvidence> { GhostEvidence.GHOST_ORB, GhostEvidence.GHOST_WRITING, GhostEvidence.DOTS_PROJECTOR} },
        };


        public Dictionary<Ghosts, List<GhostEvidence>> EvidenceMap { get => evidenceMap; set => evidenceMap = value; }
        internal List<GhostEvidence> EvidenceList { get => evidenceList; set => evidenceList = value; }
        public List<GhostEvidence> StruckOutEvidenceList { get => struckOutEvidenceList; set => struckOutEvidenceList = value; }

        public GhostTypes()
        {
        }


        public List<Ghosts> GetSortedPossibleGhosts(bool reverse = false)
        {
            List<Ghosts> validGhosts = new List<Ghosts>();

            foreach (var pair in EvidenceMap)
            {

                if (struckOutEvidenceList.Any(pair.Value.Contains))
                {
                    continue;
                }

                var foundCount = evidenceList.Count(pair.Value.Contains);

                if (reverse)
                {
                    if (foundCount != evidenceList.Count)
                    {
                        validGhosts.Add(pair.Key);
                    }
                }
                else
                {
                    if (foundCount == evidenceList.Count)
                    {
                        validGhosts.Add(pair.Key);
                    }
                }
            }

            return validGhosts;
        }


        public List<GhostEvidence> GetSortedEvidence()
        {
            List<GhostEvidence> validEvidence = new List<GhostEvidence>();

            foreach (var pair in EvidenceMap)
            {
                if (GetSortedPossibleGhosts().Contains(pair.Key))
                {
                    foreach (var evidence in pair.Value)
                    {
                        if (!validEvidence.Contains(evidence))
                        {
                            validEvidence.Add(evidence);
                        }
                    }
                }
            }

            return validEvidence;
        }
    }
}