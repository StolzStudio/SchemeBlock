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
        private EditorForm Form;
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
            FillCombinationDataGrid();
            MessageBox.Show("");
        }
        
        public void SetIndexArray()
        {
            foreach (var key in Blocks.Keys)
            {
                BlocksIndex.Add(new CountBlockIndeces(MetaDataManager.Instance.GetIdCortageByType(Blocks[key].ClassText),   
                                Blocks[key].ClassText));
                //MessageBox.Show(BlocksIndex[0].Count.ToString());
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

        public void FillCombinationDataGrid()
        {
            DataGridViewCheckBoxColumn FirstColumn = new DataGridViewCheckBoxColumn();
            FirstColumn.HeaderText = "Номер";
            FirstColumn.Width = 70;
            Form.CombinationDataGridView.Columns.Add(FirstColumn);
            foreach (var key in Blocks.Keys)
            {
                DataGridViewTextBoxColumn el = new DataGridViewTextBoxColumn();
                el.HeaderText = Blocks[key].ClassText;
                el.Width = 70;
                Form.CombinationDataGridView.Columns.Add(el);
            }
        }

        public void MakeCalculate(List<int> aCombination)
        {
            foreach (var key in Blocks.Keys)
            {
                if (Blocks[key].ClassText == "pump")
                {

                }
            }
        }
    }
}
