using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    public enum CreateType { PCDevice, PCStructure, PCComplex, PCNone };

    public class ProjectConfig
    {
        public string     Name { get; set; }
        public CreateType Type { get; set; }
        public bool isUserGoFuther { get; set; }
        public bool isNewProject { get; set; }

        public ProjectConfig()
        {
            SetConfigParameters("", CreateType.PCNone, false, false);
        }

        public void SetConfigParameters(string aName, CreateType aType, bool aUserChoice, bool aProjectStatus)
        {
            Name = aName;
            Type = aType;
            isUserGoFuther = aUserChoice;
            isNewProject = aProjectStatus;
        }
    }
}
