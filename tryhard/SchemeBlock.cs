using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    class SchemeBlock
    {
        //{consts}
            private int BlockBodyWidth  = 120;
            private int BlockBodyHeight = 60;
        //{end}

        //{controls}
            public System.Windows.Forms.Panel BlockBody;
            public System.Windows.Forms.Label NameLabel;
        //{end}

        public SchemeBlock(string AName)
        {
            InitializeComponent(AName);
        }

        private void InitializeComponent(string AName)
        {
            //{BlockBody}
                this.BlockBody = new System.Windows.Forms.Panel();

                this.BlockBody.BackColor =     System.Drawing.Color.Beige;
                this.BlockBody.Location  = new System.Drawing.Point(12, 12);
                this.BlockBody.Size      = new System.Drawing.Size(BlockBodyWidth, BlockBodyHeight);
            //{end}

            //{NameLabel}
                this.NameLabel = new System.Windows.Forms.Label();

                this.NameLabel.Location = new System.Drawing.Point(0, 0);
                this.NameLabel.Text     = AName;

                this.BlockBody.Controls.Add(this.NameLabel);
            //{end}
        }
    }
}
