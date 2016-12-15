using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing.Text;
using System.IO;

namespace tryhard
{
    public partial class MainForm : Form
    {
        public System.Drawing.Point DrawingPanelOffset;

        public int  SelectedBlockIndex;
        public bool isHaveSelectedBlock = false;
        private const int DefaultMargin = 10;
        public bool isOilFieldAdd = false;

        public Dictionary<int, SchemeBlock> Blocks = new Dictionary<int, SchemeBlock>();
        public List<SchemeLink> Links = new List<SchemeLink>();
        private List<string> ItemsIdList = new List<string>();
        private int block_counter = 0;

        public MainForm()
        {
            Meta = new CMeta("../Databases/database.db");
            InitializeComponent();
            FillEquipmentCB();
            DrawingPanelOffset = DrawingPanel.Location;
        }

        public void AddSchemeLink(SchemeLink ANewLink)
        {
            Links.Add(ANewLink);
            DrawingPanel.Invalidate();
        }

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            Point Pos = new Point(DefaultMargin, 90 * (Blocks.Count) + DefaultMargin);
            if (!isOilFieldAdd)
            {
                isOilFieldAdd = (string)EquipmentCB.SelectedItem == "Месторождение";
            }
            Blocks.Add(block_counter, new SchemeBlock(Blocks.Count, 
                       CMeta.DictionaryName[(string)EquipmentCB.SelectedItem],
                       ItemsIdList[ModelCB.SelectedIndex], Pos, this));
            foreach (int Key in Blocks.Keys)
            {
                Blocks[Key].ClearFocus();
            }
            Blocks[block_counter].SetFocus();
            block_counter++;
            Console.WriteLine(block_counter);
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (Links.Count != 0)
            {
                foreach(SchemeLink Link in Links)
                {
                    Link.Draw(this, e);
                }
            }
        }

        public bool CheckLink(int AFirstBlockIndex, int ASecondBlockIndex)
        {
            foreach(SchemeLink Link in Links)
            {
                return (Link.FirstBlockIndex == AFirstBlockIndex) && (Link.SecondBlockIndex == ASecondBlockIndex);
            }
            return false;
        }

        private int FoundLinkIndex(int ASecondBlockIndex)
        {
            for (int i = 0; i < Links.Count; i++)
            {
                if (Links[i].SecondBlockIndex == ASecondBlockIndex)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindLastLink()
        {
            int LastElement = 0;
            int[] CountLinksToBlock = new int[Links.Count + 1];
            for (int i = 0; i < Links.Count; i++)
            {
                CountLinksToBlock[Links[i].FirstBlockIndex]++;
            }
            for (int i = 0; i < CountLinksToBlock.Length; i++)
            {
                if (CountLinksToBlock[i] == 0)
                {
                    return i;
                }
            }
            return LastElement;
        }

        /* Equipment ComboBoxes */

        private void EquipmentCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ModelCB.Items.Clear();
            FillModelCB(CMeta.DictionaryName[(string)EquipmentCB.SelectedItem]);
        }

        private void ModelCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillParametersGrid(CMeta.DictionaryName[(string)EquipmentCB.SelectedItem], ItemsIdList[ModelCB.SelectedIndex]);
        }

        public void SetComboBoxes(string AEquipmentName, string AModelName)
        {
            EquipmentCB.SelectedIndex = Meta.TablesList.IndexOf(AEquipmentName);
            ModelCB.SelectedIndex = ItemsIdList.IndexOf(AModelName);
        }

        private void FillEquipmentCB()
        {
            foreach (string Equipment in Meta.TablesList)
            {
                EquipmentCB.Items.Add(CMeta.DictionaryName[Equipment]);
            }
            EquipmentCB.SelectedIndex = 0;
            EquipmentCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void FillModelCB(string AEquipmentName)
        {
            ModelCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            List<string> items = Meta.GetListRecordsWithId(AEquipmentName, "name");
            ItemsIdList.Clear();
            for (int i = 0; i < items.Count; i += 2)
            {
                ItemsIdList.Add(items[i]);
                ModelCB.Items.Add(items[i + 1]);
            }
            ModelCB.SelectedIndex = 0;
            ModelCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void FillParametersGrid(string ATableName, string AFieldId)
        {
            DataGridView.Rows.Clear();
            List<string> FieldData = Meta.GetFieldData(ATableName, AFieldId);
            List<string> NameCols = Meta.GetListFieldOfTableName(ATableName);
            for (int i = 0; i < FieldData.Count(); i++)
            {
                DataGridView.Rows.Add(CMeta.DictionaryName[NameCols[i]], FieldData[i]);
            }
        }

        private void DrawingPanel_Click(object sender, EventArgs e)
        {
            foreach(int key in Blocks.Keys)
            {
                Blocks[key].ClearFocus();
            }
        }

        public Dictionary<int, CalcBlock> GetCalculatedBlocks()
        {
            /*Fill Dict */
            Dictionary<int, CalcBlock> CalcBlocks = new Dictionary<int, CalcBlock>();
            foreach (int Key in Blocks.Keys)
            {
                CalcBlocks.Add(Key, new CalcBlock(Key, Blocks[Key].BlockClass, Blocks[Key].BlockId));
            }
            /* Fill Links at blocks */
            foreach (SchemeLink Link in Links)
            {
                CalcBlocks[Link.FirstBlockIndex].OutputLinks.Add(Link.SecondBlockIndex);
                CalcBlocks[Link.SecondBlockIndex].InputLinks.Add(Link.FirstBlockIndex);
            }
            /* Calculating count */
            bool isAllBlocksCalculated = false;
            while (!isAllBlocksCalculated)
            {
                isAllBlocksCalculated = true;
                foreach (int Key in CalcBlocks.Keys)
                {
                    if ((CalcBlocks[Key].InputLinks.Count != 0) || (CalcBlocks[Key].OutputLinks.Count != 0))
                    {
                        if (CalcBlocks[Key].BlockClass == "field_parameters")
                        {
                            int link_key = CalcBlocks[Key].OutputLinks[0];
                            int field_amount_holes = Meta.GetIntValueOfParameter(CalcBlocks[Key].BlockClass, CalcBlocks[Key].BlockId, "amount_holes");
                            int dk_amount_holes = Meta.GetIntValueOfParameter(CalcBlocks[link_key].BlockClass, CalcBlocks[link_key].BlockId, "amount_holes");
                            int field_fluid = Meta.GetIntValueOfParameter(CalcBlocks[Key].BlockClass, CalcBlocks[Key].BlockId, "fluid_output");
                            int dk_fluid = Meta.GetIntValueOfParameter(CalcBlocks[link_key].BlockClass, CalcBlocks[link_key].BlockId, "fluid_input");
                            int dk_count = dk_amount_holes * CalcBlocks[link_key].Count;
                            int field_count = field_amount_holes * CalcBlocks[Key].Count;
                            if (dk_count < field_count)
                            {
                                CalcBlocks[link_key].Count = field_count / dk_count;
                                if (field_count % dk_count != 0)
                                {
                                    CalcBlocks[link_key].Count += 1;
                                }
                                CalcBlocks[link_key].isDone = false;
                            }
                            CalcBlocks[link_key].isDone = true;
                        }
                        else
                        {
                            foreach (int link_key in CalcBlocks[Key].OutputLinks)
                            {
                                string common_parametr = Meta.GetCommonParameterForLink(CalcBlocks[Key].BlockClass, CalcBlocks[link_key].BlockClass);
                                int first_block_output = Meta.GetIntValueOfParameter(CalcBlocks[Key].BlockClass, CalcBlocks[Key].BlockId, common_parametr + "_output");
                                int second_block_input = Meta.GetIntValueOfParameter(CalcBlocks[link_key].BlockClass, CalcBlocks[link_key].BlockId, common_parametr + "_input");
                                int first_block_count = first_block_output * CalcBlocks[Key].Count;
                                int second_block_count = second_block_input* CalcBlocks[link_key].Count;
                                if (second_block_count < first_block_count)
                                {
                                    CalcBlocks[link_key].Count = first_block_count / second_block_count;
                                    if (first_block_count % second_block_count != 0)
                                    {
                                        CalcBlocks[link_key].Count += 1;
                                    }
                                    CalcBlocks[link_key].isDone = false;
                                }
                                //Console.WriteLine(common_parametr + " " + value_input + " " + value_output);
                            }
                        }
                    }
                }
            }

            return CalcBlocks;
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            if (!isOilFieldAdd)
            {
                string message = "Кажется вы забыли добавить месторождение";
                string caption = "Ошибка в составлении схемы";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                return;
            }
            Dictionary<int, CalcBlock> CalculatedBlocks = GetCalculatedBlocks();
            ResultForm ResForm = new ResultForm(this, CalculatedBlocks);
            ResForm.ShowDialog();
        }

        private void DeleteBlockButton_Click(object sender, EventArgs e)
        {
            //this
            for (int i = 0; i < Links.Count; i++)
            {
                if (Links[i].CheckDeletedLink(SelectedBlockIndex))
                {
                    Links.RemoveAt(i);
                    i--;
                }
            }

            DrawingPanel.Controls.Remove(Blocks[SelectedBlockIndex].BlockBody);
            Blocks.Remove(SelectedBlockIndex);
            isHaveSelectedBlock = false;
            SelectedBlockIndex = -1;

            DrawingPanel_Click(sender, e);
            DrawingPanel.Invalidate();
        }
    }
}
