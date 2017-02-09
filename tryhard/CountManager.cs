using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    class CountManager
    {
        private bool isEquipment;
        private List<List<int>> BlocksIndex;
        private Dictionary<string, List<int>> BlocksIndexCombination;

        public Dictionary<int, Block> Blocks;
        public List<Link> Links;

        public CountManager(ref Dictionary<int, Block> aBlocks, ref List<Link> aLinks)
        {
            isEquipment = false;

            Blocks = aBlocks;
            Links  = aLinks;
        }

        public CountManager(ref Dictionary<int, Block> aBlocks)
        {
            isEquipment = true;

            Blocks = aBlocks;
            BlocksIndex = new List<List<int>>();
            BlocksIndexCombination = new Dictionary<string, List<int>>();
            SetIndexArray();
        }
        
        public void SetIndexArray()
        {
            foreach (var key in Blocks.Keys)
            {
                BlocksIndex.Add(MetaDataManager.Instance.GetIdCortageByType(Blocks[key].ClassText));
                MessageBox.Show(BlocksIndex[0][1].ToString());
            }
        }
    }
}
