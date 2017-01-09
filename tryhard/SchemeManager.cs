using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace tryhard
{
    public class SchemeManager
    {
        public int SelectedBlockIndex;
        public bool isAddBlockButtonClick;
        public bool isHaveSelectedBlock;
        public bool isOilFieldAdd;

        public Dictionary<int, SchemeBlock> Blocks = new Dictionary<int, SchemeBlock>();
        public List<SchemeLink> Links = new List<SchemeLink>();
        public List<string> ItemsIdList = new List<string>();

        private MainForm Form;
        private int block_counter;

        public SchemeManager(MainForm AForm)
        {
            SelectedBlockIndex = -1;
            isAddBlockButtonClick = false;
            isHaveSelectedBlock = false;
            isOilFieldAdd = false;

            Form = AForm;
            block_counter = 1;
    }

        public void AddBlock(Point Pos)
        {
            if (!isOilFieldAdd)
            {
                isOilFieldAdd = (string)Form.EquipmentCB.SelectedItem == "Месторождение";
            } 
            Blocks.Add(block_counter, new SchemeBlock(block_counter,
                       CMeta.DictionaryName[(string)Form.EquipmentCB.SelectedItem],
                       ItemsIdList[Form.ModelCB.SelectedIndex], Pos, Form));

            foreach (int Key in Blocks.Keys)
            {
                Blocks[Key].ClearFocus();
            }
            Blocks[block_counter].SetFocus();
            block_counter++;
            isAddBlockButtonClick = false;
        }
        
        public void ChangeSelectBlock()
        {
            Blocks[SelectedBlockIndex].BlockId = ItemsIdList[Form.ModelCB.SelectedIndex];
            Blocks[SelectedBlockIndex].BlockModelLabel.Text = (string)Form.ModelCB.SelectedItem;
        }

        public void AddSchemeLink(SchemeLink ANewLink)
        {
            Links.Add(ANewLink);
            Form.SchemePage.Invalidate();
        }

        public bool CheckLink(int AFirstBlockIndex, int ASecondBlockIndex)
        {
            foreach (SchemeLink Link in Links)
            {
                return (Link.FirstBlockIndex == AFirstBlockIndex) && (Link.SecondBlockIndex == ASecondBlockIndex);
            }
            return false;
        }

        public void ClearLinksFocus()
        {
            foreach(SchemeLink link in Links)
            {
                link.isFocus = false;
            }

            Form.SchemePage.Invalidate();
        }
        
        public void ClearBlocksFocus()
        {
            foreach (int key in Blocks.Keys)
            {
                Blocks[key].ClearFocus();
            }

            Form.SchemePage.Invalidate();
        }

        public void TrySetFocusInLinks(Point Coord)
        {
            foreach(SchemeLink link in Links)
            {
                link.TrySetFocus(Coord);
                if (link.isFocus)
                {
                    Form.DeleteBlockButton.Visible = true;
                }
            }
            Form.SchemePage.Invalidate();
        }
    }
}
