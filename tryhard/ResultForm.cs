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

        public ResultForm(MainForm AForm, List<Dictionary<int, CalcBlock>> ACalcBlocks)
        {
            MForm = AForm;
            InitializeComponent();
            FillDataGrid(ACalcBlocks);
        }

        public void FillDataGrid(List<Dictionary<int, CalcBlock>> ACombinationsBlocks)
        {
            foreach (Dictionary<int, CalcBlock> CalcBlocks in ACombinationsBlocks)
            {
                int common_weight = 0;
                int common_volume = 0;
                int common_cost = 0;
                foreach (int Key in CalcBlocks.Keys)
                {
                    int cost = 0;
                    int weight = 0;
                    int volume = 0;
                    if (CalcBlocks[Key].BlockClass != "field_parameters")
                    {
                        cost = MForm.Meta.GetIntValueOfParameter(CalcBlocks[Key].BlockClass, CalcBlocks[Key].BlockId, "cost_equipment");
                        weight = MForm.Meta.GetIntValueOfParameter(CalcBlocks[Key].BlockClass, CalcBlocks[Key].BlockId, "weight_equipment");
                        volume = MForm.Meta.GetIntValueOfParameter(CalcBlocks[Key].BlockClass, CalcBlocks[Key].BlockId, "volume_equipment");
                    }
                    string ModelName = MForm.Meta.GetStringValueOfParameter(CalcBlocks[Key].BlockClass, CalcBlocks[Key].BlockId, "name");
                    weight = weight * CalcBlocks[Key].Count;
                    volume = volume * CalcBlocks[Key].Count;
                    cost = cost * CalcBlocks[Key].Count;
                    CombinationGridView.Rows.Add(CMeta.DictionaryName[CalcBlocks[Key].BlockClass], ModelName, CalcBlocks[Key].Count, weight, volume, cost);
                    common_weight += weight;
                    common_volume += volume;
                    common_cost += cost;
                }
                CombinationGridView.Rows.Add("", "", "Итого: ", common_weight, common_volume, common_cost);
            }
        }
    }
}
