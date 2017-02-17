﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    class CountBlockIndeces
    {
        public List<int> Indeces;
        public string ClassText;

        public CountBlockIndeces(List<int> aIndeces, string aClassText)
        {
            Indeces = aIndeces;
            ClassText = aClassText;
        }
    }

    class CountManager
    {
        private bool isEquipment;
        private string CategoryType;
        private string ObjectType;
        List<int> Queue;
        Dictionary<int, Dictionary<string, Int64>> BlockStashValue;
        private List<CountBlockIndeces> BlocksIndex;
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
            CreateQueue();
        }
        
        public void SetIndexArray()
        {
            foreach (var key in Blocks.Keys)
            {
                BlocksIndex.Add(new CountBlockIndeces(MetaDataManager.Instance.GetIdCortageByType(Blocks[key].ClassText),   
                                Blocks[key].ClassText));
            }
        }

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

        public void FillPropertyDataGrid(List<BaseObject> aCombination, bool aState, string aType)
        {
            //BaseObject SaveObject = MetaDataManager.Instance.GetBaseObjectOfId(ComboBoxType, 0);

            //Form.PropteryDataGridView.Rows.Clear();
            //if (!aState)
            //{
            //    MakeCalculateEquipment(Combination, SaveObject);
            //    foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[aType].Where(obj => obj.Name == ComboBoxType))
            //        foreach (string APropertyName in AObjectInfo.Properties)
            //        {
            //            switch (APropertyName)
            //            {
            //                case "Id":
            //                    Form.PropteryDataGridView.Rows.Add(APropertyName, MetaDataManager.Instance.GetIdCortageByType(ComboBoxType).Count);
            //                    break;
            //                case "Name":
            //                    Form.PropteryDataGridView.Rows.Add(APropertyName, "");
            //                    break;
            //                default:
            //                    Form.PropteryDataGridView.Rows.Add(APropertyName, SaveObject.GetType().GetProperty(APropertyName).GetValue(SaveObject));
            //                    break;
            //            }
            //        }
            //}
            //else
            //{
            //    foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[aType].Where(obj => obj.Name == ComboBoxType))
            //        foreach (string APropertyName in AObjectInfo.Properties)
            //        {
            //            if (APropertyName == "Id")
            //            {
            //                Form.PropteryDataGridView.Rows.Add(APropertyName, MetaDataManager.Instance.GetIdCortageByType(ComboBoxType).Count);
            //            }
            //            else
            //            {
            //                Form.PropteryDataGridView.Rows.Add(APropertyName, SaveObject.GetType().GetProperty(APropertyName).GetValue(SaveObject));
            //            }
            //        }
            //}
        }

        public void MakeCalculateEquipment(List<BaseObject> aCombination, BaseObject aSaveObject)
        {
            foreach (var el in aCombination)
            {
                (aSaveObject as MaterialObject).Weight += (el as MaterialObject).Weight;
                (aSaveObject as MaterialObject).Volume += (el as MaterialObject).Volume;
                (aSaveObject as MaterialObject).Cost   += (el as MaterialObject).Cost;
            }
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
            Complex Result = new Complex();
            if (Links.Count == 0)
            {
                return Result;
            }

            BlockStashValue = new Dictionary<int, Dictionary<string, Int64>>();

            BaseObject FieldObject = MetaDataManager.Instance.GetBaseObjectOfId("field_parameters", GetObjectId("field_parameters", aFieldName));

            BaseObject MainObject;
            if (ObjectType == "mining_complex")
            {
                MainObject = MetaDataManager.Instance.GetBaseObjectOfId("dk", aCombination[Links[Queue[0]].FirstBlockIndex].Id);
                aCombination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfDkObject(aCombination, FieldObject, MainObject);
            }
            else
            {
                MainObject = MetaDataManager.Instance.GetBaseObjectOfId("upn", aCombination[Links[Queue[0]].FirstBlockIndex].Id);
                aCombination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfUpnObject(aCombination, FieldObject, MainObject);
            }

            foreach (var q in Queue)
            {
                BaseObject FirstObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[Links[q].FirstBlockIndex].ClassText, aCombination[Links[q].FirstBlockIndex].Id);
                BaseObject SecondObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[Links[q].SecondBlockIndex].ClassText, aCombination[Links[q].SecondBlockIndex].Id);
                aCombination[Links[q].SecondBlockIndex].Count = GiveCountOfObject(aCombination, FirstObject, SecondObject, Links[q]);
            }

            foreach (var key in aCombination.Keys)
            {
                BaseObject BlockObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[key].ClassText, aCombination[key].Id);
                
                Result.Cost   += Convert.ToInt64(BlockObject.GetType().GetProperty("Cost").GetValue(BlockObject)) * aCombination[key].Count;
                Result.Volume += Convert.ToInt64(BlockObject.GetType().GetProperty("Volume").GetValue(BlockObject)) * aCombination[key].Count;
                Result.Weight += Convert.ToInt64(BlockObject.GetType().GetProperty("Weight").GetValue(BlockObject)) * aCombination[key].Count;
                Result.PeopleDemand += Convert.ToInt32(BlockObject.GetType().GetProperty("PeopleDemand").GetValue(BlockObject)) * aCombination[key].Count;
                Result.ElectricityDemand += Convert.ToInt32(BlockObject.GetType().GetProperty("ElectricityDemand").GetValue(BlockObject)) * aCombination[key].Count;
            }
            return Result;
        }

        private void FillBlockStash(BaseObject aBlockObject, Block aBlock)
        {
            List<string> BlockParameters = MetaDataManager.Instance.GetParametersByParamenterType("Equipment", aBlock.ClassText, "Output");
            Dictionary<string, Int64> Parameters = new Dictionary<string, Int64>();
            foreach (var b in BlockParameters)
            {
                Parameters.Add(b, (Int64)aBlockObject.GetType().GetProperty(b + "Output").GetValue(aBlockObject) * aBlock.Count);
            }
            BlockStashValue.Add(aBlock.Index, Parameters);

        }

        private int GiveCountOfUpnObject(Dictionary<int, Block> aCombination, BaseObject aFieldObject, BaseObject aUpnObject)
        {
            Int64 FieldObjectValue = (Int64)aFieldObject.GetType().GetProperty("FluidOutput").GetValue(aFieldObject);
            Int64 UpnObjectValue = (Int64)aUpnObject.GetType().GetProperty("FluidInput").GetValue(aUpnObject);

            FillBlockStash(aUpnObject, aCombination[Links[Queue[0]].FirstBlockIndex]);

            double Result = (double)FieldObjectValue / (double)UpnObjectValue;
            if ((Result > (int)Result)) { Result++; }
            return (int)Result;
        }

        private int GiveCountOfDkObject(Dictionary<int, Block> aCombination, BaseObject aFieldObject, BaseObject aDkObject)
        {
            int FieldHoles = (int)aFieldObject.GetType().GetProperty("HolesAmount").GetValue(aFieldObject);
            int DkHoles = (int)aDkObject.GetType().GetProperty("HolesAmount").GetValue(aDkObject);

            double Result = (double)FieldHoles / (double)DkHoles;
            if ((Result > (int)Result)) { Result++; }

            Int64 FieldObjectValue = (Int64)aFieldObject.GetType().GetProperty("FluidOutput").GetValue(aFieldObject);
            Int64 DkObjectValue = (Int64)aDkObject.GetType().GetProperty("FluidInput").GetValue(aDkObject);

            FillBlockStash(aDkObject, aCombination[Links[Queue[0]].FirstBlockIndex]);

            if (FieldHoles <= DkHoles)
            {
                if (FieldObjectValue < DkObjectValue)
                {
                   BlockStashValue[aCombination[Links[Queue[0]].FirstBlockIndex].Index]["Fluid"] = FieldObjectValue;
                }
                else
                {
                    BlockStashValue[aCombination[Links[Queue[0]].FirstBlockIndex].Index]["Fluid"] = DkObjectValue;
                }
            }
            else
            {
                DkObjectValue *= (int)Result;
                if (FieldObjectValue < DkObjectValue)
                {
                    BlockStashValue[aCombination[Links[Queue[0]].FirstBlockIndex].Index]["Fluid"] = FieldObjectValue;
                }
                else
                {
                    BlockStashValue[aCombination[Links[Queue[0]].FirstBlockIndex].Index]["Fluid"] = DkObjectValue;
                }
            }
            return (int)Result; 
        }

        private int GiveCountOfObject(Dictionary<int, Block> aCombination, BaseObject aFirstObject, BaseObject aSecondObject, Link aLink)
        {
            Int64 FirstObjectValue = BlockStashValue[aLink.FirstBlockIndex][aLink.LinkParameter];
            if (!BlockStashValue.ContainsKey(aLink.FirstBlockIndex))
            {
                FirstObjectValue = (Int64)aFirstObject.GetType().GetProperty(aLink.LinkParameter + "Output").GetValue(aFirstObject);
            }
            else
            {
                FirstObjectValue = BlockStashValue[aLink.FirstBlockIndex][aLink.LinkParameter];
            }

            Int64 SecondObjectValue = (Int64)aSecondObject.GetType().GetProperty(aLink.LinkParameter + "Input").GetValue(aSecondObject);
            
            if (FirstObjectValue > aLink.LinkParameterValue)
            {
                FirstObjectValue = aLink.LinkParameterValue;
                if (BlockStashValue.ContainsKey(aLink.FirstBlockIndex))
                {
                   BlockStashValue[aLink.FirstBlockIndex][aLink.LinkParameter] -= aLink.LinkParameterValue;
                }
            }

            FillBlockStash(aSecondObject, aCombination[aLink.SecondBlockIndex]);

            double Result = (double)FirstObjectValue / (double)SecondObjectValue;
            if (Result > (int)Result) { Result++; }
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
