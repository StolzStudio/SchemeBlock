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
        private List<Dictionary<int, CalcBlock>> BlocksCombinations;

        public ResultForm(MainForm AForm, List<Dictionary<int, CalcBlock>> ACalcBlocks)
        {
            MForm = AForm;
            InitializeComponent();
            FillGrids(ACalcBlocks);
        }

        public void FillCombinationsGridView(int AId, int AWeight, int AVolume, int ACost)
        {
            CombinationsGridView.Rows.Add(AId, AWeight, AVolume, ACost);
        }

        public void FillCombinationGridView(int AIndex, Dictionary<int, CalcBlock> ACalcBlocks)
        {
            CombinationGridView.Rows.Clear();
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
                if (AIndex == 0)
                {
                    CombinationGridView.Rows.Add(CMeta.DictionaryName[ACalcBlocks[Key].BlockClass], ModelName,
                                                 ACalcBlocks[Key].Count, weight, volume, cost);
                }
                common_weight += weight;
                common_volume += volume;
                common_cost += cost;
            }
            FillCombinationsGridView(AIndex, common_weight, common_volume, common_cost);
            if (AIndex == 0)
                CombinationGridView.Rows.Add("", "", "Итого: ", common_weight, common_volume, common_cost);
        }

        public void FillGrids(List<Dictionary<int, CalcBlock>> ACombinationsBlocks)
        {
            BlocksCombinations = new List<Dictionary<int, CalcBlock>> (ACombinationsBlocks);
            int i = 0;
            foreach (Dictionary<int, CalcBlock> CalcBlocks in BlocksCombinations)
            {
                FillCombinationGridView(i, CalcBlocks);
                i++;
            }
        }

        private void CombinationsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillCombinationGridView(0, (BlocksCombinations[e.RowIndex]));
        }
    }
}
