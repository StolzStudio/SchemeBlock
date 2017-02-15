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
        private string ComboBoxType;
        List<int> Queue;
        private EditorForm Form;
        public BaseObject SelectedField { get; set; }
        public List<BaseObject> FieldObjects { get; set; }
        Dictionary<int, Int64> BlockStashValue;
        private List<CountBlockIndeces> BlocksIndex;
        private List<BaseObject> Combination;
        private new List<List<int>> BlocksIndexCombination;

        public Dictionary<int, Block> Blocks;
        public List<Link> Links;

        public CountManager(ref Dictionary<int, Block> aBlocks, ref List<Link> aLinks, string aComboBoxType, EditorForm aForm)
        {
            isEquipment = false;

            ComboBoxType = aComboBoxType;
            Blocks = aBlocks;
            Links  = aLinks;
            Form = aForm;
            CreateQueue();
        }

        public CountManager(ref Dictionary<int, Block> aBlocks, string aComboBoxType, EditorForm aForm)
        {
            isEquipment = true;

            ComboBoxType = aComboBoxType;
            Blocks = aBlocks;
            Form = aForm;
            BlocksIndex = new List<CountBlockIndeces>();
            BlocksIndexCombination = new List<List<int>>();
            SetIndexArray();
            SetCombinations();
            FillPropertyDataGrid(Combination, Form.isEditObject, Form.CategoryStripComboBox.SelectedItem.ToString());
        }
        
        public void SetIndexArray()
        {
            foreach (var key in Blocks.Keys)
            {
                BlocksIndex.Add(new CountBlockIndeces(MetaDataManager.Instance.GetIdCortageByType(Blocks[key].ClassText),   
                                Blocks[key].ClassText));
            }
        }

        public void SetCombinations()
        {
            Combination = new List<BaseObject>();

            foreach (var el in BlocksIndex)
            {
                Combination.Add(MetaDataManager.Instance.GetBaseObjectOfId(el.ClassText, el.Indeces[0]));
            }
        }

        public void SetFieldObjects()
        {
            FieldObjects = new List<BaseObject>();
            List<int> Ids = MetaDataManager.Instance.GetIdCortageByType("field_parameters");

            foreach (var i in Ids)
            {
                FieldObjects.Add(MetaDataManager.Instance.GetBaseObjectOfId("field_parameters", i));
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
            BaseObject SaveObject = MetaDataManager.Instance.GetBaseObjectOfId(ComboBoxType, 0);

            Form.PropteryDataGridView.Rows.Clear();
            if (!aState)
            {
                MakeCalculateEquipment(Combination, SaveObject);
                foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[aType].Where(obj => obj.Name == ComboBoxType))
                    foreach (string APropertyName in AObjectInfo.Properties)
                    {
                        switch (APropertyName)
                        {
                            case "Id":
                                Form.PropteryDataGridView.Rows.Add(APropertyName, MetaDataManager.Instance.GetIdCortageByType(ComboBoxType).Count);
                                break;
                            case "Name":
                                Form.PropteryDataGridView.Rows.Add(APropertyName, "");
                                break;
                            default:
                                Form.PropteryDataGridView.Rows.Add(APropertyName, SaveObject.GetType().GetProperty(APropertyName).GetValue(SaveObject));
                                break;
                        }
                    }
            }
            else
            {
                foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[aType].Where(obj => obj.Name == ComboBoxType))
                    foreach (string APropertyName in AObjectInfo.Properties)
                    {
                        if (APropertyName == "Id")
                        {
                            Form.PropteryDataGridView.Rows.Add(APropertyName, MetaDataManager.Instance.GetIdCortageByType(ComboBoxType).Count);
                        }
                        else
                        {
                            Form.PropteryDataGridView.Rows.Add(APropertyName, SaveObject.GetType().GetProperty(APropertyName).GetValue(SaveObject));
                        }
                    }
            }
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
        //calc functions
        //
        private void CreateQueue()
        {
            Queue = new List<int>();
            List<int> Indeces = new List<int>();
            int k = 0;

            if (TryFoundDkInLinks())
            {
                Indeces = GiveIndexOfLinksThatKeepStartBlock("dk");
                foreach (var ind in Indeces)
                {
                    Queue.Add(ind);
                }
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
            BaseObject DkObject = MetaDataManager.Instance.GetBaseObjectOfId("dk", aCombination[Links[Queue[0]].FirstBlockIndex] .Id);
            aCombination[Links[Queue[0]].FirstBlockIndex].Count = GiveCountOfDkObject(FieldObject, DkObject);

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
        
                Complex.Cost   += (Int64)BlockObject.GetType().GetProperty("Cost").GetValue(BlockObject) * Blocks[key].Count;
                Complex.Volume += (Int64)BlockObject.GetType().GetProperty("Volume").GetValue(BlockObject) * Blocks[key].Count;
                Complex.Weight += (Int64)BlockObject.GetType().GetProperty("Weight").GetValue(BlockObject) * Blocks[key].Count;
            }
            return Complex;
        }

        private int GiveCountOfDkObject(BaseObject aFieldObject, BaseObject aDkObject)
        {
            Int64 FieldObjectValue = (Int64)aFieldObject.GetType().GetProperty("FluidOutput").GetValue(aFieldObject);
            return 1; 
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

        private bool TryFoundDkInLinks()
        {
            foreach (var el in Links)
            {
                if ((Blocks[el.FirstBlockIndex].ClassText == "dk")||(Blocks[el.SecondBlockIndex].ClassText == "dk"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
