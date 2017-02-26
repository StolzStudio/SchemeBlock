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
            Type SaveObjectType = Type.GetType("tryhard." + MetaDataManager.Instance.GiveTypeName(MetaDataManager.Instance.Dictionary[ObjectType]));

            if (ObjectId != -1)
            {
                SaveObject = MetaDataManager.Instance.Objects[MetaDataManager.Instance.Dictionary[ObjectType]][ObjectId];
            }
            else
            {
                SaveObject = (BaseObject)Activator.CreateInstance(SaveObjectType);
            }

            foreach (var property in SaveObjectType.GetProperties())
            {
                if ((property.Name != "Id") && (property.Name != "Structure"))
                {
                    ObjectParamDataGridView.Rows.Add(MetaDataManager.Instance.Dictionary[property.Name], SaveObject.GetType().GetProperty(property.Name).GetValue(SaveObject));
                }
            }
        }

        private void ObjectParamDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (SaveObject != null)
            {
                if (ObjectParamDataGridView.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    if (ObjectParamDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() != "Имя")
                    {
                        SaveObject.GetType().GetProperty(MetaDataManager.Instance.Dictionary[ObjectParamDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()]).SetValue(SaveObject, Convert.ToInt32(ObjectParamDataGridView.Rows[e.RowIndex].Cells[1].Value));
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
            foreach (DataGridViewRow row in ObjectParamDataGridView.Rows)
            {
                if ((row.Cells[1].Value == null)||(row.Cells[1].Value == ""))
                {
                    MessageBox.Show("Перед сохранением нужно заполнить все поля");
                    return;
                }
            }
            List<int> Ids = MetaDataManager.Instance.GetIdCortageByType(MetaDataManager.Instance.Dictionary[ObjectType]);
            ObjectsStructure structure = new ObjectsStructure();
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
