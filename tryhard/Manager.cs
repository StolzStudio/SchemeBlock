using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace tryhard
{
    public class Manager
    {
        public int SelectedBlockIndex;
        public bool isAddBlockButtonClick;
        public bool isHaveSelectedBlock;
        public bool isOilFieldAdd;

        public Dictionary<int, Block> Blocks;
        public List<Link> Links;
        public List<string> ItemsIdList;

        private DrawPage Page;
        private int block_counter;

        public Manager(DrawPage aPage)
        {
            SelectedBlockIndex = -1;

            Blocks = new Dictionary<int, Block>();
            Links = new List<Link>();
            ItemsIdList = new List<string>();

            isAddBlockButtonClick = false;
            isHaveSelectedBlock = false;
            isOilFieldAdd = false;

            Page = aPage;
            block_counter = 1;
        }

        public void AddBlock(Point Pos)
        {
            if (!isOilFieldAdd)
            {
                //isOilFieldAdd = (string)Form.ObjectTypeCB.SelectedItem == "Месторождение";
            }

            Blocks.Add(block_counter, new Block(block_counter, "type", "class", Pos, Page.Location));

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
            //Blocks[SelectedBlockIndex].BlockModelLabel.Text = (string)Form.ObjectModelCB.SelectedItem;
            //Blocks[SelectedBlockIndex].BlockId = ItemsIdList[Form.ObjectModelCB.SelectedIndex];
        }

        public void AddLink(Link ANewLink)
        {
            Links.Add(ANewLink);
            Page.Invalidate();
        }

        public bool CheckLink(int AFirstBlockIndex, int ASecondBlockIndex)
        {
            foreach (Link Link in Links)
            {
                return (Link.FirstBlockIndex == AFirstBlockIndex) && (Link.SecondBlockIndex == ASecondBlockIndex);
            }
            return false;
        }

        public void ClearLinksFocus()
        {
            foreach (Link link in Links)
            {
                link.isFocus = false;
            }

            Page.Invalidate();
        }

        public void ClearBlocksFocus()
        {
            foreach (int key in Blocks.Keys)
            {
                Blocks[key].ClearFocus();
            }

            Page.Invalidate();
        }

        public void TrySetFocusInLinks(Point Coord)
        {
            foreach (Link link in Links)
            {
                link.TrySetFocus(Coord);
            }
            Page.Invalidate();
        }

        public void DrawElements(Graphics g)
        {
            if (Links.Count != 0)
            {
                foreach (Link Link in Links)
                {
                    Link.Draw(this.Blocks, g);
                }
            }
            if (Blocks.Count != 0)
            {
                foreach (int Key in Blocks.Keys)
                {
                    Blocks[Key].Draw(g);
                }
            }
        }
    }
}
