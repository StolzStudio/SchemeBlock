using System;
using System.Collections.Generic;
using System.Linq;

namespace tryhard
{
    public class LinkInfo
    {
        public int    Index         { get; set; }
        public string LinkParameter { get; set; }

        public LinkInfo(int index, string linkParameter)
        {
            Index         = index;
            LinkParameter = linkParameter;
        }
    }

    public class CalcBlock
    {
        /* Fields */

        public List<LinkInfo> InputLinks  = new List<LinkInfo>();
        public List<LinkInfo> OutputLinks = new List<LinkInfo>();

        /* Properties */

        public string LinkParameter { get; set; }
        public string BlockClass    { get; set; }
        public string BlockId       { get; set; }
        public bool   isDone        { get; set; }
        public int    Index         { get; set; }
        public int    Count         { get; set; }


        public CalcBlock(int index, string blockClass, string blockId, int count = 1)
        {
            Index      = index;
            BlockClass = blockClass;
            BlockId    = blockId;
            Count      = count;
        }

        public CalcBlock(CalcBlock other)
        {
            Index      = other.Index;
            Count      = other.Count;
            isDone     = other.isDone;
            BlockId    = other.BlockId;
            BlockClass = other.BlockClass;
            InputLinks.AddRange(other.InputLinks);
            OutputLinks.AddRange(other.OutputLinks);
        }
    }

    public class CalculationManager
    {   
        public List<Dictionary<int, CalcBlock>> CalculateBlocksCombinations(Dictionary<int, CalcBlock> baseBlocks)
        {
            List<Dictionary<int, CalcBlock>> Combinations = GetAllCombinations(baseBlocks);
            return Combinations;
        }

        private List<Dictionary<int, CalcBlock>> GetAllCombinations(Dictionary<int, CalcBlock> baseBlocks)
        {
            Dictionary<string, List<string>> AllFieldsId = new Dictionary<string, List<string>>();
            List<int> BlockKeys = new List<int>();
            int CombinationsCount = 1;
            foreach (int Key in baseBlocks.Keys)
            {
                BlockKeys.Add(Key);
                if (!AllFieldsId.ContainsKey(baseBlocks[Key].BlockClass))
                {
                    string TableName = baseBlocks[Key].BlockClass;
                    //AllFieldsId.Add(TableName, AMeta.GetTableOfName(TableName).IdList);
                }
                CombinationsCount *= AllFieldsId[baseBlocks[Key].BlockClass].Count;
            }

            List<Dictionary<int, CalcBlock>> Combinations = new List<Dictionary<int, CalcBlock>>();
            int BlockKeysIdx = 0;
            foreach (string FieldId in AllFieldsId[baseBlocks[BlockKeys[BlockKeysIdx]].BlockClass])
            {
                List<string> FieldIdSequence = new List<string>() { FieldId };
                FillCombination(FieldIdSequence, BlockKeysIdx + 1, ref BlockKeys, ref baseBlocks, ref AllFieldsId, ref Combinations);
            }
            return Combinations;
        }
        
        private void FillCombination(List<string> fieldIdSequense,
                                     int blockKeysIdx,
                                     ref List<int> blockKeys,
                                     ref Dictionary<int, CalcBlock> baseBlocks,
                                     ref Dictionary<string, List<string>> allFieldsId,
                                     ref List<Dictionary<int, CalcBlock>> combinations)
        {
            if (blockKeysIdx < blockKeys.Count)
            {
                foreach (string FieldId in allFieldsId[baseBlocks[blockKeys[blockKeysIdx]].BlockClass])
                {
                    List<string> FieldIdSequence = new List<string>(fieldIdSequense);
                    FieldIdSequence.Add(FieldId);
                    FillCombination(FieldIdSequence, blockKeysIdx + 1, ref blockKeys, ref baseBlocks, ref allFieldsId, ref combinations);
                }
            } else
            {
                combinations.Add(TranslateSeqInBlocksCombination(fieldIdSequense, ref blockKeys, ref baseBlocks, ref allFieldsId, ref combinations));
            }
        }

        private Dictionary<int, CalcBlock> TranslateSeqInBlocksCombination(List<string> fieldIdSequense,
                                                                           ref List<int> blockKeys,
                                                                           ref Dictionary<int, CalcBlock> baseBlocks,
                                                                           ref Dictionary<string, List<string>> allFieldsId,
                                                                           ref List<Dictionary<int, CalcBlock>> combinations)
        {
            Dictionary<int, CalcBlock> combination = new Dictionary<int, CalcBlock>();
            for (int i = 0; i < blockKeys.Count; i++)
            {
                combination.Add(blockKeys[i], new CalcBlock(baseBlocks[blockKeys[i]]));
                combination[blockKeys[i]].BlockId = fieldIdSequense[i];
            }
            return combination;
        }
    }
}