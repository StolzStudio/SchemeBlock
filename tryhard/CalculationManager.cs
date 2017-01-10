using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace tryhard
{
    public class CalcBlock
    {
        /* Fields */

        public List<int> InputLinks  = new List<int>();
        public List<int> OutputLinks = new List<int>();

        /* Properties */

        public string BlockClass { get; set; }
        public string BlockId    { get; set; }
        public bool   isDone     { get; set; }
        public int    Index      { get; set; }
        public int    Count      { get; set; }


        public CalcBlock(int AIndex, string ABlockClass, string ABlockId, int ACount = 1)
        {
            Index = AIndex;
            BlockClass = ABlockClass;
            BlockId= ABlockId;
            Count = ACount;
        }

        public CalcBlock(CalcBlock AOther)
        {
            Index = AOther.Index;
            Count = AOther.Count;
            isDone = AOther.isDone;
            BlockClass = AOther.BlockClass;
            BlockId = AOther.BlockId;
            InputLinks.AddRange(AOther.InputLinks);
            OutputLinks.AddRange(AOther.OutputLinks);
        }
    }

    public class CalculationManager
    {
        public List<Dictionary<int, CalcBlock>> CalculateBlocksCombinations(CMeta AMeta, Dictionary<int, CalcBlock> BaseBlocks, PageType APageType)
        {
            List<Dictionary<int, CalcBlock>> Combinations = GetAllCombinations(AMeta, BaseBlocks);
            if (APageType == PageType.SchemeType)
                CalculateBlocksCombinations(AMeta, ref Combinations);
            return Combinations;
        }

        private List<Dictionary<int, CalcBlock>> GetAllCombinations(CMeta AMeta,
                                                                    Dictionary<int, CalcBlock> ABaseBlocks)
        {
            Dictionary<string, List<string>> AllFieldsId = new Dictionary<string, List<string>>();
            List<int> BlockKeys = new List<int>();
            int FirstKey = -1;
            int CombinationsCount = 1;
            foreach (int Key in ABaseBlocks.Keys)
            {
                BlockKeys.Add(Key);
                if (!AllFieldsId.ContainsKey(ABaseBlocks[Key].BlockClass))
                {
                    string TableName = ABaseBlocks[Key].BlockClass;
                    AllFieldsId.Add(TableName, AMeta.GetTableOfName(TableName).IdList);
                }
                CombinationsCount *= AllFieldsId[ABaseBlocks[Key].BlockClass].Count;
            }

            List<Dictionary<int, CalcBlock>> Combinations = new List<Dictionary<int, CalcBlock>>();
            int BlockKeysIdx = 0;
            foreach (string FieldId in AllFieldsId[ABaseBlocks[BlockKeys[BlockKeysIdx]].BlockClass])
            {
                List<string> FieldIdSequence = new List<string>() { FieldId };
                FillCombination(FieldIdSequence, BlockKeysIdx + 1, ref BlockKeys, ref ABaseBlocks, ref AllFieldsId, ref Combinations);
            }
            return Combinations;
        }
        
        private void FillCombination(List<string> AFieldIdSequense,
                                     int ABlockKeysIdx,
                                     ref List<int> ABlockKeys,
                                     ref Dictionary<int, CalcBlock> ABaseBlocks,
                                     ref Dictionary<string, List<string>> AAllFieldsId,
                                     ref List<Dictionary<int, CalcBlock>> ACombinations)
        {
            if (ABlockKeysIdx < ABlockKeys.Count)
            {
                foreach (string FieldId in AAllFieldsId[ABaseBlocks[ABlockKeys[ABlockKeysIdx]].BlockClass])
                {
                    List<string> FieldIdSequence = new List<string>(AFieldIdSequense);
                    FieldIdSequence.Add(FieldId);
                    FillCombination(FieldIdSequence, ABlockKeysIdx + 1, ref ABlockKeys, ref ABaseBlocks, ref AAllFieldsId, ref ACombinations);
                }
            } else
            {
                ACombinations.Add(TranslateSeqInBlocksCombination(AFieldIdSequense, ref ABlockKeys, ref ABaseBlocks, ref AAllFieldsId, ref ACombinations));
            }
        }

        private Dictionary<int, CalcBlock> TranslateSeqInBlocksCombination(List<string> AFieldIdSequense,
                                                                           ref List<int> ABlockKeys,
                                                                           ref Dictionary<int, CalcBlock> ABaseBlocks,
                                                                           ref Dictionary<string, List<string>> AAllFieldsId,
                                                                           ref List<Dictionary<int, CalcBlock>> ACombinations)
        {
            Dictionary<int, CalcBlock> Combination = new Dictionary<int, CalcBlock>();
            for (int i = 0; i < ABlockKeys.Count; i++)
            {
                Combination.Add(ABlockKeys[i], new CalcBlock(ABaseBlocks[ABlockKeys[i]]));
                Combination[ABlockKeys[i]].BlockId = AFieldIdSequense[i];
            }
            return Combination;
        }

        private void CalculateBlocksCombinations(CMeta AMeta, ref List<Dictionary<int, CalcBlock>> BlocksCombinations)
        {
            foreach (Dictionary<int, CalcBlock> BlocksCombination in BlocksCombinations)
            {
                bool isAllBlocksCalculated = false;
                while (!isAllBlocksCalculated)
                {
                    isAllBlocksCalculated = true;
                    foreach (int Key in BlocksCombination.Keys)
                    {
                        if ((BlocksCombination[Key].InputLinks.Count != 0) || (BlocksCombination[Key].OutputLinks.Count != 0))
                        {
                            if (BlocksCombination[Key].BlockClass == "field_parameters")
                            {
                                int link_key = BlocksCombination[Key].OutputLinks[0];
                                int field_amount_holes = AMeta.GetIntValueOfParameter(BlocksCombination[Key].BlockClass,
                                                                                     BlocksCombination[Key].BlockId, "amount_holes");
                                int dk_amount_holes = AMeta.GetIntValueOfParameter(BlocksCombination[link_key].BlockClass,
                                                                                  BlocksCombination[link_key].BlockId, "amount_holes");
                                int field_fluid = AMeta.GetIntValueOfParameter(BlocksCombination[Key].BlockClass,
                                                                               BlocksCombination[Key].BlockId, "fluid_output");
                                int dk_fluid = AMeta.GetIntValueOfParameter(BlocksCombination[link_key].BlockClass,
                                                                            BlocksCombination[link_key].BlockId, "fluid_input");
                                int dk_count = dk_amount_holes * BlocksCombination[link_key].Count;
                                int field_count = field_amount_holes * BlocksCombination[Key].Count;
                                if (dk_count < field_count)
                                {
                                    BlocksCombination[link_key].Count = field_count / dk_count;
                                    if (field_count % dk_count != 0)
                                    {
                                        BlocksCombination[link_key].Count += 1;
                                    }
                                    BlocksCombination[link_key].isDone = false;
                                }
                                BlocksCombination[link_key].isDone = true;
                            }
                            else
                            {
                                foreach (int link_key in BlocksCombination[Key].OutputLinks)
                                {
                                    string common_parametr = AMeta.GetCommonParameterForLink(BlocksCombination[Key].BlockClass,
                                                                                             BlocksCombination[link_key].BlockClass);
                                    int first_block_output = AMeta.GetIntValueOfParameter(BlocksCombination[Key].BlockClass,
                                                                                          BlocksCombination[Key].BlockId, common_parametr + "_output");
                                    int second_block_input = AMeta.GetIntValueOfParameter(BlocksCombination[link_key].BlockClass,
                                                                                          BlocksCombination[link_key].BlockId, common_parametr + "_input");
                                    int first_block_count = first_block_output * BlocksCombination[Key].Count;
                                    int second_block_count = second_block_input * BlocksCombination[link_key].Count;
                                    if (second_block_count < first_block_count)
                                    {
                                        BlocksCombination[link_key].Count = first_block_count / second_block_count;
                                        if (first_block_count % second_block_count != 0)
                                        {
                                            BlocksCombination[link_key].Count += 1;
                                        }
                                        BlocksCombination[link_key].isDone = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}