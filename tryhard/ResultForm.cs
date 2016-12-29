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
            int common_weight = 0;
            int common_volume = 0;
            int common_cost = 0;
            foreach (int Key in ACalcBlocks.Keys)
            {
                int cost = 0;
                int weight = 0;
                int volume = 0;
                if (ACalcBlocks[Key].BlockClass != "field_parameters")
                {
                    cost = MForm.Meta.GetIntValueOfParameter(ACalcBlocks[Key].BlockClass, ACalcBlocks[Key].BlockId, "cost_equipment");
                    weight = MForm.Meta.GetIntValueOfParameter(ACalcBlocks[Key].BlockClass, ACalcBlocks[Key].BlockId, "weight_equipment");
                    volume = MForm.Meta.GetIntValueOfParameter(ACalcBlocks[Key].BlockClass, ACalcBlocks[Key].BlockId, "volume_equipment");
                }
                string ModelName = MForm.Meta.GetStringValueOfParameter(ACalcBlocks[Key].BlockClass, ACalcBlocks[Key].BlockId, "name");
                weight = weight * ACalcBlocks[Key].Count;
                volume = volume * ACalcBlocks[Key].Count;
                cost = cost * ACalcBlocks[Key].Count;
                CombinationGridView.Rows.Add(CMeta.DictionaryName[ACalcBlocks[Key].BlockClass], ModelName, ACalcBlocks[Key].Count, weight, volume, cost);
                common_weight += weight;
                common_volume += volume;
                common_cost += cost;
            }
            CombinationGridView.Rows.Add("", "", "Итого: ", common_weight, common_volume, common_cost);
        }
    }
}
