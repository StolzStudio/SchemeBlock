using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    public partial class ResultForm : Form
    {
        private MainForm MForm;

        public ResultForm(MainForm AForm, Dictionary<int, CalcBlock> ACalcBlocks)
        {
            MForm = AForm;
            InitializeComponent();
            FillDataGrid(ACalcBlocks);
        }

        public void FillDataGrid(Dictionary<int, CalcBlock> ACalcBlocks)
        {
            int common_cost = 0;
            foreach (int Key in ACalcBlocks.Keys)
            {
                int cost = 0;
                if (ACalcBlocks[Key].BlockClass != "field_parameters")
                {
                    cost = MForm.Meta.GetIntValueOfParameter(ACalcBlocks[Key].BlockClass, ACalcBlocks[Key].BlockId, "cost_equipment");
                }
                string ModelName = MForm.Meta.GetStringValueOfParameter(ACalcBlocks[Key].BlockClass, ACalcBlocks[Key].BlockId, "name");
                ResultGridView.Rows.Add(CMeta.DictionaryName[ACalcBlocks[Key].BlockClass], ModelName, ACalcBlocks[Key].Count, cost * ACalcBlocks[Key].Count);
                common_cost += cost * ACalcBlocks[Key].Count;
            }
            ResultGridView.Rows.Add("", "", "Общая стоимость: ", common_cost);
        }
    }
}
