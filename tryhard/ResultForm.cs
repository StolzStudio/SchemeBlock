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
        private List<Dictionary<int, CalcBlock>> Combinations;
        private List<List<int>> ParametersCombinations;

        public ResultForm(MainForm AForm, List<Dictionary<int, CalcBlock>> ACalcBlocks)
        {
            MForm = AForm;
            InitializeComponent();
            Combinations = new List<Dictionary<int, CalcBlock>>(ACalcBlocks);
            CalcParametersCombinations();
            FillGrids(ACalcBlocks);
        }

        private void CalcParametersCombinations()
        {
            ParametersCombinations = new List<List<int>>();
            foreach (Dictionary<int, CalcBlock> Combination in Combinations)
            {
                int common_weight = 0;
                int common_volume = 0;
                int common_cost = 0;
                foreach (int Key in Combination.Keys)
                {
                    int cost = 0;
                    int weight = 0;
                    int volume = 0;
                    if (Combination[Key].BlockClass != "field_parameters")
                    {
                        cost = MForm.Meta.GetIntValueOfParameter(Combination[Key].BlockClass, Combination[Key].BlockId, "cost");
                        weight = MForm.Meta.GetIntValueOfParameter(Combination[Key].BlockClass, Combination[Key].BlockId, "weight");
                        volume = MForm.Meta.GetIntValueOfParameter(Combination[Key].BlockClass, Combination[Key].BlockId, "volume");
                    }
                    string ModelName = MForm.Meta.GetStringValueOfParameter(Combination[Key].BlockClass, Combination[Key].BlockId, "name");
                    weight = weight * Combination[Key].Count;
                    volume = volume * Combination[Key].Count;
                    cost = cost * Combination[Key].Count;
                    common_weight += weight;
                    common_volume += volume;
                    common_cost += cost;
                }
                ParametersCombinations.Add(new List<int>() { common_weight, common_volume, common_cost });
            }
        }

        public void FillCombinationGridView(int AIndex, Dictionary<int, CalcBlock> ACombination)
        {
            CombinationGridView.Rows.Clear();
            foreach (int Key in ACombination.Keys)
            {
                int cost = 0, weight = 0, volume = 0;
                if (ACombination[Key].BlockClass != "field_parameters")
                {
                    cost = MForm.Meta.GetIntValueOfParameter(ACombination[Key].BlockClass, ACombination[Key].BlockId, "cost");
                    weight = MForm.Meta.GetIntValueOfParameter(ACombination[Key].BlockClass, ACombination[Key].BlockId, "weight");
                    volume = MForm.Meta.GetIntValueOfParameter(ACombination[Key].BlockClass, ACombination[Key].BlockId, "volume");
                }
                string ModelName = MForm.Meta.GetStringValueOfParameter(ACombination[Key].BlockClass, ACombination[Key].BlockId, "name");
                CombinationGridView.Rows.Add(CMeta.DictionaryName[ACombination[Key].BlockClass], ModelName,
                                             ACombination[Key].Count, weight, volume, cost);
            }
            CombinationGridView.Rows.Add("", "", "Итого: ", ParametersCombinations[AIndex][0],
                                                            ParametersCombinations[AIndex][1],
                                                            ParametersCombinations[AIndex][2]);
        }

        public void FillGrids(List<Dictionary<int, CalcBlock>> ACombinationsBlocks)
        {
            /*Fill Combinations */

            for (int i = 0; i < ParametersCombinations.Count; i++)
            {
                CombinationsGridView.Rows.Add(i, ParametersCombinations[i][0], 
                                                 ParametersCombinations[i][1], 
                                                 ParametersCombinations[i][2]);
            }

            FillCombinationGridView(0, Combinations[0]);
        }

        private void CombinationsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < Combinations.Count)
            {
                int Index = (int)CombinationsGridView.Rows[e.RowIndex].Cells[0].Value;
                FillCombinationGridView(Index, Combinations[Index]);
            }
        }
    }
}
