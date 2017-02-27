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
        public bool isHaveSelectedBlock;
        public bool isOilFieldAdd;

        public Dictionary<int, Block> Blocks;
        public List<Link> Links;

        private DrawPage Page;
        private int block_counter;

        public Manager(DrawPage aPage)
        {
            SelectedBlockIndex = -1;

            Blocks = new Dictionary<int, Block>();
            Links = new List<Link>();

            isHaveSelectedBlock = false;
            isOilFieldAdd = false;

            Page = aPage;
            block_counter = 1;
        }

        public void AddBlock(Point Pos, string ABlockType, string ABlockModel, int ABlockId)
        {
            Blocks.Add(block_counter, new Block(block_counter, ABlockType, ABlockModel, ABlockId, Pos, Page.Location));

            foreach (int Key in Blocks.Keys)
            {
                Blocks[Key].ClearFocus();
            }
            Blocks[block_counter].SetFocus(); 
            SelectedBlockIndex = block_counter;
            block_counter++;
        }

        public void LoadStructureOfObject(string AType, int AId)
        {
            DeleteAllElements();
            MetaDataManager.Instance.FillDrawingObjectStructure(AType, AId, ref Links, ref Blocks, Page.Location);
            if (Blocks.Count != 0)
                this.block_counter = Blocks.Max(block => block.Value.Index) + 1;
            Page.Invalidate();
        }

        public void LoadProjectStructureOfObject(int AId)
        {
            DeleteAllElements();
            MetaDataManager.Instance.FillDrawingObjectProjectStructure(AId, ref Links, ref Blocks, Page.Location);
            if (Blocks.Count != 0)
                this.block_counter = Blocks.Max(block => block.Value.Index) + 1;
            Page.Invalidate();
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

        public Link GetFocusedLink()
        {
            for (int i = 0; i < Links.Count; i++)
                if (Links[i].isFocus)
                    return Links[i];
            return null;
        }

        public void UpdateFocusedLink(string ALinkParameter, int ALinkParameterValue)
        {
            for (int i = 0; i < Links.Count; i++)
                if (Links[i].isFocus)
                {
                    Links[i].LinkParameter = ALinkParameter;
                    Links[i].LinkParameterValue = ALinkParameterValue;
                }
        }

        public void ClearBlocksFocus()
        {
            foreach (int key in Blocks.Keys)
            {
                Blocks[key].ClearFocus();
            }

            isHaveSelectedBlock = false;
            SelectedBlockIndex = -1;
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

        public void DeleteAllElements()
        {
            Links.Clear();
            Blocks.Clear();
            SelectedBlockIndex = -1;
        }

        public void DeleteElements()
        {
            Link[] LinksArr = Links.ToArray();
            Links.Clear();

            for (int i = 0; i < LinksArr.Length; i++)
            {
                if (!isHaveSelectedBlock)
                {
                    if (LinksArr[i].isFocus) { LinksArr[i] = null; }
                }
                else
                {
                    if (LinksArr[i].CheckDeletedLink(SelectedBlockIndex)) { LinksArr[i] = null; }
                }
            }

            LinksArr = LinksArr.Where(x => x != null).ToArray();
            Links = LinksArr.ToList();

            if (SelectedBlockIndex != -1)
            {
                if (Blocks[SelectedBlockIndex].isFocus)
                {
                    Blocks.Remove(SelectedBlockIndex);
                    isHaveSelectedBlock = false;
                    SelectedBlockIndex = -1;
                }
            }
        }

        public bool TrySetFocusInBlocks(Point Coord)
        {
            SelectedBlockIndex = -1;
            foreach (int Key in Blocks.Keys)
            {
                if (Blocks[Key].CheckFocus(Coord))
                {
                    isHaveSelectedBlock = true;
                    ClearLinksFocus();
                    SelectedBlockIndex = Key;
                    return true;
                }
            }
            return false;
        }

        public bool TrySetFocusInLinks(Point Coord)
        {
            foreach (Link link in Links)
            {
                link.TrySetFocus(Coord);
                if (link.isFocus)
                {
                    Page.Invalidate();
                    return true;
                }
            }
            Page.Invalidate();
            return false;
        }
    }
}
