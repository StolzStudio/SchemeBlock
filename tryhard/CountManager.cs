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
        public  bool       isEquipment;

        public List<int> Queue;
        public Dictionary<int, Dictionary<string, float>> BlockStashValue;

        public List<Link> Links;
        public Dictionary<int, Block> Blocks;

        private string     ObjectType;
        private string     CategoryType;
        private OilQuality FluidParam;
        private bool       isComeToStashAgain;


        public CountManager(ref Dictionary<int, Block> blocks, ref List<Link> links, string categoryType, string objectType)
        {
            if (categoryType == "Equipment") { isEquipment = true;  }
                                         else { isEquipment = false; }
            Links        = links;
            Blocks       = blocks;
            ObjectType   = objectType;
            CategoryType = categoryType;

            isComeToStashAgain = false;
            CreateQueue();
        }

        public void SetFluidParam(string fieldName)
        {
            int OilQualityId = (MetaDataManager.Instance.GetBaseObjectOfId("field_parameters", GetObjectId("field_parameters", fieldName)) as FieldParameters).OilQualityId;
            FluidParam = (MetaDataManager.Instance.GetBaseObjectOfId("oil_quality", OilQualityId) as OilQuality);
        }
        //
        //make combinations
        //
        public List<Dictionary<int, Block>> CalculateBlocksCombinations(Dictionary<int, Block> baseBlocks)
        {
            List<Dictionary<int, Block>> Combinations = GetAllCombinations(baseBlocks);
             return Combinations;
        }

        private List<Dictionary<int, Block>> GetAllCombinations(Dictionary<int, Block> baseBlocks)
        {
            Dictionary<string, List<string>> AllFieldsId = new Dictionary<string, List<string>>();
            List<int> BlockKeys = new List<int>();
            int CombinationsCount = 1;
            foreach (var Key in baseBlocks.Keys)
            {
                BlockKeys.Add(Key);
                if (!AllFieldsId.ContainsKey(baseBlocks[Key].ClassText))
                {
                    List<int> IdList = MetaDataManager.Instance.GetIdCortageByType(baseBlocks[Key].ClassText);
                    List<string> ConvertIdList = new List<string>();
                    foreach (var c in IdList)
                    {
                        ConvertIdList.Add(Convert.ToString(c));
                    }
                    AllFieldsId.Add(baseBlocks[Key].ClassText, ConvertIdList);
                } 
                CombinationsCount *= AllFieldsId[baseBlocks[Key].ClassText].Count;
            }

            List<Dictionary<int, Block>> Combinations = new List<Dictionary<int, Block>>();
            int BlockKeysIdx = 0;
            foreach (string FieldId in AllFieldsId[baseBlocks[BlockKeys[BlockKeysIdx]].ClassText])
            {
                List<string> FieldIdSequence = new List<string>() { FieldId };
                FillCombination(FieldIdSequence, BlockKeysIdx + 1, ref BlockKeys, ref baseBlocks, ref AllFieldsId, ref Combinations);
            }
            return Combinations;
        }

        private void FillCombination(List<string> fieldIdSequense,
                                     int blockKeysIdx,
                                     ref List<int> blockKeys,
                                     ref Dictionary<int, Block> baseBlocks,
                                     ref Dictionary<string, List<string>> allFieldsId,
                                     ref List<Dictionary<int, Block>> combinations)
        {
            if (blockKeysIdx < blockKeys.Count)
            {
                foreach (string FieldId in allFieldsId[baseBlocks[blockKeys[blockKeysIdx]].ClassText])
                {
                    List<string> FieldIdSequence = new List<string>(fieldIdSequense);
                    FieldIdSequence.Add(FieldId);
                    FillCombination(FieldIdSequence, blockKeysIdx + 1, ref blockKeys, ref baseBlocks, ref allFieldsId, ref combinations);
                }
            }
            else
            {
                combinations.Add(TranslateSeqInBlocksCombination(fieldIdSequense, ref blockKeys, ref baseBlocks, ref allFieldsId, ref combinations));
            }
        }

        private Dictionary<int, Block> TranslateSeqInBlocksCombination(List<string> fieldIdSequense,
                                                                       ref List<int> blockKeys,
                                                                       ref Dictionary<int, Block> baseBlocks,
                                                                       ref Dictionary<string, List<string>> allFieldsId,
                                                                       ref List<Dictionary<int, Block>> combinations)
        {
            Dictionary<int, Block> Combination = new Dictionary<int, Block>();
            for (int i = 0; i < blockKeys.Count; i++)
            {
                Combination.Add(blockKeys[i], new Block(baseBlocks[blockKeys[i]]));
                Combination[blockKeys[i]].Id = Convert.ToInt32(fieldIdSequense[i]);
            }
            return Combination;
        }
        //
        //
        //
        private int GetObjectId(string _class, string _model)
        {
            List<int> Ids = MetaDataManager.Instance.GetIdCortageByType(_class);

            foreach (var i in Ids)
            {
                if (MetaDataManager.Instance.GetBaseObjectOfId(_class, i).Name == _model)
                {
                    return i;
                }
            }
            return -1;
        }
        //
        //check parameters
        //
        private bool TryFoundMainObjectInLinks(string type)
        {
            foreach (var key in Blocks.Keys)
            {
                if ((Blocks[key].ClassText == type) || (Blocks[key].ClassText == type))
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

        public Complex MakeCalculate(Dictionary<int, Block> combination, string fieldName)
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
            Result.EstimatedFieldId = GetObjectId("field_parameters", fieldName);

            if (Links.Count != 0)
            {
                BlockStashValue = new Dictionary<int, Dictionary<string, float>>();

                BaseObject FieldObject = MetaDataManager.Instance.GetBaseObjectOfId("field_parameters", Result.EstimatedFieldId);

                BaseObject MainObject;
                if ((ObjectType == "mining_complex")||(ObjectType == "integrated_complex"))
                {
                    MainObject = MetaDataManager.Instance.GetBaseObjectOfId("dk", combination[Links[Queue[0]].FirstBlockIndex].Id);
                    combination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfDkObject(combination, FieldObject, MainObject, Result, ResultType);
                }
                else
                {
                    MainObject = MetaDataManager.Instance.GetBaseObjectOfId("upn", combination[Links[Queue[0]].FirstBlockIndex].Id);
                    combination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfUpnObject(combination, FieldObject, MainObject, Result);
                }

                foreach (var q in Queue)
                {
                    BaseObject FirstObject = MetaDataManager.Instance.GetBaseObjectOfId(combination[Links[q].FirstBlockIndex].ClassText, combination[Links[q].FirstBlockIndex].Id);
                    BaseObject SecondObject = MetaDataManager.Instance.GetBaseObjectOfId(combination[Links[q].SecondBlockIndex].ClassText, combination[Links[q].SecondBlockIndex].Id);


                    int count = GiveCountOfObject(combination, FirstObject, SecondObject, Links[q], Result, ResultType);
                    if (!isComeToStashAgain)
                    {
                        combination[Links[q].SecondBlockIndex].Count = count;
                    }
                    else
                    {
                        int CheckCount = combination[Links[q].SecondBlockIndex].Count + count;
                        float ObjectValue = (float)SecondObject.GetType().GetProperty(Links[q].LinkParameter + "Input").GetValue(SecondObject);
                        float StashValue = BlockStashValue[Links[q].SecondBlockIndex][Links[q].LinkParameter];

                        if ((ObjectValue * CheckCount - StashValue) > ObjectValue)
                        {
                            CheckCount--;
                        }
                        combination[Links[q].SecondBlockIndex].Count = CheckCount;
                        isComeToStashAgain = false;
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
            
            foreach (var key in combination.Keys)
            {
                BaseObject BlockObject = MetaDataManager.Instance.GetBaseObjectOfId(combination[key].ClassText, combination[key].Id);
                
                Result.Cost   += (float)BlockObject.GetType().GetProperty("Cost").GetValue(BlockObject) * combination[key].Count * 2;
                Result.Volume += (float)BlockObject.GetType().GetProperty("Volume").GetValue(BlockObject) * combination[key].Count * 15;
                Result.Weight += (float)BlockObject.GetType().GetProperty("Weight").GetValue(BlockObject) * combination[key].Count * 10;
                Result.PeopleDemand += (float)BlockObject.GetType().GetProperty("PeopleDemand").GetValue(BlockObject) * combination[key].Count;
                Result.ElectricityDemand += (float)BlockObject.GetType().GetProperty("ElectricityDemand").GetValue(BlockObject) * combination[key].Count;
            }

            CalcObjectInComplex(combination, Result, Result.PeopleDemand, "Jk", "PeopleCapacity");
            CalcObjectInComplex(combination, Result, Result.ElectricityDemand, "Ek", "Power");

            return Result;
        }

        private void CalcObjectInComplex(Dictionary<int, Block> combination, Complex result, float resultResource, string _class, string propertyResource)
        {
            List<int> Indeces = new List<int>();
            foreach (var key in combination.Keys)
            {
                if (combination[key].ClassText == _class)
                {
                    Indeces.Add(key);
                }
            }

            if (Indeces.Count != 0)
            {
                float Resource = resultResource; 
                while (Resource > 0)
                {
                    foreach (var Index in Indeces)
                    {
                        BaseObject Object = MetaDataManager.Instance.GetBaseObjectOfId(combination[Index].ClassText, combination[Index].Id);

                        result.Cost += (float)Object.GetType().GetProperty("Cost").GetValue(Object);
                        result.Volume += (float)Object.GetType().GetProperty("Volume").GetValue(Object);
                        result.Weight += (float)Object.GetType().GetProperty("Weight").GetValue(Object);
                        if (Object.GetType().Name == "Jk")
                        {
                            result.ElectricityDemand += (float)Object.GetType().GetProperty("ElectricityDemand").GetValue(Object);
                        }

                        Resource -= (float)Object.GetType().GetProperty(propertyResource).GetValue(Object);
                        if (Resource <= 0) { break; }
                    }
                }
            }
        }

        private void FillBlockStash(BaseObject blockObject, Block block, int count)
        {
            List<string> BlockParameters = MetaDataManager.Instance.GetParametersByParamenterType("Equipment", block.ClassText, "Output");
            Dictionary<string, float> Parameters = new Dictionary<string, float>();

            foreach (var b in BlockParameters)
            {
                {
                    Parameters.Add(b, (float)blockObject.GetType().GetProperty(b + "Output").GetValue(blockObject) * count);
                }
            }

            if (!BlockStashValue.Keys.Contains(block.Index))
            {
                BlockStashValue.Add(block.Index, Parameters);
            }
            else
            {
                isComeToStashAgain = true;  
            }   
        }

        private int GiveCountOfUpnObject(Dictionary<int, Block> combination, BaseObject fieldObject, BaseObject upnObject, Complex result)
        {
            float FieldObjectValue = (float)fieldObject.GetType().GetProperty("FluidOutput").GetValue(fieldObject);
            float UpnObjectValue = (float)upnObject.GetType().GetProperty("FluidInput").GetValue(upnObject);

            double Result = (double)FieldObjectValue / (double)UpnObjectValue;
            if ((Result > (int)Result)) { Result++; }

            FillBlockStash(upnObject, combination[Links[Queue[0]].FirstBlockIndex], (int)Result);

            if (FieldObjectValue <= UpnObjectValue * Result)
            {
                BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Oil"] = FieldObjectValue * (float)FluidParam.GetType().GetProperty("OilProportion").GetValue(FluidParam);
                BlockStashValue[Links[Queue[0]].FirstBlockIndex]["WetGas"] = FieldObjectValue * (float)FluidParam.GetType().GetProperty("WetGasProportion").GetValue(FluidParam);
                BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Water"] = FieldObjectValue * (float)FluidParam.GetType().GetProperty("WaterProportion").GetValue(FluidParam);
            }

            (result as ProcessingComplex).FluidInput = FieldObjectValue;
            return (int)Result;
        }

        private int GiveCountOfDkObject(Dictionary<int, Block> combination, BaseObject fieldObject, BaseObject dkObject, Complex result, Type resultType)
        {
            int FieldHoles = (int)fieldObject.GetType().GetProperty("HolesAmount").GetValue(fieldObject);
            int DkHoles = (int)dkObject.GetType().GetProperty("HolesAmount").GetValue(dkObject);

            double Result = (double)FieldHoles / (double)DkHoles;
            if ((Result > (int)Result)) { Result++; }

            float FieldObjectValue = (float)fieldObject.GetType().GetProperty("FluidOutput").GetValue(fieldObject);
            float DkObjectValue = (float)dkObject.GetType().GetProperty("FluidInput").GetValue(dkObject);

            FillBlockStash(dkObject, combination[Links[Queue[0]].FirstBlockIndex], 1);

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

            if (resultType.Name == "MiningComplex")
            {
                (result as MiningComplex).FluidOutput = BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"];
            }
            else
            {
                (result as IntegratedComplex).FluidOutput = BlockStashValue[Links[Queue[0]].FirstBlockIndex]["Fluid"];
            }

            return (int)Result; 
        }

        private int GiveCountOfObject(Dictionary<int, Block> combination, BaseObject firstObject, BaseObject secondObject, Link link, Complex result, Type resultType)
        {
            float FirstObjectValue = BlockStashValue[link.FirstBlockIndex][link.LinkParameter];
            if (!BlockStashValue.ContainsKey(link.FirstBlockIndex))
            {
                FirstObjectValue = (float)firstObject.GetType().GetProperty(link.LinkParameter + "Output").GetValue(firstObject);
            }
            else
            {
                FirstObjectValue = BlockStashValue[link.FirstBlockIndex][link.LinkParameter];
            }

            float SecondObjectValue = (float)secondObject.GetType().GetProperty(link.LinkParameter + "Input").GetValue(secondObject);
            
            if (FirstObjectValue > link.LinkParameterValue)
            {
                FirstObjectValue = link.LinkParameterValue;
                if (BlockStashValue.ContainsKey(link.FirstBlockIndex))
                {
                   BlockStashValue[link.FirstBlockIndex][link.LinkParameter] -= link.LinkParameterValue;
                }
            }
            
            if (link.LinkParameter == "Fluid")
            {
                if (resultType.Name == "IntegratedComplex")
                {
                    (result as IntegratedComplex).FluidInput += FirstObjectValue;
                }
            }
            if (link.LinkParameter == "Oil")
            {
                if (resultType.Name == "ProcessingComplex")
                {
                    (result as ProcessingComplex).OilOutput += FirstObjectValue;
                }
                else
                {
                    (result as IntegratedComplex).OilOutput += FirstObjectValue;
                }
            }
            if (link.LinkParameter == "WetGas")
            {
                if (resultType.Name == "ProcessingComplex")
                {
                    (result as ProcessingComplex).WetGasInput += FirstObjectValue;
                }
            }
            if (link.LinkParameter == "Gas")
            {
                if (resultType.Name == "ProcessingComplex")
                {
                    (result as ProcessingComplex).GasOutput += FirstObjectValue;
                }
                else
                {
                    (result as IntegratedComplex).GasOutput += FirstObjectValue;
                }
            }

            double Result = (double)FirstObjectValue / (double)SecondObjectValue;
            if (Result > (int)Result) { Result++; }


            FillBlockStash(secondObject, combination[link.SecondBlockIndex], (int)Result);

            float ResultValue;
            if (SecondObjectValue * (int)Result <= FirstObjectValue)
            {
                ResultValue = SecondObjectValue;
            }
            else
            {
                ResultValue = FirstObjectValue;
            }

            if (secondObject.GetType().Name == "Upn")
            {
                if (!isComeToStashAgain)
                {
                    BlockStashValue[link.SecondBlockIndex]["Oil"] = ResultValue * (float)FluidParam.GetType().GetProperty("OilProportion").GetValue(FluidParam) * (int)Result;
                    BlockStashValue[link.SecondBlockIndex]["WetGas"] = ResultValue * (float)FluidParam.GetType().GetProperty("WetGasProportion").GetValue(FluidParam) * (int)Result;
                    BlockStashValue[link.SecondBlockIndex]["Water"] = ResultValue * (float)FluidParam.GetType().GetProperty("WaterProportion").GetValue(FluidParam) * (int)Result;
                }
                else
                {
                    BlockStashValue[link.SecondBlockIndex]["Oil"] += ResultValue * (float)FluidParam.GetType().GetProperty("OilProportion").GetValue(FluidParam) * (int)Result;
                    BlockStashValue[link.SecondBlockIndex]["WetGas"] += ResultValue * (float)FluidParam.GetType().GetProperty("WetGasProportion").GetValue(FluidParam) * (int)Result;
                    BlockStashValue[link.SecondBlockIndex]["Water"] += ResultValue * (float)FluidParam.GetType().GetProperty("WaterProportion").GetValue(FluidParam) * (int)Result;
                }
            }
            else
            {
                if (!isComeToStashAgain)
                {
                    BlockStashValue[link.SecondBlockIndex][link.LinkParameter] = ResultValue;
                }
                else
                {
                    BlockStashValue[link.SecondBlockIndex][link.LinkParameter] += ResultValue;
                }
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
