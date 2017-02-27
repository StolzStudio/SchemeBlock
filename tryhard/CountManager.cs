using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    class CountManager
    {
        public bool isEquipment;
        private bool isComeToStashAgain;
        private string CategoryType;
        private string ObjectType;
        private OilQuality FluidParam;
        List<int> Queue;
        Dictionary<int, Dictionary<string, float>> BlockStashValue;
        private List<BaseObject> Combination;
        private new List<List<int>> BlocksIndexCombination;

        public Dictionary<int, Block> Blocks;
        public List<Link> Links;

        public CountManager(ref Dictionary<int, Block> aBlocks, ref List<Link> aLinks, string aCategoryType, string aObjectType)
        {
            if (aCategoryType == "Equipment") { isEquipment = true;  }
                                         else { isEquipment = false; }
            CategoryType = aCategoryType;
            ObjectType = aObjectType;
            Blocks = aBlocks;
            Links  = aLinks;

            isComeToStashAgain = false;

            CreateQueue();
            List<Dictionary<int, Block>> a = CalculateBlocksCombinations(Blocks);
        }

        public void SetFluidParam(string aFieldName)
        {
            int OilQualityId = (MetaDataManager.Instance.GetBaseObjectOfId("field_parameters", GetObjectId("field_parameters", aFieldName)) as FieldParameters).OilQualityId;
            FluidParam = (MetaDataManager.Instance.GetBaseObjectOfId("oil_quality", OilQualityId) as OilQuality);
        }
        //
        //make combinations
        //
        public List<Dictionary<int, Block>> CalculateBlocksCombinations(Dictionary<int, Block> BaseBlocks)
        {
            List<Dictionary<int, Block>> Combinations = GetAllCombinations(BaseBlocks);
             return Combinations;
        }

        private List<Dictionary<int, Block>> GetAllCombinations(Dictionary<int, Block> ABaseBlocks)
        {
            Dictionary<string, List<string>> AllFieldsId = new Dictionary<string, List<string>>();
            List<int> BlockKeys = new List<int>();
            int FirstKey = -1;
            int CombinationsCount = 1;
            foreach (var Key in ABaseBlocks.Keys)
            {
                BlockKeys.Add(Key);
                if (!AllFieldsId.ContainsKey(ABaseBlocks[Key].ClassText))
                {
                    List<int> IdList = MetaDataManager.Instance.GetIdCortageByType(ABaseBlocks[Key].ClassText);
                    List<string> ConvertIdList = new List<string>();
                    foreach (var c in IdList)
                    {
                        ConvertIdList.Add(Convert.ToString(c));
                    }
                    AllFieldsId.Add(ABaseBlocks[Key].ClassText, ConvertIdList);
                } 
                CombinationsCount *= AllFieldsId[ABaseBlocks[Key].ClassText].Count;
            }

            List<Dictionary<int, Block>> Combinations = new List<Dictionary<int, Block>>();
            int BlockKeysIdx = 0;
            foreach (string FieldId in AllFieldsId[ABaseBlocks[BlockKeys[BlockKeysIdx]].ClassText])
            {
                List<string> FieldIdSequence = new List<string>() { FieldId };
                FillCombination(FieldIdSequence, BlockKeysIdx + 1, ref BlockKeys, ref ABaseBlocks, ref AllFieldsId, ref Combinations);
            }
            return Combinations;
        }

        private void FillCombination(List<string> AFieldIdSequense,
                                     int ABlockKeysIdx,
                                     ref List<int> ABlockKeys,
                                     ref Dictionary<int, Block> ABaseBlocks,
                                     ref Dictionary<string, List<string>> AAllFieldsId,
                                     ref List<Dictionary<int, Block>> ACombinations)
        {
            if (ABlockKeysIdx < ABlockKeys.Count)
            {
                foreach (string FieldId in AAllFieldsId[ABaseBlocks[ABlockKeys[ABlockKeysIdx]].ClassText])
                {
                    List<string> FieldIdSequence = new List<string>(AFieldIdSequense);
                    FieldIdSequence.Add(FieldId);
                    FillCombination(FieldIdSequence, ABlockKeysIdx + 1, ref ABlockKeys, ref ABaseBlocks, ref AAllFieldsId, ref ACombinations);
                }
            }
            else
            {
                ACombinations.Add(TranslateSeqInBlocksCombination(AFieldIdSequense, ref ABlockKeys, ref ABaseBlocks, ref AAllFieldsId, ref ACombinations));
            }
        }

        private Dictionary<int, Block> TranslateSeqInBlocksCombination(List<string> AFieldIdSequense,
                                                                       ref List<int> ABlockKeys,
                                                                       ref Dictionary<int, Block> ABaseBlocks,
                                                                       ref Dictionary<string, List<string>> AAllFieldsId,
                                                                       ref List<Dictionary<int, Block>> ACombinations)
        {
            Dictionary<int, Block> Combination = new Dictionary<int, Block>();
            for (int i = 0; i < ABlockKeys.Count; i++)
            {
                Combination.Add(ABlockKeys[i], new Block(ABaseBlocks[ABlockKeys[i]]));
                Combination[ABlockKeys[i]].Id = Convert.ToInt32(AFieldIdSequense[i]);
            }
            return Combination;
        }
        //
        //
        //
        private int GetObjectId(string aClass, string aModel)
        {
            List<int> Ids = MetaDataManager.Instance.GetIdCortageByType(aClass);

            foreach (var i in Ids)
            {
                if (MetaDataManager.Instance.GetBaseObjectOfId(aClass, i).Name == aModel)
                {
                    return i;
                }
            }
            return -1;
        }
        //
        //check parameters
        //
        private bool TryFoundMainObjectInLinks(string aType)
        {
            foreach (var key in Blocks.Keys)
            {
                if ((Blocks[key].ClassText == aType) || (Blocks[key].ClassText == aType))
                {
                    return true;
                }
            }
            return false;
        }

        public string CheckCombination()
        {
            if (ObjectType == "mining_complex")
            {
                if (!TryFoundMainObjectInLinks("dk"))
                {
                    return "Вы забыли установить ДК";
                }
            }
            else
            {
                if (!TryFoundMainObjectInLinks("upn"))
                {
                    return "Вы забыли установить УПН";
                }
            }
            return "ok";
        }
        //
        //calc functions
        //
        private void CreateQueue()
        {
            Queue = new List<int>();
            List<int> Indeces = new List<int>();
            int k = 0;

            if (TryFoundMainObjectInLinks("dk"))
            {
                Indeces = GiveIndexOfLinksThatKeepStartBlock("dk");
            }
            else if (TryFoundMainObjectInLinks("upn"))
            {
                Indeces = GiveIndexOfLinksThatKeepStartBlock("upn");
            }
            foreach (var ind in Indeces)
            {
                Queue.Add(ind);
            }
            

            while (k < Links.Count)
            {
                Indeces = GiveIndexOfLinkThatHasArgumentLikeFirstBlockIndex(Links[Queue[k]].SecondBlockIndex);
                if (Indeces.Count != 0)
                {
                    foreach (var ind in Indeces)
                    {
                        Queue.Add(ind);
                    }
                }
                k++;
            }
        }

        public Complex MakeCalculate(Dictionary<int, Block> aCombination, string aFieldName)
        {
            Complex Result;
            Type ResultType = Type.GetType("tryhard." + MetaDataManager.Instance.GiveTypeName(ObjectType));
            if (ObjectType == "mining_complex")
            {
                Result = new MiningComplex();
            }
            else if (ObjectType == "integrated_complex")
            {
                Result = new IntegratedComplex();
            }
            else
            {
                Result = new ProcessingComplex();
            }

            Convert.ChangeType(Result, ResultType);

            Result.Id = -1;
            Result.EstimatedFieldId = GetObjectId("field_parameters", aFieldName);

            if (Links.Count != 0)
            {
                BlockStashValue = new Dictionary<int, Dictionary<string, float>>();

                BaseObject FieldObject = MetaDataManager.Instance.GetBaseObjectOfId("field_parameters", Result.EstimatedFieldId);

                BaseObject MainObject;
                if ((ObjectType == "mining_complex")||(ObjectType == "integrated_complex"))
                {
                    MainObject = MetaDataManager.Instance.GetBaseObjectOfId("dk", aCombination[Links[Queue[0]].FirstBlockIndex].Id);
                    aCombination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfDkObject(aCombination, FieldObject, MainObject, Result, ResultType);
                }
                else
                {
                    MainObject = MetaDataManager.Instance.GetBaseObjectOfId("upn", aCombination[Links[Queue[0]].FirstBlockIndex].Id);
                    aCombination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfUpnObject(aCombination, FieldObject, MainObject, Result);
                }

                foreach (var q in Queue)
                {
                    BaseObject FirstObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[Links[q].FirstBlockIndex].ClassText, aCombination[Links[q].FirstBlockIndex].Id);
                    BaseObject SecondObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[Links[q].SecondBlockIndex].ClassText, aCombination[Links[q].SecondBlockIndex].Id);


                    int count = GiveCountOfObject(aCombination, FirstObject, SecondObject, Links[q], Result, ResultType);
                    if (!isComeToStashAgain)
                    {
                        aCombination[Links[q].SecondBlockIndex].Count = count;
                    }
                    else
                    {
                        int CheckCount = aCombination[Links[q].SecondBlockIndex].Count + count;
                        float ObjectValue = (float)SecondObject.GetType().GetProperty(Links[q].LinkParameter + "Input").GetValue(SecondObject);
                        float StashValue = BlockStashValue[Links[q].SecondBlockIndex][Links[q].LinkParameter];
                    }
                }

                if (Result.GetType().GetProperty("FluidOutput") != null)
                {
                    if (ResultType.Name == "MiningComplex")
                    {
                        if ((Result as MiningComplex).FluidOutput == 0)
                        {
                            (Result as MiningComplex).FluidOutput = BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"];
                        }
                    }
                    else
                    {
                        if ((Result as IntegratedComplex).FluidOutput == 0)
                        {
                            (Result as IntegratedComplex).FluidOutput = BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"];
                        }
                    }
                }
                if (Result.GetType().GetProperty("FluidInput") != null)
                {
                    if (ResultType.Name == "IntegratedComplex")
                    {
                        if ((Result as IntegratedComplex).FluidInput == 0)
                        {
                            if (Result.GetType().GetProperty("FluidOutput") != null)
                            {
                                (Result as IntegratedComplex).FluidInput = (Result as IntegratedComplex).FluidOutput;
                            }
                            else
                            {
                                (Result as IntegratedComplex).FluidInput = (float)FieldObject.GetType().GetProperty("FluidOutput").GetValue(FieldObject);
                            }
                        }
                    }
                    else
                    {
                        if ((Result as ProcessingComplex).FluidInput == 0)
                        {
                            (Result as ProcessingComplex).FluidInput = (float)FieldObject.GetType().GetProperty("FluidOutput").GetValue(FieldObject);
                        }
                    }
                }
            }
            
            foreach (var key in aCombination.Keys)
            {
                BaseObject BlockObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[key].ClassText, aCombination[key].Id);
                
                Result.Cost   += (float)BlockObject.GetType().GetProperty("Cost").GetValue(BlockObject) * aCombination[key].Count;
                Result.Volume += (float)BlockObject.GetType().GetProperty("Volume").GetValue(BlockObject) * aCombination[key].Count;
                Result.Weight += (float)BlockObject.GetType().GetProperty("Weight").GetValue(BlockObject) * aCombination[key].Count;
                Result.PeopleDemand += (float)BlockObject.GetType().GetProperty("PeopleDemand").GetValue(BlockObject) * aCombination[key].Count;
                Result.ElectricityDemand += (float)BlockObject.GetType().GetProperty("ElectricityDemand").GetValue(BlockObject) * aCombination[key].Count;
            }
            return Result;
        }

        private void FillBlockStash(BaseObject aBlockObject, Block aBlock, int aCount)
        {
            List<string> BlockParameters = MetaDataManager.Instance.GetParametersByParamenterType("Equipment", aBlock.ClassText, "Output");
            Dictionary<string, float> Parameters = new Dictionary<string, float>();

            foreach (var b in BlockParameters)
            {
                {
                    Parameters.Add(b, (float)aBlockObject.GetType().GetProperty(b + "Output").GetValue(aBlockObject) * aCount);
                }
            }

            if (!BlockStashValue.Keys.Contains(aBlock.Index))
            {
                BlockStashValue.Add(aBlock.Index, Parameters);
            }
            else
            {
                isComeToStashAgain = true;  
            }   
        }

        private int GiveCountOfUpnObject(Dictionary<int, Block> aCombination, BaseObject aFieldObject, BaseObject aUpnObject, Complex aResult)
        {
            float FieldObjectValue = (float)aFieldObject.GetType().GetProperty("FluidOutput").GetValue(aFieldObject);
            float UpnObjectValue = (float)aUpnObject.GetType().GetProperty("FluidInput").GetValue(aUpnObject);

            double Result = (double)FieldObjectValue / (double)UpnObjectValue;
            if ((Result > (int)Result)) { Result++; }

            FillBlockStash(aUpnObject, aCombination[Links[Queue[0]].FirstBlockIndex], (int)Result);

            if (FieldObjectValue <= UpnObjectValue * Result)
            {
                BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Oil"] = FieldObjectValue * (float)FluidParam.GetType().GetProperty("OilProportion").GetValue(FluidParam);
                BlockStashValue[Links[Queue[0]].FirstBlockIndex]["WetGas"] = FieldObjectValue * (float)FluidParam.GetType().GetProperty("WetGasProportion").GetValue(FluidParam);
                BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Water"] = FieldObjectValue * (float)FluidParam.GetType().GetProperty("WaterProportion").GetValue(FluidParam);
            }

            (aResult as ProcessingComplex).FluidInput = FieldObjectValue;
            return (int)Result;
        }

        private int GiveCountOfDkObject(Dictionary<int, Block> aCombination, BaseObject aFieldObject, BaseObject aDkObject, Complex aResult, Type aResultType)
        {
            int FieldHoles = (int)aFieldObject.GetType().GetProperty("HolesAmount").GetValue(aFieldObject);
            int DkHoles = (int)aDkObject.GetType().GetProperty("HolesAmount").GetValue(aDkObject);

            double Result = (double)FieldHoles / (double)DkHoles;
            if ((Result > (int)Result)) { Result++; }

            float FieldObjectValue = (float)aFieldObject.GetType().GetProperty("FluidOutput").GetValue(aFieldObject);
            float DkObjectValue = (float)aDkObject.GetType().GetProperty("FluidInput").GetValue(aDkObject);

            FillBlockStash(aDkObject, aCombination[Links[Queue[0]].FirstBlockIndex], 1);

            if (FieldHoles <= DkHoles)
            {
                if (FieldObjectValue < DkObjectValue)
                {
                   BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"] = FieldObjectValue;
                }
                else
                {
                    BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"] = DkObjectValue;
                }
            }
            else
            {
                DkObjectValue *= (int)Result;
                if (FieldObjectValue < DkObjectValue)
                {
                    BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"] = FieldObjectValue;
                }
                else
                {
                    BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"] = DkObjectValue;
                }
            }

            if (aResultType.Name == "MiningComplex")
            {
                (aResult as MiningComplex).FluidOutput = BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"];
            }
            else
            {
                (aResult as IntegratedComplex).FluidOutput = BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"];
            }

            return (int)Result; 
        }

        private int GiveCountOfObject(Dictionary<int, Block> aCombination, BaseObject aFirstObject, BaseObject aSecondObject, Link aLink, Complex aResult, Type aResultType)
        {
            float FirstObjectValue = BlockStashValue[aLink.FirstBlockIndex][aLink.LinkParameter];
            if (!BlockStashValue.ContainsKey(aLink.FirstBlockIndex))
            {
                FirstObjectValue = (float)aFirstObject.GetType().GetProperty(aLink.LinkParameter + "Output").GetValue(aFirstObject);
            }
            else
            {
                FirstObjectValue = BlockStashValue[aLink.FirstBlockIndex][aLink.LinkParameter];
            }

            float SecondObjectValue = (float)aSecondObject.GetType().GetProperty(aLink.LinkParameter + "Input").GetValue(aSecondObject);
            
            if (FirstObjectValue > aLink.LinkParameterValue)
            {
                FirstObjectValue = aLink.LinkParameterValue;
                if (BlockStashValue.ContainsKey(aLink.FirstBlockIndex))
                {
                   BlockStashValue[aLink.FirstBlockIndex][aLink.LinkParameter] -= aLink.LinkParameterValue;
                }
            }
            
            if (aLink.LinkParameter == "Fluid")
            {
                if (aResultType.Name == "IntegratedComplex")
                {
                    (aResult as IntegratedComplex).FluidInput += FirstObjectValue;
                }
            }
            if (aLink.LinkParameter == "Oil")
            {
                if (aResultType.Name == "ProcessingComplex")
                {
                    (aResult as ProcessingComplex).OilOutput += FirstObjectValue;
                }
                else
                {
                    (aResult as IntegratedComplex).OilOutput += FirstObjectValue;
                }
            }
            if (aLink.LinkParameter == "Gas")
            {
                if (aResult.Name == "ProcessingComplex")
                {
                    (aResult as ProcessingComplex).GasOutput += FirstObjectValue;
                }
                else
                {
                    (aResult as IntegratedComplex).GasOutput += FirstObjectValue;
                }
            }

            double Result = (double)FirstObjectValue / (double)SecondObjectValue;
            if (Result > (int)Result) { Result++; }

            FillBlockStash(aSecondObject, aCombination[aLink.SecondBlockIndex], (int)Result);

            float ResultValue;
            if (SecondObjectValue * (int)Result <= FirstObjectValue)
            {
                ResultValue = SecondObjectValue;
            }
            else
            {
                ResultValue = FirstObjectValue;
            }

            if(aSecondObject.GetType().Name == "Upn")
            {
                BlockStashValue[aLink.SecondBlockIndex]["Oil"] = ResultValue * (float)FluidParam.GetType().GetProperty("OilProportion").GetValue(FluidParam) * (int)Result;
                BlockStashValue[aLink.SecondBlockIndex]["WetGas"] = ResultValue * (float)FluidParam.GetType().GetProperty("WetGasProportion").GetValue(FluidParam) * (int)Result;
                BlockStashValue[aLink.SecondBlockIndex]["Water"] = ResultValue * (float)FluidParam.GetType().GetProperty("WaterProportion").GetValue(FluidParam) * (int)Result;
            }
            else
            {
                BlockStashValue[aLink.SecondBlockIndex][aLink.LinkParameter] = ResultValue;
            }

            return (int)Result;
        }

        private List<int> GiveIndexOfLinkThatHasArgumentLikeFirstBlockIndex(int aIndex)
        {
            List<int> Result = new List<int>();

            for (int i = 0; i < Links.Count; i++)
            {
                if (Links[i].FirstBlockIndex == aIndex)
                {
                    Result.Add(i);
                }
            }
            return Result;
        }

        private List<int> GiveIndexOfLinksThatKeepStartBlock(string aClass)
        {
            List<int> Result = new List<int>();
            for (int i = 0; i < Links.Count; i++)
            {
                if(Blocks[Links[i].FirstBlockIndex].ClassText == aClass)
                {
                    Result.Add(i);
                }
            }
            return Result;
        }
    }
}
