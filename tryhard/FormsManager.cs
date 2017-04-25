using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    class FormsManager
    {
        private static FormsManager instance;
        private FormsManager() { }

        public MainForm Form;
        public List<EditorForm> EditForms = new List<EditorForm>();
        public static FormsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FormsManager();
                }
                return instance;
            }
        }

        public void Initialize(MainForm form)
        {
            Form = form;
        }

        public void AddEditForm(EditorForm editForm)
        {
            EditForms.Add(editForm);
        }

        public void DeleteEditForm(EditorForm editForm)
        {
            EditForms.Remove(editForm);
        }

        public void UpdateViewControls()
        {
            Form.FillObjectTreeView();
            foreach (EditorForm f in EditForms)
                f.FillObjectTreeView();
        }
    }
}
