using System;
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
        private string ObjectType;
        List<int> Queue;
        Dictionary<int, Int64> BlockStashValue;
        private List<CountBlockIndeces> BlocksIndex;
        private List<BaseObject> Combination;
        private new List<List<int>> BlocksIndexCombination;

        public Dictionary<int, Block> Blocks;
        public List<Link> Links;

        public CountManager(ref Dictionary<int, Block> aBlocks, ref List<Link> aLinks, string aCategoryType, string aObjectType)
        {
            if (aCategoryType == "Equipment") { isEquipment = true;  }
                                         else { isEquipment = false; }
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
            BlockStashValue = new Dictionary<int, Int64>();

            BaseObject FieldObject = MetaDataManager.Instance.GetBaseObjectOfId("field_parameters", GetObjectId("field_parameters", aFieldName));

            BaseObject MainObject;
            if (ObjectType == "mining_complex")
            {
                MainObject = MetaDataManager.Instance.GetBaseObjectOfId("dk", aCombination[Links[Queue[0]].FirstBlockIndex].Id);
                aCombination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfDkObject(FieldObject, MainObject);
            }
            else
            {
                MainObject = MetaDataManager.Instance.GetBaseObjectOfId("upn", aCombination[Links[Queue[0]].FirstBlockIndex].Id);
                aCombination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfUpnObject(FieldObject, MainObject);
            }
            

            foreach (var q in Queue)
            {
                BaseObject FirstObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[Links[q].FirstBlockIndex].ClassText, aCombination[Links[q].FirstBlockIndex].Id);
                BaseObject SecondObject = MetaDataManager.Instance.GetBaseObjectOfId(aCombination[Links[q].SecondBlockIndex].ClassText, aCombination[Links[q].SecondBlockIndex].Id);
                aCombination[Links[q].SecondBlockIndex].Count = GiveCountOfObject(FirstObject, SecondObject, Links[q]);
            }

            Complex Complex = new Complex();

            foreach (var key in Blocks.Keys)
            {
                BaseObject BlockObject = MetaDataManager.Instance.GetBaseObjectOfId(Blocks[key].ClassText, Blocks[key].Id);
                
                Complex.Cost   += Convert.ToInt64(BlockObject.GetType().GetProperty("Cost").GetValue(BlockObject)) * Blocks[key].Count;
                Complex.Volume += Convert.ToInt64(BlockObject.GetType().GetProperty("Volume").GetValue(BlockObject)) * Blocks[key].Count;
                Complex.Weight += Convert.ToInt64(BlockObject.GetType().GetProperty("Weight").GetValue(BlockObject)) * Blocks[key].Count;
                Complex.PeopleDemand += Convert.ToInt32(BlockObject.GetType().GetProperty("PeopleDemand").GetValue(BlockObject)) * Blocks[key].Count;
                Complex.ElectricityDemand += Convert.ToInt32(BlockObject.GetType().GetProperty("ElectricityDemand").GetValue(BlockObject)) * Blocks[key].Count;
            }
            return Complex;
        }

        private int GiveCountOfUpnObject(BaseObject aFieldObject, BaseObject aUpnObject)
        {
            Int64 FieldObjectValue = (Int64)aFieldObject.GetType().GetProperty("FluidOutput").GetValue(aFieldObject);
            Int64 UpnObjectValue = (Int64)aUpnObject.GetType().GetProperty("FluidInput").GetValue(aUpnObject);
            BlockStashValue.Add(Blocks[Links[0].FirstBlockIndex].Index, FieldObjectValue);

            double Result = (double)FieldObjectValue / (double)UpnObjectValue;
            if ((Result > (int)Result)) { Result++; }
            return (int)Result;
        }

        private int GiveCountOfDkObject(BaseObject aFieldObject, BaseObject aDkObject)
        {
            int FieldHoles = (int)aFieldObject.GetType().GetProperty("HolesAmount").GetValue(aFieldObject);
            int DkHoles = (int)aDkObject.GetType().GetProperty("HolesAmount").GetValue(aDkObject);

            double Result = (double)FieldHoles / (double)DkHoles;
            if ((Result > (int)Result)) { Result++; }

            Int64 FieldObjectValue = (Int64)aFieldObject.GetType().GetProperty("FluidOutput").GetValue(aFieldObject);
            Int64 DkObjectValue = (Int64)aDkObject.GetType().GetProperty("FluidInput").GetValue(aDkObject);

            if (FieldHoles <= DkHoles)
            {
                if (FieldObjectValue < DkObjectValue)
                {
                    BlockStashValue.Add(Blocks[Links[0].FirstBlockIndex].Index, FieldObjectValue);
                }
                else
                {
                    BlockStashValue.Add(Blocks[Links[0].FirstBlockIndex].Index, DkObjectValue);
                }
            }
            else
            {
                DkObjectValue *= (int)Result;
                if (FieldObjectValue < DkObjectValue)
                {
                    BlockStashValue.Add(Blocks[Links[0].FirstBlockIndex].Index, FieldObjectValue);
                }
                else
                {
                    BlockStashValue.Add(Blocks[Links[0].FirstBlockIndex].Index, DkObjectValue);
                }
            }
            return (int)Result; 
        }

        private int GiveCountOfObject(BaseObject aFirstObject, BaseObject aSecondObject, Link aLink)
        {
            Int64 FirstObjectValue;
            if (!BlockStashValue.ContainsKey(aLink.FirstBlockIndex))
            {
                FirstObjectValue = (Int64)aFirstObject.GetType().GetProperty(aLink.LinkParameter + "Output").GetValue(aFirstObject);
            }
            else
            {
                FirstObjectValue = BlockStashValue[aLink.FirstBlockIndex];
            }
            
            Int64 SecondObjectValue = (Int64)aSecondObject.GetType().GetProperty(aLink.LinkParameter + "Input").GetValue(aSecondObject);
            
            if (FirstObjectValue > aLink.LinkParameterValue)
            {
                FirstObjectValue = aLink.LinkParameterValue;
                if (BlockStashValue.ContainsKey(aLink.FirstBlockIndex))
                {
                    BlockStashValue[aLink.FirstBlockIndex] -= aLink.LinkParameterValue;
                }
            }

            BlockStashValue.Add(Blocks[aLink.SecondBlockIndex].Index, FirstObjectValue);

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
