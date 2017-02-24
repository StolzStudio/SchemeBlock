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
    public partial class SetParamForm : Form
    {
        private string ObjectCategory = "";
        private string ObjectType     = "";
        private int    ObjectId       = -1;
        private BaseObject SaveObject;

        public SetParamForm(string aCategory, string aType)
        {
            ObjectCategory = aCategory;
            ObjectType     = aType;

            InitializeComponent();
            FillControls();
        }

        public SetParamForm(string aCategory, string aType, int aId)
        {
            ObjectCategory = aCategory;
            ObjectType     = aType;
            ObjectId       = aId;

            InitializeComponent();
            FillControls();
        }

        private void FillControls()
        {
            CategoryLabel.Text = ObjectCategory + ":";
            TypeLabel.Text     = ObjectType;
            FillObjectParamDataGridView();
        }

        private void FillObjectParamDataGridView()
        {
            Type SaveObjectType = Type.GetType("tryhard." + GiveTypeName());

            SaveObject = (BaseObject)Activator.CreateInstance(SaveObjectType);

            foreach (var property in SaveObjectType.GetProperties())
            {
                if ((property.Name != "Id") && (property.Name != "Structure"))
                {
                    ObjectParamDataGridView.Rows.Add(MetaDataManager.Instance.Dictionary[property.Name], SaveObject.GetType().GetProperty(property.Name).GetValue(SaveObject));
                }
            }
        }

        private string GiveTypeName()
        {
            switch (MetaDataManager.Instance.Dictionary[ObjectType])
            {
                case "integrated_complex": return "IntegratedComplex"; break;
                case "processing_complex": return "ProcessingComplex"; break;
                case "field_parameters": return "FieldParameters"; break;
                case "mining_complex": return "MiningComplex"; break;
                case "oil_quality": return "OilQuality"; break;
                case "ukppv": return "Ukppv"; break;
                case "ukpg": return "Ukpg"; break;
                case "nnpv": return "Nnpv"; break;
                case "rpv": return "Rpv"; break;
                case "rtn": return "Rtn"; break;
                case "upn": return "Upn"; break;
                case "dks": return "Dks"; break;
                case "dk": return "Dk"; break;
                case "rr": return "Rr"; break;
                case "fu": return "Fu"; break;
            }
            return null;
        }

        private void ObjectParamDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (SaveObject != null)
            {
                if (ObjectParamDataGridView.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    if (ObjectParamDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() != "Имя")
                    {
                        SaveObject.GetType().GetProperty(MetaDataManager.Instance.Dictionary[ObjectParamDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()]).SetValue(SaveObject, Convert.ToInt64(ObjectParamDataGridView.Rows[e.RowIndex].Cells[1].Value));
                    }
                    else
                    {
                        SaveObject.GetType().GetProperty(MetaDataManager.Instance.Dictionary[ObjectParamDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()]).SetValue(SaveObject, ObjectParamDataGridView.Rows[e.RowIndex].Cells[1].Value);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<int> Ids = MetaDataManager.Instance.GetIdCortageByType(MetaDataManager.Instance.Dictionary[ObjectType]);
            ObjectsStructure structure = new ObjectsStructure();
            //MetaDataManager.Instance.FillObjectStructure(DrawManager.Links, Combinations[ind], ref structure);
            if (ObjectId != -1)
            {
                MetaDataManager.Instance.Objects[MetaDataManager.Instance.Dictionary[ObjectType]][ObjectId] = SaveObject;
            }
            else
            {
                SaveObject.Id = Ids.Max() + 1;
                MetaDataManager.Instance.Objects[MetaDataManager.Instance.Dictionary[ObjectType]].Add(SaveObject);
            }
            MetaDataManager.Instance.PushObjectStructure(MetaDataManager.Instance.Dictionary[ObjectType], SaveObject.Id, structure);
            this.Close();
        }
    }
}
