using System;
using System.Collections.Generic;
using System.Data.SQLite;

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


        public CalcBlock(int AIndex, string ABlockClass, string ABlockId)
        {
            Index = AIndex;
            BlockClass = ABlockClass;
            BlockId= ABlockId;
            Count = 1;
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
        public List<Dictionary<int, CalcBlock>> CalculateBlocksCombinations(CMeta AMeta, 
                                                                                   Dictionary<int, CalcBlock> BaseBlocks)
        {
            List<Dictionary<int, CalcBlock>> Combinations = new List<Dictionary<int, CalcBlock>>();
            List<Dictionary<int, CalcBlock>> NotCountedCombinations = new List<Dictionary<int, CalcBlock>>();
            NotCountedCombinations = GetAllCombinations(AMeta, BaseBlocks);
            foreach (Dictionary<int, CalcBlock> Combination in NotCountedCombinations)
                Combinations.Add(CalculateBlocksCombination(AMeta, Combination));
            return Combinations;
        }

        private List<Dictionary<int, CalcBlock>> GetAllCombinations(CMeta AMeta,
                                                                           Dictionary<int, CalcBlock> BaseBlocks)
        {
            Dictionary<string, List<string>> AllFieldsId = new Dictionary<string, List<string>>();
            List<int> BlockKeys = new List<int>();
            int FirstKey = -1;
            foreach (int Key in BaseBlocks.Keys)
            {
                BlockKeys.Add(Key);
                if (!AllFieldsId.ContainsKey(BaseBlocks[Key].BlockClass))
                {
                    string TableName = BaseBlocks[Key].BlockClass;
                    AllFieldsId.Add(TableName, AMeta.GetTableOfName(TableName).IdList);
                }
            }
            List<Dictionary<int, CalcBlock>> AllCombinations = new List<Dictionary<int, CalcBlock>>();
            int BlockKeysIdx = 0;
            foreach (string FieldId in AllFieldsId[BaseBlocks[BlockKeys[BlockKeysIdx]].BlockClass])
            {
                Dictionary<int, CalcBlock> Combination = new Dictionary<int, CalcBlock>(BaseBlocks);
                Combination[BlockKeys[BlockKeysIdx]].BlockId = FieldId;
                AllCombinations.AddRange(GetCombination(Combination, BlockKeys, BlockKeysIdx + 1, AllFieldsId));
            }
            return AllCombinations;
        }
        
        private List<Dictionary<int, CalcBlock>> GetCombination(Dictionary<int, CalcBlock> ACombination,
            List<int> ABlockKeys, int ABlockKeysIdx, Dictionary<string, List<string>> AAllFieldsId)
        {
            List<Dictionary<int, CalcBlock>> Combinations = new List<Dictionary<int, CalcBlock>>();
            if (ABlockKeysIdx < ABlockKeys.Count)
            {
                foreach (string FieldId in AAllFieldsId[ACombination[ABlockKeys[ABlockKeysIdx]].BlockClass])
                {
                    Dictionary<int, CalcBlock> Combination;// = new Dictionary<int, CalcBlock>(ACombination);
                    Combination = new Dictionary<int, CalcBlock>(ACombination);
                    Combination[ABlockKeys[ABlockKeysIdx]].BlockId = FieldId;
                    Combinations.AddRange(GetCombination(Combination, ABlockKeys, ABlockKeysIdx + 1, AAllFieldsId));
                }
            } else
            {
                Combinations.Add(ACombination);
            }
            return Combinations;
        }

        private Dictionary<int, CalcBlock> CalculateBlocksCombination(CMeta AMeta,
                                                                             Dictionary<int, CalcBlock> BlocksCombination)
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
            return BlocksCombination;
        }
    }
}