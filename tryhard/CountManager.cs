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

    class CountElement
    {
        public string ID    { get; set; }
        public string Class { get; set; }

        public CountElement(string aID, string aClass)
        {
            this.ID    = aID;
            this.Class = aClass;
        }
    }

    class CountManager
    {
        private bool isEquipment;
        private string ComboBoxType;
        private EditorForm Form;
        public BaseObject SelectedField { get; set; }
        public List<BaseObject> FieldObjects { get; set; }
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
            CreateGrapth();
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

        public List<DataGridViewColumn> GiveCombinationColumns()
        {
            List<DataGridViewColumn> Result = new List<DataGridViewColumn>();
            foreach (var key in Blocks.Keys)
            {
                DataGridViewTextBoxColumn el = new DataGridViewTextBoxColumn();
                el.HeaderText = Blocks[key].ClassText;
                el.Width = 70;
                Form.CombinationDataGridView.Columns.Add(el);
            }
            DataGridViewCheckBoxColumn FirstColumn = new DataGridViewCheckBoxColumn();
            FirstColumn.HeaderText = "Номер";
            FirstColumn.Width = 70;
            Result.Add(FirstColumn);
            return Result;
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
        public void MakeCalculateComplex(List<BaseObject> aCombination, BaseObject aSaveObject)
        {

        }

        private void CreateGrapth()
        {
            int CurElem = -1;
            int k = -1;

            if (TryFoundDkInLinks())
            {
                k = GiveIndexOfLinkThatKeepStartBlock("dk");
                CurElem = Links[k].FirstBlockIndex;
            }
            while (BlockHasOutputLink(CurElem))
            {
                k = GiveIndexOfLinkThatHasArgumentLikeFirstBlockIndex(CurElem);
                CurElem = Links[k].SecondBlockIndex;
            }
        }

        private int GiveIndexOfLinkThatHasArgumentLikeFirstBlockIndex(int aIndex)
        {
            for (int i = 0; i < Links.Count; i++)
            {
                if (Links[i].FirstBlockIndex == aIndex)
                {
                    return i;
                }
            }
            return -1;
        }

        private int GiveIndexOfLinkThatKeepStartBlock(string aClass)
        {
            for (int i = 0; i < Links.Count; i++)
            {
                if(Blocks[Links[i].FirstBlockIndex].ClassText == aClass)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool BlockHasOutputLink(int aIndex)
        {
            foreach (var el in Links)
            {
                if (el.FirstBlockIndex == aIndex)
                {
                    return true;
                }
            }
            return false;
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
